using System;
using System.Collections.Generic;
using System.Linq;
using StadisticCalculator.Models;

namespace StadisticCalculator.Services
{
    public class CentralTendencyMeasures : ICentralTendencyMeasures
    {
        private readonly Table _table;
        private readonly General _general;

        public CentralTendencyMeasures(General general, Table table)
        {
            _table = table;
            _general = general;
        }
        public double GetMedian()
        {
            try
            {
                double median = 0;
                double halfOfTotal = 0;

                if (_general.NumbersArray.Count() % 2 == 0)
                    halfOfTotal = _general.NumbersArray.Count() / 2;
                else
                    halfOfTotal = (_general.NumbersArray.Count() + 1) / 2;

                var totalAcumulatedFrequencies = _table.GetTotalAcumulatedFrequencies();

                double numberWanted = totalAcumulatedFrequencies.Where(n => n == halfOfTotal).FirstOrDefault();
                int intervalIndex = totalAcumulatedFrequencies.IndexOf(numberWanted);

                if (numberWanted != 0)
                {
                    var rightLimits = _table.GetArrayOfRightLimits(_table.GetIntervals());
                    median = rightLimits[intervalIndex];
                }
                else
                {
                    numberWanted = totalAcumulatedFrequencies.Where(n => n > halfOfTotal).FirstOrDefault();
                    var leftLimits = _table.GetArrayOfLeftLimits(_table.GetIntervals());
                    var absolutesFrequencies = _table.GetAbsolutesFrequency();
                    intervalIndex = totalAcumulatedFrequencies.IndexOf(numberWanted);

                    double inferiorLimit = 0;
                    double previousTotalAcumulatedFrequency = 0;
                    double absoluteFrequency = 0;

                    if (intervalIndex > 0)
                    {
                        inferiorLimit = leftLimits[intervalIndex];
                        previousTotalAcumulatedFrequency = totalAcumulatedFrequencies[intervalIndex - 1];
                        absoluteFrequency = absolutesFrequencies[intervalIndex];
                    }
                    else
                    {
                        inferiorLimit = leftLimits[intervalIndex];
                        previousTotalAcumulatedFrequency = 0;
                        absoluteFrequency = absolutesFrequencies[intervalIndex];
                    }

                    double amplitude = _table.GetAmplitude();

                    median = inferiorLimit + ((halfOfTotal - previousTotalAcumulatedFrequency) / absoluteFrequency) * amplitude;
                }
                return Math.Round(median, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double GetFashion()
        {
            try
            {
                var absolutesFrequencies = _table.GetAbsolutesFrequency();

                double maxAbsoluteFrequency = absolutesFrequencies.Max();
                int intervalIndex = absolutesFrequencies.LastIndexOf(maxAbsoluteFrequency);

                var leftLimits = _table.GetArrayOfLeftLimits(_table.GetIntervals());

                double leftLimit = 0;
                double previousAbsoluteFrequency = 0;
                double nextAbsoluteFrequency = 0;

                if (intervalIndex > 0 && (intervalIndex + 1 <= absolutesFrequencies.Count - 1))
                {
                    leftLimit = leftLimits[intervalIndex];
                    previousAbsoluteFrequency = absolutesFrequencies[intervalIndex - 1];
                    nextAbsoluteFrequency = absolutesFrequencies[intervalIndex + 1];
                }
                else
                {
                    leftLimit = leftLimits[intervalIndex];
                    previousAbsoluteFrequency = 0;
                    nextAbsoluteFrequency = absolutesFrequencies[intervalIndex + 1];
                }
                double amplitude = _table.GetAmplitude();

                double sustraction = maxAbsoluteFrequency - previousAbsoluteFrequency;
                double sustraction2 = maxAbsoluteFrequency - nextAbsoluteFrequency;

                double fashion = leftLimit + (sustraction / (sustraction + sustraction2)) * amplitude;

                return Math.Round(fashion, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double GetVariationCoefficent()
        {
            try
            {
                var standardDesviation = GetStandardDesviation();
                var arithmeticMedia = GetArithmeticMedia();

                double variationCoefficent = standardDesviation / arithmeticMedia;

                return Math.Round(variationCoefficent, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double GetStandardDesviation()
        {
            try
            {
                double variance = GetVariance();
                double standardDesviation = Math.Sqrt(variance);

                return Math.Round(standardDesviation, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double GetVariance()
        {
            try
            {
                var potences = GetPotences();
                List<double> multiplyingResults = new List<double>();
                var absoluteFrequencies = _table.GetAbsolutesFrequency();

                for (int i = 0; i < potences.Count; i++)
                {
                    var multiplyResult = absoluteFrequencies[i] * potences[i];
                    multiplyingResults.Add(multiplyResult);
                }

                double totalMultiplyResults = multiplyingResults.Sum();
                double variance = totalMultiplyResults / _general.NumbersArray.Length;

                return Math.Round(variance, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<double> GetPotences()
        {
            try
            {
                var classMarks = _table.GetClassMark();
                List<double> potences = new List<double>();

                var arithmeticMedia = GetArithmeticMedia();

                for (int i = 0; i < classMarks.Count; i++)
                {
                    double potence = Math.Pow((classMarks[i] - arithmeticMedia), 2);
                    potences.Add(potence);
                }
                return potences;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double GetArithmeticMedia()
        {
            try
            {
                var absolutesFrecuencies = _table.GetAbsolutesFrequency();
                var classMarks = _table.GetClassMark();

                List<double> productResults = new List<double>();

                double arithmeticMedia;

                for (int i = 0; i < classMarks.Count; i++)
                {
                    productResults.Add(absolutesFrecuencies[i] * classMarks[i]);
                }

                arithmeticMedia = productResults.Sum() / _general.NumbersArray.Count();

                return Math.Round(arithmeticMedia, 2);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
