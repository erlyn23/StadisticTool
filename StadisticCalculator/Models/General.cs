
namespace StadisticCalculator.Models
{
    public class General
    {
        public string Numbers { get; set; }
        public int ClassInterval { get; set; }
        public char Delimiter { get; set; }
        public string[] NumbersArray { get; private set; }

        public General(string numbers)
        {
            Numbers = numbers;
            if(!string.IsNullOrEmpty(Numbers))
                NumbersArray = Numbers.Trim().Replace(Delimiter, ',').Split(',');
        }
    }
}
