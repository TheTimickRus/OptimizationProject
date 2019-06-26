using System.Runtime.Serialization;

namespace OptimiationProject.Models
{
    public class ParabolasModel : BaseModel
    {
        public static ParabolasModel Instanse;

        private double? _deltaX;
        [DataMember]
        public double? DeltaX
        {
            get => _deltaX;
            set
            {
                _deltaX = value;
                OnPropertyChanged();
            }
        }

        private double? _x1;
        [DataMember]
        public double? X1
        {
            get => _x1;
            set
            {
                _x1 = value;
                OnPropertyChanged();
            }
        }

        private double? _oneEps;
        [DataMember]
        public double? OneEps
        {
            get => _oneEps;
            set
            {
                _oneEps = value;
                OnPropertyChanged();
            }
        }

        private double? _twoEps;
        [DataMember]
        public double? TwoEps
        {
            get => _twoEps;
            set
            {
                _twoEps = value;
                OnPropertyChanged();
            }
        }
    }
}
