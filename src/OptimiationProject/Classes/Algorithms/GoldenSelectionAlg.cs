using System;
using org.mariuszgromada.math.mxparser;

namespace OptimiationProject.Classes.Algorithms
{
    internal class GoldenSelectionAlg : IDisposable
    {
        public GoldenSelectionAlg(string funcStr, double lowerLimit, double upperLimit, double eps)
        {
            Str += $"Заданная функция: {funcStr}\n";
            Str += "\n===================================\n\n";

            Exp = new Expression("f(x)", new Function($"f(x) = { funcStr }"));
            Exp.addArguments(new Argument("x"));

            LowerLimit = lowerLimit;
            UpperLimit = upperLimit;
            Eps = eps;
        }

        public void Dispose() { }

        public Expression Exp { get; set; }
        private double Func(double x)
        {
            Exp.setArgumentValue("x", x);
            return Exp.calculate();
        }


        public string Str { get; set; }
        public double LowerLimit { get; set; }
        public double UpperLimit { get; set; }
        public double Eps { get; set; }
        public double StarX { get; set; }
        public double FuncStarX { get; set; }

        private int Count { get; set; }
        private double CurrentY { get; set; }
        private double CurrentZ { get; set; }

        private int SignsCount { get; set; }
       
        public void MainWorking()
        {
            SignsCount = Convert.ToInt32(MainModel.Instanse.RoundSliderValue);

            Count = 0;

            Str += $"Итерация {Count} | Действие 1: \n";
            Str += $"\tЗаданные значения: LowerLimit = {LowerLimit}, UpperLimit = {UpperLimit}, Eps = {Eps}\n";

            CurrentY = LowerLimit + (3 - Math.Sqrt(5)) / 2 * (UpperLimit - LowerLimit);
            CurrentZ = LowerLimit + UpperLimit - CurrentY;

            Str += $"Итерация {Count} | Действие 3: \n";
            Str += $"\tCurrentY = {Math.Round(CurrentY, SignsCount)}, CurrentZ = {Math.Round(CurrentZ, SignsCount)}\n";
            Str += "\n***********************************\n\n";

            while (Math.Abs(LowerLimit - UpperLimit) > Eps)
            {
                var fFromY = Func(CurrentY);
                var fFromZ = Func(CurrentZ);

                Str += $"Итерация {Count} | Действие 4: \n";
                Str += $"\tF(Y) = {Math.Round(fFromY, SignsCount)}, F(Z) = {Math.Round(fFromZ, SignsCount)}\n";

                if (fFromY <= fFromZ)
                {
                    UpperLimit = CurrentZ;

                    CurrentZ = CurrentY;
                    CurrentY = LowerLimit + UpperLimit - CurrentY;

                    Str += $"Итерация {Count} | Действие 5: \n";
                    Str += $"\tF(Y) <= F(Z): \n\t\tUpperLimit = {Math.Round(UpperLimit, SignsCount)}\n\t\tCurrentY = {Math.Round(CurrentY, SignsCount)}\n\t\tCurrentZ = {Math.Round(CurrentZ, SignsCount)}\n";
                }
                else if (fFromY > fFromZ)
                {
                    LowerLimit = CurrentY;

                    CurrentY = CurrentZ;
                    CurrentZ = LowerLimit + UpperLimit - CurrentZ;

                    Str += $"Итерация {Count} | Действие 5: \n";
                    Str += $"\tF(Y) > F(Z): \n\t\tLowerLimit = {Math.Round(LowerLimit, SignsCount)}\n\t\tCurrentY = {Math.Round(CurrentY, SignsCount)}\n\t\tCurrentZ = {Math.Round(CurrentZ, SignsCount)}\n";
                }

                Str += $"Итерация {Count} | Действие 6: \n";

                var delta = Math.Abs(LowerLimit - UpperLimit);
                Str += $"\tDelta = {Math.Round(delta, SignsCount)} => ";
                if (delta > Eps)
                {
                    Count++;
                    Str += "Step > Eps => Переходим к шагу 4\n";
                }
                else
                {
                    Str += "Step < Eps => Работа алгоритма завершена!\n";
                }

                Str += "\n***********************************\n\n";
            }

            StarX = (LowerLimit + UpperLimit) / 2;
            FuncStarX = Func(StarX);
            Str += $"X* = {Math.Round(StarX, SignsCount)}\nF(X*) = {Math.Round(FuncStarX, SignsCount)}";
        }
    }
}
