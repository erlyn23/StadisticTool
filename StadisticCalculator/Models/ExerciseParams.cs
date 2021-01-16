using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StadisticCalculator.Models
{
    public class ExerciseParams
    {
        [Required(ErrorMessage = "Debes insertar los datos")]
        public string Numbers { get; set; }
        public int ClassInterval { get; set; }

        [Required(ErrorMessage = "Debes escribir el delimitador")]
        public char Delimiter { get; set; }
        public double Amplitude { get; set; }
        public double Range { get; set; }
        public List<string> Intervals { get; set; }
        public List<double> ClassMarks { get; set; }
        public List<double> AbsolutesFrequencies { get; set; }
        public List<double> RelativeFrequencies { get; set; }
        public List<double> TotalAcumulatedFrequencies { get; set; }
        public double ArithmeticMedia { get; set; }
        public double Median { get; set; }
        public double Fashion { get; set; }
        public double Variance { get; set; }
        public double StandardDesviation { get; set; }
        public double VariationCoefficent { get; set; }
        public bool HasArithmeticMedia { get; set; }
        public bool HasMedian { get; set; } 
        public bool HasFashion { get; set; }
        public bool HasVariance { get; set; }
        public bool HasStandardDesviation { get; set; }
        public bool HasVariationCoefficent { get; set; }
        public bool IsAscending { get; set; }

        public ExerciseParams()
        {
            Intervals = new List<string>();
            ClassMarks = new List<double>();
            AbsolutesFrequencies = new List<double>();
            RelativeFrequencies = new List<double>();
            TotalAcumulatedFrequencies = new List<double>();
            Delimiter = ',';

            ArithmeticMedia = 0;
            Median = 0;
            Fashion = 0;
            StandardDesviation = 0;
            Variance = 0;
            VariationCoefficent = 0;
        }
    }
}
