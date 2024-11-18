using MathNet.Numerics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGasStation2.Utils
{
    public class NormalizeChecker
    {
        public NormalizeChecker() 
        {

        }

        private class Range
        {
            public double Start { get; set; }
            public double End { get; set; }
            public int CountValues { get; set; }
            public double Middle { get => (Start + End) / 2; }
        }

        private double PhiFunction(double arg)
        {
            if (arg < 0)
                return -PhiFunction(-arg);

            double firstMultiple = 1 / Math.Sqrt(2 * Math.PI);

            double integral = 0;
            double delta = 0.01;

            for (double x = 0; x <= arg; x += delta)
            {
                integral += Math.Exp(-x * x / 2) * delta;
            }

            return firstMultiple * integral;
        }

        public bool IsDataNormalized(List<int> items)
        {
            int r = 10;
            int n = items.Count();

            int max = items.Max();
            int min = items.Min();

            double h = (max - min) / (double)r;

            List<Range> ranges = new();

            for (double start = min; start < max; start += h)
                ranges.Add(
                    new()
                    {
                        Start = start,
                        End = start + h,
                        CountValues = items.Where(x => x >= start && x <= start + h).Count()
                    }
                );

            double c = ranges.MaxBy(x => x.CountValues).Middle;

            //На основе значений k_i * u_i
            double m1WithStar = ranges.Select(
                x => {
                    double uI = (x.Middle - c) / h;

                    double result = x.CountValues * uI;

                    return result;
                }
            )
            .Sum() / n;

            //На основе значений k_i * u_i^2
            double m2WithStar = ranges.Select(
                x => {
                    double uI = (x.Middle - c) / h;

                    double result = x.CountValues * uI * uI;

                    return result;
                }
            )
            .Sum() / n;

            double xViborochnayaAvg = m1WithStar * h + c;
            double avgOtklonenye = Math.Sqrt(
                (m2WithStar - m1WithStar * m1WithStar) * h * h
            );

            var teoreticheskyeChastoty = ranges.Select(
                    x => {
                        double t1I = (x.Start - xViborochnayaAvg) / avgOtklonenye;
                        double t2I = (x.End - xViborochnayaAvg) / avgOtklonenye;

                        double phiT1I = PhiFunction(t1I);
                        double phiT2I = PhiFunction(t2I);

                        double pI = phiT2I - phiT1I;

                        return (int)(n * pI);
                    }
                )
                .ToList();

            double accurancy = 0.9;
            double chiTeoretic = MathNet.Numerics.Distributions.ChiSquared.InvCDF(r - 3, accurancy);
            double chiFact = 0;
            for (int i = 0; i < ranges.Count(); i++)
                chiFact += (ranges[i].CountValues - teoreticheskyeChastoty[i]) * (ranges[i].CountValues - teoreticheskyeChastoty[i]) / (double)teoreticheskyeChastoty[i];

            return chiFact <= chiTeoretic;
        }

        /// <summary>
        /// Определяет данные, которые стоит удалить из БД
        /// </summary>
        /// <param name="items"></param>
        /// <returns>badDataList - список плохих данных, isHelp - поможет ли удаление данных нормализовать выборку (true если поможет)</returns>
        public (HashSet<int> badData, bool isHelp) BadData(List<int> items)
        {
            List<int> currentItems = new(items);

            HashSet<int> badData = new();
            bool wereScaled = false;

            do
            {
                double avg = currentItems.Average();
                int n = currentItems.Count;

                double avgOtklonenie = Math.Sqrt(currentItems.Sum(x => (x - avg) * (x - avg)) / (n - 1));

                int max = currentItems.Max();
                int min = currentItems.Min();

                double deltaMax = Math.Abs(avg - max);
                double deltaMin = Math.Abs(avg - min);

                double tMax = deltaMax / avgOtklonenie;
                double tMin = deltaMin / avgOtklonenie;

                //double tTeoretic = tMax - tMin;

                double mistakeProcent = 0.1;
                double tTable = StudentT.InvCDF(0, 1, n - 1, 1 - mistakeProcent / 2.0);

                if (tMax <= tTable && tMin <= tTable)
                    wereScaled = false;

                if (tMax > tTable)
                {
                    currentItems.RemoveAll(x => x == max);

                    badData.Add(max);

                    wereScaled = true;
                }
                if (tMin > tTable)
                {
                    currentItems.RemoveAll(x => x == min);

                    badData.Add(min);

                    wereScaled = true;
                }
            }
            while (wereScaled);

            bool isHelp = IsDataNormalized(currentItems);

            return (badData, isHelp);
        }
    }
}
