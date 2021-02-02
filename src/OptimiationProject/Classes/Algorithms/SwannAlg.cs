using System;
using System.Collections.Generic;
using org.mariuszgromada.math.mxparser;

namespace OptimiationProject.Classes.Algorithms
{
    public class SwannAlg : IDisposable
    {
        public SwannAlg(string funcStr, double startValue = 0, double step = 0) 
        {
            Str += $"Заданна функция: {funcStr.Trim().ToLower()}\n";
            Str += "\n===================================\n\n";

            Exp = new Expression("f(x)", new Function($"f(x) = { funcStr.Trim().ToLower() }"));
            Exp.addArguments(new Argument("x"));

            StartValue = startValue;
            Step = step;  
        }
        public void Dispose() { }

        public double LowerLimit { get; set; }
        public double UpperLimit { get; set; }

        public double StartValue { get; set; }
        public double Step { get; set; }

        public string Str { get; set; }


        private int Count { get; set; }
        private double Delta { get; set; }

        private int Iteration { get; set; }

        private double CurrentValue { get; set; }
        private double NextValue { get; set; }


        public Expression Exp { get; set; }
        private double Func(double x)
        { 
            Exp.setArgumentValue("x", x);
            return Exp.calculate();
        }


        private int SignsCount { get; set; }

        private int FirstCondition()
        {
            var values = new List<double>
            {
                Func(StartValue - Step),
                Func(StartValue),
                Func(StartValue + Step)
            };

            Str += $"Итерация {Iteration} | Действие 2: \n";
            Str += $"\t[{Math.Round(values[0], SignsCount)}; {Math.Round(values[1], SignsCount)}; {Math.Round(values[2], SignsCount)}]\n";

            if (values[0] <= values[1] && values[1] >= values[2])
            {
                Str += $"Итерация {Iteration} | Действие 3: \n";
                Str += "\tF(x0 - Step) <= F(x0) >= F(x0 + Step) => Интервал не может быть найден! (Ошибка -1)\n";
                return -1;
            }

            if (values[0] >= values[1] && values[1] <= values[2])
            {
                LowerLimit = StartValue - Step;
                UpperLimit = StartValue + Step;

                Str += $"Итерация {Iteration} | Действие 3: \n";
                Str += "\tF(x0 - Step) >= F(x0) <= F(x0 + Step) => Поиск завершен!\n";
                Str += "\n===================================\n\n";
                return 0;
            }

            if (values[0] >= values[1] && values[1] >= values[2])
            {
                Count++;
                Delta = Step;
                LowerLimit = StartValue;
                CurrentValue = StartValue + Step;

                Str += $"Итерация {Iteration} | Действие 4: \n";
                Str += "\tF(x0 - Step) >= F(x0) >= F(x0 + Step):\n";
                Str += $"\t\tCount = {Count}\n\t\tDelta = {Delta}\n\t\tLowerLimit = {LowerLimit}\n\t\tCurrentValue = {CurrentValue}\n";
            }
            else if (values[0] <= values[1] && values[1] <= values[2])
            {
                Count++;
                Delta = -Step;
                UpperLimit = StartValue;
                CurrentValue = StartValue - Step;

                Str += $"Итерация {Iteration} | Действие 4: \n";
                Str += "\tF(x0 - Step) <= F(x0) <= F(x0 + Step):\n";
                Str += $"\t\tCount = {Count}\n\t\tDelta = {Delta}\n\t\tUpperLimit = {LowerLimit}\n\t\tCurrentValue = {CurrentValue}\n";
            }

            return 1;
        }
        private int SecondCondition()
        {
            var values = new List<double>()
            {
                Func(CurrentValue),
                Func(NextValue)
            };

            Str += $"Итерация {Iteration} | Действие 6: \n";
            Str += $"\t[CurrentValue = {Math.Round(values[0], SignsCount)}; NextValue = {Math.Round(values[1], SignsCount)}]\n";

            if (values[0] > values[1])
            {
                if (Delta.Equals(Step))
                {
                    LowerLimit = CurrentValue;

                    Str += "\tF(CurrentValue) > F(NextValue) && Step == Step:\n";
                    Str += $"\t\tLowerLimit = {CurrentValue}\n\t\tCount = {Count}\n";
                }
                else
                {
                    UpperLimit = CurrentValue;

                    Str += "\tF(CurrentValue) > F(NextValue) && Step == -Step:\n";
                    Str += $"\t\tUpperLimit = {CurrentValue}\n\t\tCount = {Count}\n";
                }

                Count++;
                return -1;
            }

            if (values[0] < values[1])
            {
                if (Delta.Equals(Step))
                {
                    UpperLimit = NextValue;

                    Str += "\tF(CurrentValue) < F(NextValue) && Step == Step:\n";
                    Str += $"\t\tUpperLimit = {UpperLimit}\n";
                }
                else
                {
                    LowerLimit = NextValue;

                    Str += "\tF(CurrentValue) < F(NextValue) && Step != -Step:\n";
                    Str += $"\t\tLowerLimit = {LowerLimit}\n";
                }

                return 0;
            }

            return 1;
        }


        public int MainWorking()
        {
            SignsCount = Convert.ToInt32(MainModel.Instanse.RoundSliderValue);

            Str += $"Итерация {Iteration} | Действие 1: \n";
            Str += $"\tЗаданные значения: x0 = {StartValue}, Step = {Step}, Count = {Count}\n";
            
            switch (FirstCondition())
            {
                case -1:
                {
                    return -1;
                }

                case 1:
                {
                    break;
                }

                default:
                {
                    return 0;
                }
            }

            while (true)
            {
                NextValue = CurrentValue + Math.Pow(2, Count) * Delta;

                Str += $"Итерация {Iteration} | Действие 5: \n";
                Str += $"\tNextValue = {NextValue}\n";

                switch (SecondCondition())
                {
                    case -1:
                    {
                        break;
                    }

                    case 1:
                    {
                        break;
                    }

                    default:
                    {
                        Str += "\n===================================\n\n"; 
                        return 0;
                    }
                }

                CurrentValue = NextValue;
                Iteration++;

                Str += "\n***********************************\n\n";
            }
        }
    }
}
