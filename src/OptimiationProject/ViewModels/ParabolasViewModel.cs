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
    public class ParabolasViewModel : ViewModelBase
    {
        private ParabolasModel _myParabolasModel;
        public ParabolasModel MyParabolasModel
        {
            get => _myParabolasModel;
            set
            {
                _myParabolasModel = value;
                RaisePropertyChanged();
            }
        }

        private bool CheckParam()
        {
            return CheckFuncStr.CheckFunc(MyParabolasModel.FuncStr)
                   && MyParabolasModel.DeltaX != null
                   && MyParabolasModel.X1 != null
                   && MyParabolasModel.OneEps != null
                   && MyParabolasModel.TwoEps != null;
        }

        public ParabolasViewModel()
        {
            MyParabolasModel = ParabolasModel.Instanse;
            MyParabolasModel.PropertyChanged +=
                (sender, args) => MainSettings.Instanse.MyParabolasModel = MyParabolasModel;
        }

        public ICommand BCheckFuncStr
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MetroDialogs.MainWindowMd("Проверка...", CheckFuncStr.CheckFunc(MyParabolasModel.FuncStr)
                        ? "Функция введена верно!"
                        : "Функция введена с ошибкой!");
                }, () => MyParabolasModel.FuncStr != null && !MyParabolasModel.FuncStr.Equals(string.Empty));
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
                        SaveInFile.Save(MyParabolasModel.ResultStr, "Метод Парабол", "ParabolasResult");
                    }
                    catch (Exception ex)
                    {
                        MetroDialogs.MainWindowMd("Ошибка!", ex.Message);
                    }
                }, ()=> MyParabolasModel.ResultStr != null && !MyParabolasModel.ResultStr.Equals(string.Empty));
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
                            CalcErrors.GetError("Для метода Парабол", _receivedValue));
                    }
                    catch (Exception ex)
                    {
                        MetroDialogs.MainWindowMd("Ошибка!", ex.Message);
                    }
                }, () => MyParabolasModel.ResultStr != null && !MyParabolasModel.ResultStr.Equals(string.Empty));
            }
        }

        public ICommand BClearCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MyParabolasModel.DeltaX = null;
                    MyParabolasModel.X1 = null;

                    MyParabolasModel.OneEps = null;
                    MyParabolasModel.TwoEps = null;

                    MyParabolasModel.ResultStr = "";
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
                        var deltaX = MyParabolasModel.DeltaX ?? 0;
                        var x1 = MyParabolasModel.X1 ?? 0;
                        var oneEps = MyParabolasModel.OneEps ?? 0;
                        var twoEps = MyParabolasModel.OneEps ?? 0;

                        using (var alg = new ParabolasAlg(MyParabolasModel.FuncStr, deltaX, x1, oneEps, twoEps))
                        {
                            await Task.Factory.StartNew(() => alg.MainWorking());
                            MyParabolasModel.ResultStr = $"X* = { alg.StarX }; F(X*) = { alg.FuncStarX }; Iterations = {alg.IterationCount}";

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
