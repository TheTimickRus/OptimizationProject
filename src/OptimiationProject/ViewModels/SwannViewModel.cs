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

namespace OptimiationProject.ViewModels
{
    public class SwannViewModel : ViewModelBase
    {
        private SwannModel _swannModel;
        public SwannModel MySwannModel
        {
            get => _swannModel;
            set
            {
                _swannModel = value;
                RaisePropertyChanged();
            }
        }

        public SwannViewModel()
        {
            MySwannModel = SwannModel.Instanse;
            MySwannModel.PropertyChanged += (sender, args) => 
                MainSettings.Instanse.MySwannModel = MySwannModel;
        }


        public ICommand BCheckFuncStr
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MetroDialogs.MainWindowMd("Проверка...", CheckFuncStr.CheckFunc(MySwannModel.FuncStr) 
                        ? "Функция введена верно!" 
                        : "Функция введена с ошибкой!");
                }, ()=> MySwannModel.FuncStr != null && !MySwannModel.FuncStr.Equals(string.Empty));
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
                        SaveInFile.Save(MySwannModel.ResultStr, "Алгоритм Свенна", "SwannResult");
                    }
                    catch (Exception ex)
                    {
                        MetroDialogs.MainWindowMd("Ошибка!", ex.Message);
                    }
                }, () => MySwannModel.ResultStr != null && !MySwannModel.ResultStr.Equals(string.Empty));
            }
        }

        public ICommand BClearCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MySwannModel.StartValue = null;
                    MySwannModel.StepValue = null;

                    MySwannModel.ResultStr = "";
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
                        var startValue = MySwannModel.StartValue ?? -1;
                        var stepValue = MySwannModel.StepValue ?? -1;

                        using (var alg = new SwannAlg(MySwannModel.FuncStr, startValue, stepValue))
                        {
                            MySwannModel.ResultStr = string.Empty;

                            var result = -1;

                            await Task.Factory.StartNew(() =>
                            {
                                result = alg.MainWorking();
                            });

                            if (result.Equals(0))
                            {
                                var str = alg.Str + $"Интервал найден! [{alg.LowerLimit}; {alg.UpperLimit}]";
                                MySwannModel.ResultStr = MainModel.Instanse.NumberSeparator.Equals(0) ? str : str.Replace(",", ".");
                            }
                            else
                            {
                                MySwannModel.ResultStr = MainModel.Instanse.NumberSeparator.Equals(0) ? alg.Str : alg.Str.Replace(",", ".");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MetroDialogs.MainWindowMd("Ошибка!", ex.Message);
                    }
                }, () => CheckFuncStr.CheckFunc(MySwannModel.FuncStr) && !(MySwannModel.StartValue == null || MySwannModel.StepValue == null));
            }
        }
    }
}
