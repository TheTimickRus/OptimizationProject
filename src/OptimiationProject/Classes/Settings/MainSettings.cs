using System.Runtime.Serialization;
using OptimiationProject.Models;
using OptimiationProject.Windows.Graph.Model;

namespace OptimiationProject.Classes.Settings
{
    [DataContract]
    public class MainSettings
    {
        public static MainSettings Instanse;


        [DataMember]
        public MainModel MyMainModel { get; set; }
        
        [DataMember]
        public SwannModel MySwannModel { get; set; }

        [DataMember]
        public DichotomiesModel MyDichotomiesModel { get; set; }
        [DataMember]
        public GoldenSelectionModel MyGoldenSelectionModel { get; set; }
        [DataMember]
        public ParabolasModel MyParabolasModel { get; set; }
        
        [DataMember]
        public GraphModel MyGraphModel { get; set; }
    }
}
