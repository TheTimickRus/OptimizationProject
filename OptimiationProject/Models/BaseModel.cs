using OptimiationProject.Properties;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace OptimiationProject.Models
{
    [DataContract]
    public class BaseModel : INotifyPropertyChanged
    {
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
        private string _resultStr;
        [DataMember]
        public string ResultStr
        {
            get => _resultStr;
            set
            {
                _resultStr = value;
                OnPropertyChanged();
            }
        }

        //==================================================================

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
