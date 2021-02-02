using OptimiationProject.Classes.Algorithms;
using OptimiationProject.Windows.Graph.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace OptimiationProject.Windows.Graph.Classes
{
    public static class ParabolasGraphWorking
    {
        public static void MainWorking(GraphModel graphModel)
        {
            var paramsMain = new List<double>
            {
                graphModel.ParamOne ?? throw new Exception("Ошибка! Один из параметров NULL!"),
                graphModel.ParamTwo ?? throw new Exception("Ошибка! Один из параметров NULL!"),
                graphModel.ParamThree ?? throw new Exception("Ошибка! Один из параметров NULL!"),
                graphModel.ParamFour ?? throw new Exception("Ошибка! Один из параметров NULL!")
            };

            var paramsDynamic = new List<double>
            {
                graphModel.IntervalParamOne ?? throw new Exception("Ошибка! Один из параметров NULL!"),
                graphModel.IntervalParamTwo ?? throw new Exception("Ошибка! Один из параметров NULL!")
            };

            var step = (paramsDynamic[1] - paramsDynamic[0]) / graphModel.IterationCount;
            var current = paramsDynamic[0];

            var xPoint = new List<double>();
            var yPoint = new List<double>();

            for (var i = 0; i < graphModel.IterationCount; i++)
            {
                var alg = new ParabolasAlg(graphModel.FuncStr, paramsMain[1],
                                                               paramsMain[0],
                                                               paramsMain[2],
                                                               paramsMain[3]);

                switch (graphModel.ChangedParam)
                {
                    case 0:
                        alg.X1 = current; alg.MainWorking();

                        if (alg.FuncStarX.Equals(0))
                        {
                            continue;
                        }

                        xPoint.Add(alg.X1);
                        break;

                    case 1:
                        alg.DeltaX = current; alg.MainWorking();

                        if (alg.FuncStarX.Equals(0))
                        {
                            continue;
                        }

                        xPoint.Add(alg.DeltaX);
                        break;

                    case 2:
                        alg.OneEps = current; alg.MainWorking();

                        if (alg.FuncStarX.Equals(0))
                        {
                            continue;
                        }

                        xPoint.Add(alg.OneEps);
                        break;

                    case 3:
                        alg.TwoEps = current; alg.MainWorking();

                        if (alg.FuncStarX.Equals(0))
                        {
                            continue;
                        }

                        xPoint.Add(alg.TwoEps);
                        break;
                }

                yPoint.Add(alg.FuncStarX);
                current += step;
            }

            if (xPoint.Count == 0 || yPoint.Count == 0 || xPoint.TrueForAll(d => d.Equals(xPoint[0])) || yPoint.TrueForAll(d => d.Equals(yPoint[0])))
            {
                throw new Exception("Массив точек пуст, или не изменяется!\n" +
                                    $"yPoint[0] = {yPoint[0]}\nyPoint[{yPoint.Count}] = {yPoint[yPoint.Count - 1]}");
            }

            File.WriteAllLines("values.txt", xPoint.Select((t, i) => $"{t};{yPoint[i]}"));
            Process.Start("PlotGraph.exe");
        }
    }
}
