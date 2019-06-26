using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OptimiationProject.Classes.Algorithms
{
    internal class ParabolasAlg : IDisposable
    {
        #region Sevice
        public ParabolasAlg(string funcStr, double deltaX, double x1, double oneEps, double twoEps)
        {
            Exp = new Expression("f(x)", new Function($"f(x) = { funcStr }"));
            Exp.addArguments(new Argument("x"));

            DeltaX = deltaX;
            X1 = x1;
            OneEps = oneEps;
            TwoEps = twoEps;
        }
        public void Dispose() { }
        #endregion

        #region Private Functions
        private Expression Exp { get; }
        private double Func(double x)
        {
            Exp.setArgumentValue("x", x);
            return Exp.calculate();
        }

        private static double Pow(double x)
        {
            return Math.Pow(x, 2);
        }
        private double GetPolinome(IReadOnlyList<double> valuesList)
        {
            return 0.5 * (((Pow(X2) - Pow(X3)) * valuesList[0] + (Pow(X3) - Pow(X1)) * valuesList[1] + (Pow(X1) - Pow(X2)) * valuesList[2]) / ((X2 - X3) * valuesList[0] + (X3 - X1) * valuesList[1] + (X1 - X2) * valuesList[2]));
        }

        private bool CondOne(double fMin, double fXPol)
        {
            return Math.Abs((fMin - fXPol) / fXPol) < OneEps;
        }

        private bool CondTwo(double xMin, double xPol)
        {
            return Math.Abs((xMin - xPol) / xPol) < TwoEps;
        }

        private static bool CheckInterval(double a, double b, double x)
        {
            return x >= a && x <= b || x <= a && x >= b;
        }
        #endregion

        #region Private Fields
        private double X2 { get; set; }
        private double X3 { get; set; }
        #endregion

        #region Public Fields
        public double StarX { get; private set; }
        public double FuncStarX { get; private set; }

        public double X1 { get; set; }
        public double DeltaX { get; set; }
        public double OneEps { get; set; }
        public double TwoEps { get; set; }

        public int IterationCount { get; private set; }
        public int IterationMax { get; set; }
        #endregion

        public void MainWorking()
        {
            IterationCount = 0;
            IterationMax = 200;

            while (IterationCount < IterationMax)
            {
                X2 = X1 + DeltaX;

                if (Func(X1) > Func(X2))
                {
                    X3 = X1 + 2 * DeltaX;
                }
                else
                {
                    X3 = X1 - DeltaX;
                }

                while (true)
                {
                    var valuesList = new List<double> { Func(X1), Func(X2), Func(X3) };
                    var keysList = new List<double> { X1, X2, X3 };

                    double fxMin = valuesList.Min(), 
                           xMin = keysList[valuesList.IndexOf(fxMin)], 
                           xPol = double.MinValue;

                    try
                    {
                        xPol = GetPolinome(valuesList);
                    }
                    catch
                    {
                        if (xPol.Equals(double.MinValue))
                        {
                            X1 = xMin;
                            break;
                        }
                    }

                    var fxPol = Func(xPol);

                    if (CondOne(fxMin, fxPol) && CondTwo(xMin, xPol))
                    {
                        StarX = xPol;
                        FuncStarX = Func(xPol);

                        return;
                    }

                    if (CheckInterval(X1, X3, xPol))
                    {
                        var newX2 = fxMin < fxPol ? xMin : xPol;

                        keysList.Sort();
                        
                        for (var i = 0; i < keysList.Count - 1; i++)
                        {
                            if (newX2 <= keysList[i + 1])
                            {
                                X1 = keysList[i];
                                X2 = newX2;
                                X3 = keysList[i + 1];

                                break;
                            }
                        }

                        continue;
                    }

                    X1 = xPol;
                    break;
                }

                IterationCount++;
            }
        }
    }
}
