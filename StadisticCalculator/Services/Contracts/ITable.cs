using System.Collections.Generic;

namespace StadisticCalculator.Services
{
    public interface ITable
    {
        double GetRange();
        double GetAmplitude();
        List<string> GetIntervals();
        List<double> GetClassMark();
        List<double> GetAbsolutesFrequency();
        List<double> GetRelativeFrequencies();
        List<double> GetTotalAcumulatedFrequencies();
    }
}
