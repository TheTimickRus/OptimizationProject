using OptimiationProject.Classes.Algorithms;
using OptimiationProject.Classes.Dialogs;
using OptimiationProject.Classes.Ocher;
using OptimiationProject.Models;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro.Controls.Dialogs;
using OptimiationProject.Classes.Settings;

namespace OptimiationProject.ViewModels
{
    public class GoldenSelectionViewModel : ViewModelBase
    {
        private GoldenSelectionModel _myGoldenSelectionModel;
        public GoldenSelectionModel MyGoldenSelectionModel
        {
            get => _myGoldenSelectionModel;
            set
            {
                _myGoldenSelectionModel = value;
                RaisePropertyChanged();
            }
        }

        private bool CheckParam()
        {
            return CheckFuncStr.CheckFunc(MyGoldenSelectionModel.FuncStr)
                   && MyGoldenSelectionModel.LowerLimit != null
                   && MyGoldenSelectionModel.UpperLimit != null
                   && MyGoldenSelectionModel.EpsValue != null;
        }

        public GoldenSelectionViewModel()
        {
            MyGoldenSelectionModel = GoldenSelectionModel.Instanse;
            MyGoldenSelectionModel.PropertyChanged += (sender, args) =>
                MainSettings.Instanse.MyGoldenSelectionModel = MyGoldenSelectionModel;
        }

        public ICommand BCheckFuncStr
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MetroDialogs.MainWindowMd("Проверка...", CheckFuncStr.CheckFunc(MyGoldenSelectionModel.FuncStr)
                        ? "Функция введена верно!"
                        : "Функция введена с ошибкой!");
                }, () => MyGoldenSelectionModel.FuncStr != null && !MyGoldenSelectionModel.FuncStr.Equals(string.Empty));
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
                        SaveInFile.Save(MyGoldenSelectionModel.ResultStr, "Золотое сечение", "GoldenSelectionResult");
                    }
                    catch (Exception ex)
                    {
                        MetroDialogs.MainWindowMd("Ошибка!", ex.Message);
                    }
                }, () => MyGoldenSelectionModel.ResultStr != null && !MyGoldenSelectionModel.ResultStr.Equals(string.Empty));
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
                            CalcErrors.GetError("Для метода Золотого сечения", _receivedValue));
                    }
                    catch (Exception ex)
                    {
                        MetroDialogs.MainWindowMd("Ошибка!", ex.Message);
                    }
                }, () => MyGoldenSelectionModel.ResultStr != null && !MyGoldenSelectionModel.ResultStr.Equals(string.Empty));
            }
        }

        public ICommand BClearCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MyGoldenSelectionModel.LowerLimit = null;
                    MyGoldenSelectionModel.UpperLimit = null;

                    MyGoldenSelectionModel.EpsValue = null;

                    MyGoldenSelectionModel.ResultStr = "";
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
                        var lowerLimit = MyGoldenSelectionModel.LowerLimit ?? 0;
                        var upperLimit = MyGoldenSelectionModel.UpperLimit ?? 0;
                        var epsValue = MyGoldenSelectionModel.EpsValue ?? 0;

                        using (var alg = new GoldenSelectionAlg(MyGoldenSelectionModel.FuncStr, lowerLimit, upperLimit, epsValue))
                        {
                            await Task.Factory.StartNew(() => alg.MainWorking());
                            MyGoldenSelectionModel.ResultStr = MainModel.Instanse.NumberSeparator.Equals(0) ? alg.Str : alg.Str.Replace(",", ".");

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
