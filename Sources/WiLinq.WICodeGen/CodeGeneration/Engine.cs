using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using WiLinq.LinqProvider.Extensions;



namespace WiLinq.CodeGen.CodeGeneration
{
    /// <summary>
    /// Class used to generate the model
    /// </summary>
    public class Engine
    {
        private readonly List<string> _fieldToIgnoreList;
        private readonly List<string> _definedPropertyList;
        private FieldSeparator _fieldSeparator = FieldSeparator.NoSpace;

        public FieldSeparator FieldSeparator
        {
            get
            {
                return _fieldSeparator;
            }
            set
            {
                _fieldSeparator = value;
            }
        }

        public Engine()
        {
            _fieldToIgnoreList = new List<string>();
            _definedPropertyList = new List<string>();

            foreach (var pi in typeof(WorkItemBase).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                _definedPropertyList.Add(pi.Name);
                var attribs = pi.GetCustomAttributes(typeof(FieldAttribute), false) as FieldAttribute[];

                if (attribs == null || attribs.Length != 1)
                {
                    continue;
                }

                _fieldToIgnoreList.Add(attribs[0].ReferenceName);
            }

            foreach (var ignoreField in
                typeof(WorkItemBase)
                .GetCustomAttributes(typeof(IgnoreFieldAttribute), false)
                .OfType<IgnoreFieldAttribute>())
            {
                _fieldToIgnoreList.Add(ignoreField.ReferenceName);
            }
        }


        public ModelDefinition GenerateModelDefinition(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException("project");
            }

            var result = new ModelDefinition();

            var q = from wit in project.WorkItemTypes.Cast<WorkItemType>()
                    let classDef = new ModelClassDefinition
                    {
                        WorkItemType = wit.Name,
                        ClassName = Regex.Replace(wit.Name, @"\W", String.Empty),
                        FieldDefinitions = (from field in wit.FieldDefinitions.Cast<FieldDefinition>()
                                            select new ModelFieldDefinition
                                            {
                                                Name = field.Name,
                                                PropertyName = Regex.Replace(field.Name, @"\W", String.Empty),
                                                IsReadOnly = (!field.IsEditable) || field.IsComputed,
                                                Description = field.HelpText,
                                                ReferenceName = field.ReferenceName,
                                                Inherited = _fieldToIgnoreList.Contains(field.ReferenceName),
                                                Type = field.SystemType
                                            }).ToList()


                    }
                    select classDef;
            result.ClassDefinitions = q.ToList();


            return result;
        }


        /// <summary>
        /// Method used to generate model from a modelDefinition
        /// </summary>
        /// <param name="modeldefinition"></param>
        /// <param name="codeProvider"></param>
        /// <param name="output"></param>
        /// <returns></returns>        
        public bool GenerateCode(ModelDefinition modeldefinition, CodeDomProvider codeProvider, TextWriter output)
        {
            var unit = new CodeCompileUnit();

            // Declare a new namespace called Samples.
            var codeNamespace = new CodeNamespace(modeldefinition.Namespace);
            codeNamespace.Imports.Add(new CodeNamespaceImport("WiLinq.LinqProvider"));
            // Add the new namespace to the compile unit.
            unit.Namespaces.Add(codeNamespace);


            foreach (var classDefinition in modeldefinition.ClassDefinitions)
            {

                var wiTypeClass = GenerateWorkItemTypeClass(classDefinition);
                codeNamespace.Types.Add(wiTypeClass);


            }

            var options = new CodeGeneratorOptions {BlankLinesBetweenMembers = true, IndentString = "  "};

            try
            {
                codeProvider.GenerateCodeFromCompileUnit(unit, output, options);
                output.Flush();
            }
            catch
            {
                return false;
            }

            return true;
        }

        private CodeTypeDeclaration GenerateWorkItemTypeClass(ModelClassDefinition classDefinition)
        {
            var wiTypeClass = new CodeTypeDeclaration(classDefinition.ClassName)
            {
                // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
                Attributes = MemberAttributes.Public | MemberAttributes.Final,
                IsPartial = true,
                IsClass = true
            };
            wiTypeClass.BaseTypes.Add(typeof(WorkItemBase));

            wiTypeClass.CustomAttributes.Add(new CodeAttributeDeclaration(
                new CodeTypeReference(typeof(WorkItemTypeAttribute)),
                new CodeAttributeArgument(new CodePrimitiveExpression(classDefinition.WorkItemType))));



            if (!String.IsNullOrEmpty(classDefinition.Description))
            {

                var comment = new CodeComment(String.Format("<summary>{0}</summary>", classDefinition.Description), true);
                wiTypeClass.Comments.Add(new CodeCommentStatement(comment));
            }


            //GenerateConstructor(wiTypeClass);

            var sortedFields = from field in classDefinition.FieldDefinitions
                               orderby field.Name
                               select field;

            foreach (var fieldDefinition in sortedFields)
            {
                if (_fieldToIgnoreList.Contains(fieldDefinition.ReferenceName))
                {
                    continue;
                }

                GenerateField(wiTypeClass, fieldDefinition);
            }
            return wiTypeClass;
        }


        /// <summary>
        /// Generates a field.
        /// </summary>
        /// <param name="wiTypeClass">The wi type class.</param>
        /// <param name="fieldDefinition">The field definition.</param>
        private void GenerateField(CodeTypeDeclaration wiTypeClass, ModelFieldDefinition fieldDefinition)
        {
            var property = new CodeMemberProperty {HasGet = true};

            var propName = GeneratePropertyName(fieldDefinition);

            property.Name = propName;

            var fieldType = fieldDefinition.Type;
            if (!fieldType.IsClass)
            {
                var nullableType = typeof(Nullable<>);
                fieldType = nullableType.MakeGenericType(fieldType);

            }
            property.Type = new CodeTypeReference(fieldType);
            property.Attributes = MemberAttributes.Public;

            property.CustomAttributes.Add(new CodeAttributeDeclaration(
                   new CodeTypeReference(typeof(FieldAttribute)),
                   new CodeAttributeArgument(new CodePrimitiveExpression(fieldDefinition.ReferenceName))));


            if (!String.IsNullOrEmpty(fieldDefinition.Description))
            {

                var comment = new CodeComment(String.Format("<summary>{0}</summary>", fieldDefinition.Description), true);
                property.Comments.Add(new CodeCommentStatement(comment));
            }

            var fieldMethod = new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "WorkItem"), "Field", new[] { new CodeTypeReference(fieldType) });
            var fieldMethodInvoke = new CodeMethodInvokeExpression(fieldMethod, new CodeExpression[] { new CodePrimitiveExpression(fieldDefinition.ReferenceName) });

            property.GetStatements.Add(new CodeMethodReturnStatement(fieldMethodInvoke));


            if (!fieldDefinition.IsReadOnly)
            {

                var setFieldMethod = new CodeMethodReferenceExpression(new CodePropertyReferenceExpression(new CodeThisReferenceExpression(), "WorkItem"), "SetField", new[] { new CodeTypeReference(fieldType) });
                var setFielMethodInvoke = new CodeMethodInvokeExpression(setFieldMethod, new CodeExpression[] { new CodePrimitiveExpression(fieldDefinition.ReferenceName), new CodePropertySetValueReferenceExpression() });
                property.SetStatements.Add(setFielMethodInvoke);
            }
            wiTypeClass.Members.Add(property);
            _definedPropertyList.Add(property.Name);
        }

        /// <summary>
        /// Generates the name of the property.
        /// </summary>
        /// <param name="fieldDefinition">The field definition.</param>
        /// <returns></returns>
        private string GeneratePropertyName(ModelFieldDefinition fieldDefinition)
        {
            if (!String.IsNullOrEmpty(fieldDefinition.PropertyName))
            {
                return fieldDefinition.PropertyName;
            }

            string replacement;

            switch (_fieldSeparator)
            {
                case FieldSeparator.NoSpace:
                    replacement = String.Empty;
                    break;
                case FieldSeparator.Underscore:
                    replacement = "_";
                    break;
                default:
                    throw new GenerationException("InvalidSeparatorToken");
            }

            var propName = Regex.Replace(fieldDefinition.Name, @"\W+", replacement);

            if (_definedPropertyList.Contains(propName))
            {
                throw new GenerationException("PropertyNameConflict", fieldDefinition.ReferenceName);
            }

            return propName;
        }

/*
        private static void GenerateConstructor(CodeTypeDeclaration wiTypeClass)
        {
            var constructor = new CodeConstructor();
            constructor.Attributes = MemberAttributes.Public;

            wiTypeClass.Members.Add(constructor);
        }
*/
    }
}
