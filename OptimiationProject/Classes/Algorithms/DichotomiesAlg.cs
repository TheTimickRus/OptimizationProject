using org.mariuszgromada.math.mxparser;
using System;

namespace OptimiationProject.Classes.Algorithms
{
    internal class DichotomiesAlg : IDisposable
    {
        public DichotomiesAlg(string funcStr, double lowerLimit, double upperLimit, double delta, double eps)
        {
            Str += $"Заданная функция: {funcStr}\n";
            Str += "\n===================================\n\n";

            Exp = new Expression("f(x)", new Function($"f(x) = { funcStr }"));
            Exp.addArguments(new Argument("x"));

            LowerLimit = lowerLimit;
            UpperLimit = upperLimit;
            Delta = delta;
            Eps = eps;
        }
        public void Dispose() { }


        public Expression Exp { get; set; }
        private double Func(double x)
        {
            Exp.setArgumentValue("x", x);
            return Exp.calculate();
        }

        private int SignsCount { get; set; }
        private double Round(double x)
        {
            return Math.Round(x, SignsCount);
        }


        public string Str { get; set; }

        public double Delta { get; set; }
        public double Eps { get; set; }

        private int Count { get; set; }

        private double CurrentX { get; set; }
        private double CurrentY { get; set; }

        public double LowerLimit { get; set; }
        public double UpperLimit { get; set; }

        public double StarX { get; set; }
        public double FuncStarX { get; set; }

        public void MainWorking()
        {
            SignsCount = Convert.ToInt32(MainModel.Instanse.RoundSliderValue);

            Count = 0;

            Str += $"Итерация {Count} | Действие 1: \n";
            Str += $"\tЗаданные значения: LowerLimit = {LowerLimit}, UpperLimit = {UpperLimit}, Step = {Delta}, Eps = {Eps}\n";

            while (Math.Abs(UpperLimit - LowerLimit) > Eps)
            {
                var prevEps = Math.Abs(UpperLimit - LowerLimit);

                CurrentX = (LowerLimit + UpperLimit - Delta) / 2;
                CurrentY = (LowerLimit + UpperLimit + Delta) / 2;
                var fFromX = Func(CurrentX);
                var fFromY = Func(CurrentY);

                Str += "\n***********************************\n\n";
                Str += $"Итерация {Count} | Действие 3: \n";
                Str += $"\tCurrentX = {Math.Round(CurrentX, SignsCount)}, CurrentY = {Math.Round(CurrentY, SignsCount)}\n";
                Str += $"\tF(CurrentX) = {Math.Round(fFromX, SignsCount)}, F(CurrentY) = {Math.Round(fFromY, SignsCount)}\n";

                Str += $"Итерация {Count} | Действие 4: \n";

                if (fFromX > fFromY)
                {
                    LowerLimit = CurrentX;

                    Str += $"\tF(CurrentX) > F(CurrentY): \n\t\tLowerLimit = {Math.Round(CurrentX, SignsCount)}\n";
                }
                else
                {
                    UpperLimit = CurrentY;

                    Str += $"\tF(CurrentX) < F(CurrentY): \n\t\tUpperLimit = {Math.Round(CurrentY, SignsCount)}\n";
                }

                var el = Math.Abs(UpperLimit - LowerLimit);

                Str += $"Итерация {Count} | Действие 5: \n";
                Str += $"\tL = { el } > { Eps } --> { el > Eps }\n";

                Count++;

                if (prevEps.Equals(el))
                {
                    break;
                }
            }

            Str += "\n===================================\n\n";

            StarX = (LowerLimit + UpperLimit) / 2;
            FuncStarX = Func(StarX);

            Str += $"X* = { Round(StarX) }\nF(X*) = { Round(FuncStarX) }";
        }
    }
}
