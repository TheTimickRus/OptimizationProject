using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OptimiationProject.Classes.Dialogs;
using OptimiationProject.Classes.Ocher;
using OptimiationProject.Classes.Settings;
using OptimiationProject.Windows.Graph.Classes;
using OptimiationProject.Windows.Graph.Model;
using System;
using System.Windows.Input;

namespace OptimiationProject.Windows.Graph.ViewModel
{
    public class GraphViewModel : ViewModelBase
    {
        private GraphModel _myGraphModel;
        public GraphModel MyGraphModel
        {
            get => _myGraphModel;
            set
            {
                _myGraphModel = value;
                RaisePropertyChanged();
            }
        }

        private bool CheckParam()
        {
            return CheckFuncStr.CheckFunc(MyGraphModel.FuncStr)
                   && MyGraphModel.ParamOne != null
                   && MyGraphModel.ParamTwo != null
                   && MyGraphModel.ParamThree != null
                   && MyGraphModel.ParamFour != null
                   && MyGraphModel.IntervalParamOne != null
                   && MyGraphModel.IntervalParamTwo != null;
        }

        public GraphViewModel()
        {
            MyGraphModel = GraphModel.Instanse;
            MyGraphModel.PropertyChanged += (sender, args) => MainSettings.Instanse.MyGraphModel = MyGraphModel;
        }


        public ICommand BCheckFuncStr
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MetroDialogs.MainWindowGw("Проверка...", CheckFuncStr.CheckFunc(MyGraphModel.FuncStr)
                        ? "Функция введена верно!"
                        : "Функция введена с ошибкой!");
                }, () => MyGraphModel.FuncStr != null && !MyGraphModel.FuncStr.Equals(string.Empty));
            }
        }

        public ICommand BCreateGraphCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        switch (MyGraphModel.Method)
                        {
                            case 0:
                                DichotomiesGraphWorking.MainWorking(MyGraphModel);
                                break;

                            case 1:
                                GoldenSelectionGraphWorking.MainWorking(MyGraphModel);
                                break;

                            case 2:
                                ParabolasGraphWorking.MainWorking(MyGraphModel);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MetroDialogs.MainWindowGw("Ошибка!", ex.Message);
                    }
                }, CheckParam);
            }
        }
    }
}
