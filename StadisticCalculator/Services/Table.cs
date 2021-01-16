using System;
using System.Collections.Generic;
using System.Linq;
using StadisticCalculator.Models;

namespace StadisticCalculator.Services
{
    public class Table : ITable
    {
        private readonly General _general;
        private readonly NumbersTools _numbersTools;

        public Table(General general)
        {
            _general = general;
        }

        public Table(General general, NumbersTools numbersTools)
        {
            _general = general;
            _numbersTools = numbersTools;
        }

        public double GetAmplitude()
        {
            try
            {
                if (_general.ClassInterval > 0)
                {
                    double amplitude = GetRange() / _general.ClassInterval;
                    return amplitude;
                }

                double classIntervalEstimated = 1 + 3.322 * Math.Log10(_general.NumbersArray.Length);

                double estimatedAmplitude = GetRange() / Math.Round(classIntervalEstimated);
                return estimatedAmplitude;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public double GetRange()
        {
            try
            {
                double[] convertedNumbers = _numbersTools.ConvertStringArrayIntoDoubleArray(_general.NumbersArray);
                double higher = convertedNumbers.Max();
                double less = convertedNumbers.Min();

                return higher - less;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<double> GetClassMark()
        {
            try
            {
                List<string> arrayOfIntervalsInString = new List<string>();
                arrayOfIntervalsInString = GetIntervals();
                
                var arrayOfLeftLimits = GetArrayOfLeftLimits(arrayOfIntervalsInString);
                var arrayOfRightLimits = GetArrayOfRightLimits(arrayOfIntervalsInString);

                List<double> classMarks = new List<double>();
                
                for(int i = 0; i < arrayOfRightLimits.Count; i++)
                {
                    double classMark = (arrayOfLeftLimits[i] + arrayOfRightLimits[i]) / 2;
                    classMarks.Add(Math.Ceiling(classMark));
                }
                return classMarks;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetIntervals()
        {
            try
            {
                double[] doubleNumbers = _numbersTools.ConvertStringArrayIntoDoubleArray(_general.NumbersArray);

                List<string> intervals = new List<string>();
                double amplitude;

                if (_general.ClassInterval > 0)
                    amplitude = Math.Round(GetAmplitude());
                else
                {
                    double estimatedClassInterval = 1 + 3.322 * Math.Log10(_general.NumbersArray.Length);
                    _general.ClassInterval = int.Parse(Math.Round(estimatedClassInterval).ToString());
                    amplitude = Math.Round(GetAmplitude());
                }


                for (int i = 0; i < _general.ClassInterval; i++)
                {
                    double initialLimit = doubleNumbers.Min();
                    double leftLimit = initialLimit + amplitude * (i + 1);

                    if (i == 0)
                    {
                        intervals.Add($"[{Math.Round(initialLimit)}-{Math.Round(leftLimit)})");
                    }
                    else
                    {
                        intervals.Add($"[{Math.Round(leftLimit - amplitude)}-{Math.Round(leftLimit)})");
                    }
                }
                return intervals;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<double> GetArrayOfLeftLimits(List<string> arrayOfIntervalsString)
        {
            List<double> arrayOfLeftLimits = new List<double>();
            for (int i = 0; i < arrayOfIntervalsString.Count; i++)
            {
                int startLeft = arrayOfIntervalsString[i].IndexOf('[');
                int endLeft = arrayOfIntervalsString[i].IndexOf('-', 0);
                string leftLimitInterval = arrayOfIntervalsString[i].Substring(startLeft, endLeft);

                arrayOfLeftLimits.Add(double.Parse(leftLimitInterval.Trim('[')));
            }
            return arrayOfLeftLimits;
        }

        public List<double> GetArrayOfRightLimits(List<string> arrayOfIntervalsString)
        {
            List<double> arrayOfRightLimits = new List<double>();
            for (int i = 0; i < arrayOfIntervalsString.Count; i++)
            {

                int startRight = arrayOfIntervalsString[i].IndexOf('-', 0);
                string rightLimitInterval = arrayOfIntervalsString[i].Substring(startRight);

                arrayOfRightLimits.Add(double.Parse(rightLimitInterval.Trim('-').Trim(')')));
            }
            return arrayOfRightLimits;
        }

        public List<double> GetRelativeFrequencies()
        {
            try
            {
                List<double> absolutesFrecuencies = new List<double>();

                absolutesFrecuencies = GetAbsolutesFrequency();

                List<double> relativeFrecuencies = new List<double>();

                for (int i = 0; i < absolutesFrecuencies.Count; i++)
                {
                    double relativeFrecuency = absolutesFrecuencies[i]/_general.NumbersArray.Count();

                    relativeFrecuencies.Add(Math.Round(relativeFrecuency, 3));
                }

                if(Math.Round(relativeFrecuencies.Sum()) != 1)
                {
                    throw new Exception("Los datos suplidos en el ejercicio son erróneos y pueden causar impresición en el cálculo, por favor revisa si todo está correcto e inténtalo de nuevo.");
                }

                return relativeFrecuencies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<double> GetTotalAcumulatedFrequencies()
        {
            try
            {
                List<double> absolutesFrecuencies = new List<double>();

                absolutesFrecuencies = GetAbsolutesFrequency();

                List<double> totalAcumulatedFrecuencies = new List<double>();

                totalAcumulatedFrecuencies.Add(absolutesFrecuencies[0]);

                for (int i = 1; i < absolutesFrecuencies.Count; i++)
                {
                    double totalAcumulatedFrecuency;

                    double actualNumber = absolutesFrecuencies[i];
                    double previousNumber = totalAcumulatedFrecuencies[i - 1];
                    totalAcumulatedFrecuency = actualNumber + previousNumber;
                    totalAcumulatedFrecuencies.Add(totalAcumulatedFrecuency);
                }

                return totalAcumulatedFrecuencies;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<double> GetAbsolutesFrequency()
        {
            try
            {
                var arrayOfNumbers = _numbersTools.ConvertStringArrayIntoDoubleArray(_general.NumbersArray);
                List<double> arrayOfLeftLimits = new List<double>();
                List<double> arrayOfRightLimits = new List<double>();
                
                arrayOfLeftLimits = GetArrayOfLeftLimits(GetIntervals());
                arrayOfRightLimits = GetArrayOfRightLimits(GetIntervals());

                List<double> absolutesFrequencies = new List<double>();

                for (int i = 0; i < arrayOfLeftLimits.Count; i++)
                {
                    double count = arrayOfNumbers.Where(n => n < arrayOfRightLimits[i] && n >= arrayOfLeftLimits[i]).Count();

                    if (i == 0)
                    {

                        count = arrayOfNumbers.Where(n => n < arrayOfRightLimits[i]).Count();
                        absolutesFrequencies.Add(count);
                    }
                    else if ((i + 1) == arrayOfLeftLimits.Count)
                    {
                        count = arrayOfNumbers.Where(n => n <= arrayOfRightLimits[i] && n >= arrayOfLeftLimits[i]).Count();
                        absolutesFrequencies.Add(count);
                    }
                    else
                    {
                        absolutesFrequencies.Add(count);
                    }
                }

                if(absolutesFrequencies.Sum() != _general.NumbersArray.Length)
                {
                    throw new Exception("Los datos suplidos en el ejercicio son erróneos y pueden causar impresición en el cálculo, por favor revisa si todo está correcto e inténtalo de nuevo.");
                }

                return absolutesFrequencies;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
