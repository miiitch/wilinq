using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.CSharp;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using Microsoft.VisualBasic;
using WiLinq.CodeGen.CodeGeneration;

namespace WiLinq.CodeGen.ViewModels
{
    internal class LinqCodeGenerationViewModel : ViewModelBase
    {
        private bool _cSharpCode;
        private readonly ModelDefinition _modelDefinition;

        private readonly Engine _codeGenerationEngine;


        public bool CSharpCode
        {
            get => _cSharpCode;
            set
            {
                _cSharpCode = value;
                RaisePropertyChanged(() => CSharpCode);
                RaisePropertyChanged(() => VbNetCode);
            }
        }

        public bool VbNetCode
        {
            get => !_cSharpCode;
            set
            {
                _cSharpCode = !value;
                RaisePropertyChanged(() => CSharpCode);
                RaisePropertyChanged(() => VbNetCode);
            }
        }

        public string Namespace
        {
            get => _modelDefinition.Namespace;
            set => _modelDefinition.Namespace = value;
        }

        public List<ModelClassDefinitionViewModel> Classes { get; }

        public ICommand GenerateClassesCommand { get; }

        public LinqCodeGenerationViewModel(Project project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project), @"project is null.");
            }

            CSharpCode = true;
            _codeGenerationEngine = new Engine();
            _modelDefinition = _codeGenerationEngine.GenerateModelDefinition(project);
            Classes = _modelDefinition.ClassDefinitions.Select(cd => new ModelClassDefinitionViewModel(cd)).ToList();
            GenerateClassesCommand = new RelayCommand(DoGenerateClasses);
        }

        private void DoGenerateClasses()
        {
            var dialog = new SaveFileDialog();

            using (dialog)
            {
                dialog.Title = @"Save the generated code";

                CodeDomProvider codeProvider;
                if (CSharpCode)
                {
                    dialog.DefaultExt = "cs";
                    dialog.Filter = @"CSharp source file (*.cs)| *.cs";
                    codeProvider = new CSharpCodeProvider();
                }
                else
                {
                    dialog.DefaultExt = "vb";
                    dialog.Filter = @"VB.Net source file (*.vb)| *.vb";
                    codeProvider = new VBCodeProvider();
                }

                var dialogResult = dialog.ShowDialog();

                if (dialogResult != DialogResult.OK)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(dialog.FileName))
                {
                    return;
                }

                var fileStream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write);

                using (fileStream)
                using (var writer = new StreamWriter(fileStream))
                {
                    _codeGenerationEngine.GenerateCode(_modelDefinition, codeProvider, writer);
                    writer.Flush();
                    fileStream.Flush();
                }

                MessageBox.Show(@"Generation successful");
            }
        }


#if false
        private void DoGenerate()
        {
            var saveFileMessage = new SaveFileMessage();

            saveFileMessage.DialogTitle = "Save the generated code";

            CodeDomProvider codeProvider = null;
            if (CSharpCode)
            {
                saveFileMessage.DefaultExtension = "cs";
                saveFileMessage.Filters = "CSharp source file (*.cs)| *.cs";
                codeProvider = new CSharpCodeProvider();
            }
            else
            {
                saveFileMessage.DefaultExtension = "vb";
                saveFileMessage.Filters = "VB.Net source file (*.vb)| *.vb";
                codeProvider = new VBCodeProvider();
            }

            _addin.Manager.Messenger.Send(saveFileMessage);

            if (string.IsNullOrWhiteSpace(saveFileMessage.FilePath))
            {
                return;
            }

            var fileStream = new FileStream(saveFileMessage.FilePath, FileMode.Create, FileAccess.Write);

            using (fileStream)                            
            using (var writer = new StreamWriter(fileStream))
            {
                _codeGenerationEngine.GenerateCode(_modelDefinition, codeProvider, writer);
                writer.Flush();
                fileStream.Flush();
                fileStream.Close();
            }                               
            

            UserSimpleTextMessage msg = new UserSimpleTextMessage();

            msg.Title = "Code generation";
            msg.Message = "Successfull";

            _addin.Manager.Messenger.Send(msg);
        }
#endif
    }
}