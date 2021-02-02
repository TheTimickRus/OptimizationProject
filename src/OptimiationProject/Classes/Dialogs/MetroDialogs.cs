using MahApps.Metro.Controls.Dialogs;

namespace OptimiationProject.Classes.Dialogs
{
    public static class MetroDialogs
    {
        public static async void MainWindowMd(string title, string message)
        {
            if (Settings.Settings.MainWindow == null)
            {
                return;
            }

            await Settings.Settings.MainWindow.ShowMessageAsync(title, 
                                                                message, 
                                                                MessageDialogStyle.Affirmative, 
                                                                new MetroDialogSettings
                                                                {
                                                                    AffirmativeButtonText = "OK",
                                                                    ColorScheme = MainModel.Instanse.UseAccentForDialogs 
                                                                        ? MetroDialogColorScheme.Accented 
                                                                        : MetroDialogColorScheme.Theme
                                                                });
        }

        public static async void MainWindowGw(string title, string message)
        {
            if (Settings.Settings.GraphWindow == null)
            {
                return;
            }

            await Settings.Settings.GraphWindow.ShowMessageAsync(title,
                                                                 message,
                                                                 MessageDialogStyle.Affirmative,
                                                                 new MetroDialogSettings
                                                                 {
                                                                     AffirmativeButtonText = "OK",
                                                                     ColorScheme = MainModel.Instanse.UseAccentForDialogs
                                                                         ? MetroDialogColorScheme.Accented
                                                                         : MetroDialogColorScheme.Theme
                                                                 });
        }
    }
}
