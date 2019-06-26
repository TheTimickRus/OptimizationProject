using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using OptimiationProject.Classes.Algorithms;
using OptimiationProject.Classes.Dialogs;
using OptimiationProject.Classes.Ocher;
using OptimiationProject.Classes.Settings;
using OptimiationProject.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MahApps.Metro.Controls.Dialogs;

namespace OptimiationProject.ViewModels
{
    public class DichotomiesViewModel : ViewModelBase
    {
        private DichotomiesModel _myDichotomiesModel;
        public DichotomiesModel MyDichotomiesModel
        {
            get => _myDichotomiesModel;
            set
            {
                _myDichotomiesModel = value;
                RaisePropertyChanged();
            }
        }

        private bool CheckParam()
        {
            return CheckFuncStr.CheckFunc(MyDichotomiesModel.FuncStr)
                   && MyDichotomiesModel.LowerLimit != null
                   && MyDichotomiesModel.UpperLimit != null
                   && MyDichotomiesModel.StepValue != null
                   && MyDichotomiesModel.EpsValue != null;
        }

        public DichotomiesViewModel()
        {
            MyDichotomiesModel = DichotomiesModel.Instanse;
            MyDichotomiesModel.PropertyChanged += (sender, args) => 
                MainSettings.Instanse.MyDichotomiesModel = MyDichotomiesModel;
        }

        public ICommand BCheckFuncStr
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MetroDialogs.MainWindowMd("Проверка...", CheckFuncStr.CheckFunc(MyDichotomiesModel.FuncStr)
                        ? "Функция введена верно!"
                        : "Функция введена с ошибкой!");
                }, () => MyDichotomiesModel.FuncStr != null && !MyDichotomiesModel.FuncStr.Equals(string.Empty));
            }
        }

        public ICommand BSaveResultCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        SaveInFile.Save(MyDichotomiesModel.ResultStr, "Метод Дихотомии", "DichotomiesResult");
                    }
                    catch (Exception ex)
                    {
                        MetroDialogs.MainWindowMd("Ошибка!", ex.Message);
                    }
                }, () => MyDichotomiesModel.ResultStr != null && !MyDichotomiesModel.ResultStr.Equals(string.Empty));
            }
        }


        private double _receivedValue;
        public ICommand BCalcError
        {
            get
            {
                return new RelayCommand(() =>
                {
                    try
                    {
                        MetroDialogs.MainWindowMd("Погрешность", 
                            CalcErrors.GetError("Для метода Дихотомии", _receivedValue));
                    }
                    catch (Exception ex)
                    {
                        MetroDialogs.MainWindowMd("Ошибка!", ex.Message);
                    }
                }, () => MyDichotomiesModel.ResultStr != null && !MyDichotomiesModel.ResultStr.Equals(string.Empty));
            }
        }

        public ICommand BClearCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MyDichotomiesModel.LowerLimit = null;
                    MyDichotomiesModel.UpperLimit = null;

                    MyDichotomiesModel.StepValue = null;
                    MyDichotomiesModel.EpsValue = null;

                    MyDichotomiesModel.ResultStr = "";
                });
            }
        }

        public ICommand BStartCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    try
                    {
                        var lowerLimit = MyDichotomiesModel.LowerLimit ?? 0;
                        var upperLimit = MyDichotomiesModel.UpperLimit ?? 0;
                        var stepValue = MyDichotomiesModel.StepValue ?? 0;
                        var epsValue = MyDichotomiesModel.EpsValue ?? 0;

                        using (var alg = new DichotomiesAlg(MyDichotomiesModel.FuncStr, lowerLimit, upperLimit, stepValue, epsValue))
                        {
                            await Task.Factory.StartNew(() => alg.MainWorking());
                            MyDichotomiesModel.ResultStr = MainModel.Instanse.NumberSeparator.Equals(0) ? alg.Str : alg.Str.Replace(",", ".");

                            _receivedValue = alg.FuncStarX;
                        }
                    }
                    catch (Exception ex)
                    {
                        MetroDialogs.MainWindowMd("Ошибка!", ex.Message);
                    }
                }, CheckParam);
            }
        }
    }
}
