using Microsoft.VisualStudio.TestTools.UnitTesting;
using StadisticCalculator.Services;
using System.Collections.Generic;

namespace StadisticCalculatorTest
{
    [TestClass]
    public class StadisticTest
    {
        private readonly string dataTest = "60, 60, 61, 62, 62, 62, 63, 63, 64, 65, 66, 66, 66, 67, 67, 67, 67, 68, 68, 68, 69, 69, 69, 70, 70, 70, 70, 70, 70, 71, 72, 72, 72, 72, 72, 72, 72, 73, 73, 74, 74, 74, 74, 74, 75, 75, 75, 75, 76, 76, 76, 76, 76, 76, 76, 76, 76, 76, 76, 76, 76, 77, 78, 78, 78, 78, 79, 79, 79, 79, 79, 79, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 80, 81, 81, 81, 81, 81, 81, 81, 82, 82, 82, 82, 82, 82, 82, 82, 82, 82, 82, 83, 83, 83, 83, 83, 83, 83, 83, 83, 83, 83, 83, 84, 84, 84, 84, 85, 85, 85, 85, 85, 85, 85, 85, 85, 86, 86, 86, 86, 86, 86, 86, 86, 87, 87, 87, 87, 87, 88, 89, 89, 89, 89, 90, 90, 90, 90, 92, 92, 92, 92, 93, 93, 93, 94, 95";
        private Table operations;

        public StadisticTest()
        {
            operations = new Operations();
            operations.Numbers = dataTest;
            operations.MakeArrayOfNumbers();
        }
        [TestMethod]
        public void SortNumbersTest()
        {
            string numbersExpected = dataTest+", ";
            string numbersOrderedMethod = operations.SortNumbers();

            Assert.AreEqual(numbersExpected, numbersOrderedMethod);
        }

        [TestMethod]
        public void GetRangeTest()
        {
            double rangeExpected = 35;
            double producedRange = operations.GetRange();

            Assert.AreEqual(rangeExpected, producedRange);
        }

        [TestMethod]
        public void GetAmplitudeTest()
        {
            double amplitudeExpected = 7;
            double producedAmplitude = operations.GetAmplitude(5);

            Assert.AreEqual(amplitudeExpected, producedAmplitude);
        }


        [TestMethod]
        public void GetIntervalsTest()
        {
            List<string> intervalsExpected = new List<string>();
            intervalsExpected.Add("[60-67)");
            intervalsExpected.Add("[67-74)");
            intervalsExpected.Add("[74-81)");
            intervalsExpected.Add("[81-88)");
            intervalsExpected.Add("[88-95)");
            List<string> intervalsProduced = operations.GetIntervals(5);

            for(int i = 0; i < intervalsExpected.Count; i++)
            {
                Assert.AreEqual(intervalsExpected[i], intervalsProduced[i]);
            }
        }

        [TestMethod]
        public void GetMarkClassTest()
        {
            List<double> classMarksExpected = new List<double>();
            classMarksExpected.Add(64);
            classMarksExpected.Add(71);
            classMarksExpected.Add(78);
            classMarksExpected.Add(85);
            classMarksExpected.Add(92);

            var classMarksActual = operations.GetClassMark(5);

            for (int i = 0; i < classMarksExpected.Count; i++)
            {
                Assert.AreEqual(classMarksExpected[i], classMarksActual[i]);
            }
        }

        [TestMethod]
        public void GetAbsolutesFrequenciesTest()
        {
            List<double> absolutesFrequenciesExpected = new List<double>();
            absolutesFrequenciesExpected.Add(13);
            absolutesFrequenciesExpected.Add(26);
            absolutesFrequenciesExpected.Add(53);
            absolutesFrequenciesExpected.Add(56);
            absolutesFrequenciesExpected.Add(18);

            var absolutesFrequenciesActual = operations.GetAbsolutesFrequency(5);

            for (int i = 0; i < absolutesFrequenciesExpected.Count; i++)
            {
                Assert.AreEqual(absolutesFrequenciesExpected[i], absolutesFrequenciesActual[i]);
            }
        }

        [TestMethod]
        public void GetRelativeFrequenciesTest()
        {
            List<double> relativesFrequenciesExpected = new List<double>();
            relativesFrequenciesExpected.Add(0.078);
            relativesFrequenciesExpected.Add(0.157);
            relativesFrequenciesExpected.Add(0.319);
            relativesFrequenciesExpected.Add(0.337);
            relativesFrequenciesExpected.Add(0.108);

            var relativesFrequenciesActual = operations.GetRelativeFrequencies(5);

            for (int i = 0; i < relativesFrequenciesExpected.Count; i++)
            {
                Assert.AreEqual(relativesFrequenciesExpected[i], relativesFrequenciesActual[i]);
            }
        }

        [TestMethod]
        public void GetTotalAcumulatedFrequenciesTest()
        {
            List<double> totalAcumulatedFrequenciesExpected = new List<double>();
            totalAcumulatedFrequenciesExpected.Add(13);
            totalAcumulatedFrequenciesExpected.Add(39);
            totalAcumulatedFrequenciesExpected.Add(92);
            totalAcumulatedFrequenciesExpected.Add(148);
            totalAcumulatedFrequenciesExpected.Add(166);

            var totalAcumulatedFrequenciesActual = operations.GetTotalAcumulatedFrequencies(5);

            for (int i = 0; i < totalAcumulatedFrequenciesExpected.Count; i++)
            {
                Assert.AreEqual(totalAcumulatedFrequenciesExpected[i], totalAcumulatedFrequenciesActual[i]);
            }
        }

    }
}
