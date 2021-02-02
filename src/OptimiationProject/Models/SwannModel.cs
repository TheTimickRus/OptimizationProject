using System.Runtime.Serialization;

namespace OptimiationProject.Models
{
    [DataContract]
    public class SwannModel : BaseModel
    {
        public static SwannModel Instanse { get; set; }

        private double? _startValue;
        [DataMember]
        public double? StartValue
        {
            get => _startValue;
            set
            {
                _startValue = value;
                OnPropertyChanged();
            }
        }

        private double? _stepValue;
        [DataMember]
        public double? StepValue
        {
            get => _stepValue;
            set
            {
                _stepValue = value;
                OnPropertyChanged();
            }
        }
    }
}
