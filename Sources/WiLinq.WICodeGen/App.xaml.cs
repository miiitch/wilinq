using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.WorkItemTracking.Client;
using WiLinq.CodeGen.ViewModels;

namespace WiLinq.CodeGen
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            using (var picker = new TeamProjectPicker(TeamProjectPickerMode.SingleProject, false))
            {
                var result = picker.ShowDialog();

                if (result != DialogResult.OK)
                {
                    return;
                }


                var tpc = picker.SelectedTeamProjectCollection;
                tpc.Authenticate();
                var store = tpc.GetService<WorkItemStore>();
                var project = store.Projects.Cast<Project>().First(_ => _.Name == picker.SelectedProjects[0].Name);

                MainWindow = new MainWindow
                {
                    DataContext = new LinqCodeGenerationViewModel(project)
                };
                MainWindow.Show();
            }
        }
    }
}