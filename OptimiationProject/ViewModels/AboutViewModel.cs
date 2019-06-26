using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OptimiationProject.Classes.Settings;
using OptimiationProject.Windows.Graph;

namespace OptimiationProject.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public ICommand BGraphCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Settings.MainWindow.Hide();

                    Settings.GraphWindow = new GraphWindow();
                    Settings.GraphWindow.ShowDialog();

                    Settings.MainWindow.Show();
                });
            }
        }
    }
}
