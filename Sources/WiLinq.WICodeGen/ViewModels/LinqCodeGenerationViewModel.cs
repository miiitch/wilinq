using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using Microsoft.VisualBasic;
using System.IO;
using GalaSoft.MvvmLight;
using WiLinq.CodeGen.CodeGeneration;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System.Windows.Input;
using System.Windows.Forms;

namespace WiLinq.CodeGen.ViewModels
{
    class LinqCodeGenerationViewModel : ViewModelBase
    {
      
        private bool _cSharpCode;
        private ModelDefinition _modelDefinition;
        Project _project;

        Engine _codeGenerationEngine;


        public bool CSharpCode
        {
            get
            {
                return _cSharpCode;
            }
            set
            {
                _cSharpCode = value;
                RaisePropertyChanged(() => CSharpCode);
                RaisePropertyChanged(() => VBNetCode);
            }
        }
        public bool VBNetCode
        {
            get
            {
                return !_cSharpCode;
            }
            set
            {
                _cSharpCode = !value;
                RaisePropertyChanged(() => CSharpCode);
                RaisePropertyChanged(() => VBNetCode);
            }
        }
        public string Namespace
        {
            get
            {
                return _modelDefinition.Namespace;
            }
            set
            {
                _modelDefinition.Namespace = value;
            }
        }
        public List<ModelClassDefinitionViewModel> Classes { get; private set; }

        public ICommand GenerateClassesCommand { get; private set; }

        public LinqCodeGenerationViewModel(Project project)
            : base()
        {
            if (project == null)
                throw new ArgumentNullException("project", "project is null.");
            _project = project;

            CSharpCode = true;
            _codeGenerationEngine = new Engine();
            _modelDefinition = _codeGenerationEngine.GenerateModelDefinition(project);
            Classes = _modelDefinition.ClassDefinitions.Select(cd => new ModelClassDefinitionViewModel(cd)).ToList();
            GenerateClassesCommand = new RelayCommand(() => DoGenerateClasses());
        }

        private void DoGenerateClasses()
        {
            SaveFileDialog dialog = new SaveFileDialog();

            using (dialog)
            {
                dialog.Title = "Save the generated code";
                
                CodeDomProvider codeProvider = null;
                if (CSharpCode)
                {
                    dialog.DefaultExt = "cs";
                    dialog.Filter = "CSharp source file (*.cs)| *.cs";
                    codeProvider = new CSharpCodeProvider();
                }
                else
                {
                    dialog.DefaultExt = "vb";
                    dialog.Filter = "VB.Net source file (*.vb)| *.vb";
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

                MessageBox.Show("Generation successful");
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
