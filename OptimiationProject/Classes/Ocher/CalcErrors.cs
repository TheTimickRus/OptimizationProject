using MahApps.Metro.Controls.Dialogs;
using OptimiationProject.Classes.Settings;
using System;
using System.Threading.Tasks;

namespace OptimiationProject.Classes.Ocher
{
    public static class CalcErrors
    {
        public static string GetError(string algName, double receivedValue)
        {
            var exactlyValue = MainSettings.Instanse.MyMainModel.ExValue;

            var absoluteError = exactlyValue - receivedValue;
            var relativeError = absoluteError / exactlyValue * 100;

            var signCount = (int)MainSettings.Instanse.MyMainModel.RoundSliderValue;

            var str = $"{algName}:\n" +
                      $"Точное: {Math.Round(exactlyValue, signCount)} | Полученное: {Math.Round(receivedValue, signCount)}\n" +
                      $"Абсолютная погрешность: {Math.Abs(Math.Round(absoluteError, signCount))}\n" +
                      $"Относительная погрешность: {Math.Abs(Math.Round(relativeError, signCount))}";

            return str;
        }
    }
}
