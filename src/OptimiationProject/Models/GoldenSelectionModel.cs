using System.Runtime.Serialization;

namespace OptimiationProject.Models
{
    public class GoldenSelectionModel : BaseModel
    {
        public static GoldenSelectionModel Instanse;

        private double? _lowerLimit;
        [DataMember]
        public double? LowerLimit
        {
            get => _lowerLimit;
            set
            {
                _lowerLimit = value;
                OnPropertyChanged();
            }
        }

        private double? _upperLimit;
        [DataMember]
        public double? UpperLimit
        {
            get => _upperLimit;
            set
            {
                _upperLimit = value;
                OnPropertyChanged();
            }
        }

        private double? _epsValue;
        [DataMember]
        public double? EpsValue
        {
            get => _epsValue;
            set
            {
                _epsValue = value;
                OnPropertyChanged();
            }
        }
    }
}
