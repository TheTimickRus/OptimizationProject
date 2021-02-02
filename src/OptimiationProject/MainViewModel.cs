using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro;
using OptimiationProject.Classes.Settings;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace OptimiationProject
{
    public class MainViewModel : ViewModelBase
    {
        public ReadOnlyObservableCollection<string> BaseColor { get; } = ThemeManager.BaseColors;
        public ReadOnlyObservableCollection<ColorScheme> Color { get; } = ThemeManager.ColorSchemes;

        private MainModel _myMainModel;
        public MainModel MyMainModel
        {
            get => _myMainModel;
            set
            {
                _myMainModel = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            Settings.GetInstanse().Load();

            MyMainModel = MainModel.Instanse;
            MyMainModel.PropertyChanged += MyMainModelOnPropertyChanged;
        }

        private void MyMainModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            MainSettings.Instanse.MyMainModel = MyMainModel;
        }

        public ICommand ChangeToggleFullScreen
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (MyMainModel.ToggleFullScreen)
                    {
                        MyMainModel.ShowTitleBar = false;

                        Settings.MainWindow.IgnoreTaskbarOnMaximize = true;
                        Settings.MainWindow.WindowState = WindowState.Maximized;
                        Settings.MainWindow.UseNoneWindowStyle = true;
                    }
                    else
                    {
                        Settings.MainWindow.UseNoneWindowStyle = false;
                        Settings.MainWindow.WindowState = WindowState.Normal;
                        Settings.MainWindow.IgnoreTaskbarOnMaximize = false;

                        MyMainModel.ShowTitleBar = true;
                    }
                });
            }
        }
    }
}
