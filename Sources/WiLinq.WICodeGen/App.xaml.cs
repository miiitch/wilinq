using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace WiLinq.CodeGen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnLoadCompleted(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnLoadCompleted(e);
           
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            using (var picker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false))
            {

                var result = picker.ShowDialog();

                if (result != System.Windows.Forms.DialogResult.OK)
                {
                    
                    return;
                }


                var tpc = picker.SelectedTeamProjectCollection;
                tpc.Authenticate();
                var store = tpc.GetService<WorkItemStore>();
                var project = store.Projects.Cast<Project>().First(_ => _.Name == picker.SelectedProjects[0].Name);

                this.MainWindow = new MainWindow()
                {
                    DataContext = new ViewModels.LinqCodeGenerationViewModel(project)
                };
                this.MainWindow.Show();
            }
        }
    }
}
