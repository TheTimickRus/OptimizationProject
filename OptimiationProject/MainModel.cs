using MahApps.Metro;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Windows;

namespace OptimiationProject
{
    [DataContract]
    public class MainModel : INotifyPropertyChanged
    {
        public static MainModel Instanse;


        private string _currentBaseColor;
        public string CurrentBaseColor
        {
            get => _currentBaseColor;
            set
            {
                _currentBaseColor = value;

                ThemeManager.ChangeThemeBaseColor(Application.Current, _currentBaseColor);
                OnPropertyChanged();
            }
        }

        private ColorScheme _currentColor;
        public ColorScheme CurrentColor
        {
            get => _currentColor;
            set
            {
                _currentColor = value;

                ThemeManager.ChangeThemeColorScheme(Application.Current, _currentColor.Name);
                OnPropertyChanged();
            }
        }

        private int _currentBaseColorIndex;
        [DataMember]
        public int CurrentBaseColorIndex
        {
            get => _currentBaseColorIndex;
            set
            {
                _currentBaseColorIndex = value;
                OnPropertyChanged();
            }
        }

        private int _currentColorIndex;
        [DataMember]
        public int CurrentColorIndex
        {
            get => _currentColorIndex;
            set
            {
                _currentColorIndex = value;
                OnPropertyChanged();
            }
        }


        private bool _showTitleBar = true;
        [DataMember]
        public bool ShowTitleBar
        {
            get => _showTitleBar;
            set
            {
                _showTitleBar = value;
                OnPropertyChanged();
            }
        }

        private bool _showInTaskBar = true;
        [DataMember]
        public bool ShowInTaskBar
        {
            get => _showInTaskBar;
            set
            {
                _showInTaskBar = value;
                OnPropertyChanged();
            }
        }


        private bool _toggleFullScreen;
        [DataMember]
        public bool ToggleFullScreen
        {
            get => _toggleFullScreen;
            set
            {
                _toggleFullScreen = value;
                OnPropertyChanged();
            }
        }
        

        private bool _useAccentForDialogs;
        [DataMember]
        public bool UseAccentForDialogs
        {
            get => _useAccentForDialogs;
            set
            {
                _useAccentForDialogs = value;
                OnPropertyChanged();
            }
        }

        private double _roundSliderValue = 5;
        [DataMember]
        public double RoundSliderValue
        {
            get => _roundSliderValue;
            set
            {
                _roundSliderValue = value;
                OnPropertyChanged();
            }
        }

        private int _numberSeparator;
        [DataMember]
        public int NumberSeparator
        {
            get => _numberSeparator;
            set
            {
                _numberSeparator = value;
                OnPropertyChanged();
            }
        }

        private double _exValue;
        [DataMember]
        public double ExValue
        {
            get => _exValue;
            set
            {
                _exValue = value;
                OnPropertyChanged();
            }
        }

        //==================================================================

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
