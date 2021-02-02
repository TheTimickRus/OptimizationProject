using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace OptimiationProject.Windows.Graph.Model
{
    public class GraphModel : INotifyPropertyChanged
    {
        public static GraphModel Instanse { get; set; }

        private int _method;
        [DataMember]
        public int Method
        {
            get => _method;
            set
            {
                _method = value;
                OnPropertyChanged();
            }
        }

        private string _funcStr;
        [DataMember]
        public string FuncStr
        {
            get => _funcStr;
            set
            {
                _funcStr = value;
                OnPropertyChanged();
            }
        }

        private double? _paramOne;
        private double? _paramTwo;
        private double? _paramThree;
        private double? _paramFour;

        [DataMember]
        public double? ParamOne
        {
            get => _paramOne;
            set
            {
                _paramOne = value;
                OnPropertyChanged();
            }
        }

        [DataMember]
        public double? ParamTwo
        {
            get => _paramTwo;
            set
            {
                _paramTwo = value;
                OnPropertyChanged();
            }
        }

        [DataMember]
        public double? ParamThree
        {
            get => _paramThree;
            set
            {
                _paramThree = value;
                OnPropertyChanged();
            }
        }

        [DataMember]
        public double? ParamFour
        {
            get => _paramFour;
            set
            {
                _paramFour = value;
                OnPropertyChanged();
            }
        }

        private int _changedParam;
        [DataMember]
        public int ChangedParam
        {
            get => _changedParam;
            set
            {
                _changedParam = value;
                OnPropertyChanged();
            }
        }

        private double? _intervalParamOne;
        private double? _intervalParamTwo;

        public double? IntervalParamOne
        {
            get => _intervalParamOne;
            set
            {
                _intervalParamOne = value;
                OnPropertyChanged();
            }
        }

        public double? IntervalParamTwo
        {
            get => _intervalParamTwo;
            set
            {
                _intervalParamTwo = value;
                OnPropertyChanged();
            }
        }

        private int _iterationCount;
        [DataMember]
        public int IterationCount
        {
            get => _iterationCount;
            set
            {
                _iterationCount = value;
                OnPropertyChanged();
            }
        }
        
        //===================================================

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
