using OptimiationProject.Classes.Settings;

namespace OptimiationProject
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Settings.MainWindow = this;
        }
        
        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.GetInstanse().Save();
            Settings.GraphWindow?.Close();
        }
    }
}
