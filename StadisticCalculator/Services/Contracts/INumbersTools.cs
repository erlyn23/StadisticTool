namespace StadisticCalculator.Services
{
    public interface INumbersTools
    {
        string SortNumbers(bool isAscending);
        string FormatNumbers(string option);
        double[] ConvertStringArrayIntoDoubleArray(string[] numbers);
    }
}
