using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StadisticCalculator.Models;

namespace StadisticCalculator.Services
{
    public class NumbersTools: INumbersTools
    {
        private readonly General _general;

        public NumbersTools(General general)
        {
            _general = general;
        }
       

        public string SortNumbers(bool isAscending)
        {
            try
            {
                double[] doubleNumbers = ConvertStringArrayIntoDoubleArray(_general.NumbersArray);
                if (isAscending)
                    Array.Sort(doubleNumbers);
                else
                    doubleNumbers = doubleNumbers.OrderByDescending(n => n).ToArray();

                StringBuilder stringBuilder = new StringBuilder();
                string orderedNumbers = string.Empty;
                for (int i = 0; i < _general.NumbersArray.Length; i++)
                {
                    orderedNumbers = stringBuilder.Append($"{doubleNumbers[i]}, ").ToString();
                }
                return orderedNumbers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        
        public double[] ConvertStringArrayIntoDoubleArray(string[] numbers)
        {
            try
            {
                List<double> convertedArray = new List<double>();
                for (int i = 0; i < numbers.Length; i++)
                {
                    convertedArray.Add(double.Parse(numbers[i]));
                }
                return convertedArray.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string FormatNumbers(string option)
        {
            try
            {
                if (string.Equals(option, "tab"))
                    return _general.Numbers.Replace('\t', ',');
                else if (string.Equals(option, "breakline"))
                    return _general.Numbers.Replace("\r\n", ",");
                else if (string.Equals(option, "space"))
                    return _general.Numbers.Trim();

                return string.Empty;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
