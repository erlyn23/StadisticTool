namespace StadisticCalculator.Services
{
    public interface ICentralTendencyMeasures
    {
        double GetArithmeticMedia();
        double GetMedian();
        double GetFashion();
        double GetVariance();
        double GetStandardDesviation();
        double GetVariationCoefficent();
    }
}
