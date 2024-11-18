using IGasStation2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGasStation2.Utils
{
    public class PowerUsingPredicter
    {
        public PowerUsingPredicter()
        {

        }

        private class LandedPowerUsing
        {
            public GasStationPowerUsing PowerUsing { get; set; }
            public double L { get; set; }
            public double T { get; set; }
        }

        public List<GasStationPowerUsing> Predict(List<GasStationPowerUsing> gasStationPowerUsings, int yearsCount)
        {
            Dictionary<int, int> originPowerUsing = gasStationPowerUsings.ToDictionary(x => x.Year, x => x.PowerUsing);

            Dictionary<(double k, double b), List<LandedPowerUsing>> landingedPowerUsingsByKAndB = new();

            for (double k = 0.1; k <= 1; k += 0.1)
            {
                for (double b = 0.1; b <= 1; b += 0.1)
                {
                    List<LandedPowerUsing> gasStationPowerUsingL = new(gasStationPowerUsings.Count);

                    double prevL = gasStationPowerUsings.First().PowerUsing;
                    double prevT = 0;

                    gasStationPowerUsingL.Add(
                        new()
                        {
                            PowerUsing = gasStationPowerUsings.First(),
                            L = prevL,
                            T = prevT
                        }
                    );


                    for (int i = 1; i < gasStationPowerUsings.Count; i++)
                    {
                        double l = k * gasStationPowerUsings[i].PowerUsing + (1 - k) * (prevL + prevT);
                        double t = b * (l - prevL) + (1 - b) * prevT;

                        gasStationPowerUsingL.Add(
                            new()
                            {
                                PowerUsing = new GasStationPowerUsing()
                                {
                                    Year = gasStationPowerUsings[i].Year,
                                    PowerUsing = (int)l
                                },
                                L = l,
                                T = t
                            }
                        );

                        prevL = l;
                        prevT = t;
                    }

                    landingedPowerUsingsByKAndB.Add(
                        (k, b),
                        gasStationPowerUsingL
                    );
                }
            }

            double maxAcc = -100;
            (double k, double b) maxAccKAndB = default;

            foreach (var landed in landingedPowerUsingsByKAndB)
            {
                var pUsings = landed.Value;

                List<GasStationPowerUsing> predictedUsings = new(gasStationPowerUsings.Count);

                predictedUsings.Add(pUsings[0].PowerUsing);

                for (int i = 1; i < pUsings.Count; i++)
                {
                    predictedUsings.Add(new()
                    {
                        Year = pUsings[i].PowerUsing.Year,
                        PowerUsing = (int)(pUsings[i - 1].L + pUsings[i - 1].T)
                    }
                    );
                }

                var mistakes = predictedUsings.Select(
                    x =>
                        Math.Pow(x.PowerUsing - originPowerUsing[x.Year], 2)
                        /
                        Math.Pow(originPowerUsing[x.Year], 2)
                );


                double acc = 1 - mistakes.Average();

                if (acc > maxAcc)
                {
                    maxAcc = acc;
                    maxAccKAndB = landed.Key;
                }
            }

            var neededList = landingedPowerUsingsByKAndB[maxAccKAndB];
            double lastL = neededList.Last().L;
            double lastT = neededList.Last().T;
            int lastYear = neededList.Last().PowerUsing.Year;

            for (int p = 1; p <= yearsCount; p++)
            {
                int predictedPowerUsing = (int)(lastL + p * lastT);

                neededList.Add(
                    new()
                    {
                        PowerUsing = new()
                        {
                            Year = lastYear + p,
                            PowerUsing = predictedPowerUsing
                        }
                    }
                );
            }

            return neededList.Select(x => x.PowerUsing).ToList();
        }
    }
}
