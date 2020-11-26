using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Time_And_TimePeriod_Milliseconds_Lib;

namespace Time_And_TimePeriod_MilliSeconds_Tests
{
    [TestClass]
    public class TimePeriodTests
    {
        #region Constructors

        private void AssertTimePeriod(TimePeriod timePeriod, int expectedHours, int expectedMinutes,
            int expectedSeconds, int expectedMilliseconds)
        {
            Assert.AreEqual(timePeriod.Hours, expectedHours);
            Assert.AreEqual(timePeriod.Minutes, expectedMinutes);
            Assert.AreEqual(timePeriod.Seconds, expectedSeconds);
            Assert.AreEqual(timePeriod.Milliseconds, expectedMilliseconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(600.600, 0, 10, 0, 600)]
        [DataRow(3600.999, 1, 0, 0, 999)]
        [DataRow(3601.010, 1, 0, 1, 10)]
        [DataRow(312.5, 0, 5, 12, 500)]
        [DataRow(312.005, 0, 5, 12, 5)]
        [DataRow(86400.0, 24, 0, 0, 0)]
        public void Constructor_OneParam_CreateNewTimePeriodObject(double seconds, int expectedHours, int expectedMinutes,
            int expectedSeconds, int expectedMilliseconds)
        {
            var timePeriod = new TimePeriod(seconds);

            AssertTimePeriod(timePeriod, expectedHours, expectedMinutes, expectedSeconds, expectedMilliseconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_OneParamNegativeData_ThrowsArgumentOutOfRangeException()
        {
            var timePeriod = new TimePeriod(-1.09);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, 0, 0, 0)]
        [DataRow(23, 59, 23, 59)]
        [DataRow(0, 59, 0, 59)]
        [DataRow(23, 0, 23, 0)]
        [DataRow(12, 05, 12, 05)]
        [DataRow(05, 05, 05, 05)]
        [DataRow(36, 12, 36, 12)]
        [DataRow(76, 59, 76, 59)]
        public void Constructor_TwoParams_CreateNewTimePeriodObject(int hour, int minute, int expectedHour, int expectedMinute)
        {
            var timePeriod = new TimePeriod(hour, minute);

            AssertTimePeriod(timePeriod, expectedHour, expectedMinute, 0, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(23, 59, 59.600, 23, 59, 59, 600)]
        [DataRow(23, 0, 59.001, 23, 0, 59, 1)]
        [DataRow(0, 0, 59.999, 0, 0, 59, 999)]
        [DataRow(0, 0, 0.0, 0, 0, 0, 0)]
        [DataRow(0, 59, 0.010, 0, 59, 0, 10)]
        [DataRow(120, 23, 48.011, 120, 23, 48, 11)]
        [DataRow(72, 03, 59.499, 72, 03, 59, 499)]
        [DataRow(222, 21, 20.999, 222, 21, 20, 999)]
        [DataRow(24, 24, 24.300, 24, 24, 24, 300)]
        public void Constructor_ThreeParams_CreateTimePeriodObject(int hour, int minute, double second, int expectedHour,
            int expectedMinute, int expectedSecond, int expectedMillisecond)
        {
            var timePeriod = new TimePeriod(hour, minute, second);

            AssertTimePeriod(timePeriod, expectedHour, expectedMinute, expectedSecond, expectedMillisecond);
        }

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_NoParams_CreateTimePeriodObjectWithDefaultValues()
        {
            var timePeriod = new TimePeriod();

            AssertTimePeriod(timePeriod, 0, 0, 0, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("00:00:00.45", 0, 0, 0, 450)]
        [DataRow("23:59:59.000", 23, 59, 59, 0)]
        [DataRow("24:59:50.399", 24, 59, 50, 399)]
        [DataRow("72:00:00.010", 72, 0, 0, 10)]
        [DataRow("78:31:30.300", 78, 31, 30, 300)]
        [DataRow("01:05:05.999", 1, 5, 5, 999)]
        public void Constructor_ValidStringInput_CreateNewTimePeriodObject(string input, int expectedHour,
            int expectedMinute, int expectedSecond, int expectedMillisecond)
        {
            var timePeriod = new TimePeriod(input);

            AssertTimePeriod(timePeriod, expectedHour, expectedMinute, expectedSecond, expectedMillisecond);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("0:0:0:000")]
        [DataRow("241.10")]
        [DataRow("1:10.12")]
        [DataRow("0:10;13;10")]
        [DataRow("1:59.0")]
        [DataRow(".59")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_InvalidFormat_ThrowsFormatException(string input)
        {
            var timePeriod = new TimePeriod(input);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("24:300:00.400")]
        [DataRow("24:01:120.999")]
        [DataRow("1:0440:10.1000")]
        [DataRow("1:59:60.999")]
        [DataRow("59:01:60.1000")]
        [DataRow("-59:01:50.5000")]
        [DataRow("-59:-01:50.000")]
        [DataRow("-59:-01:-50.930")]
        [DataRow("59:-01:-50.000")]
        [DataRow("59:01:-50.100")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ValidStringInvalidData_ThrowsArgumentOutOfRangeException(string input)
        {
            var timePeriod = new TimePeriod(input);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(3, 0, 0, 500, 8, 59, 59, 500)]
        [DataRow(3, 30, 0, 300, 8, 29, 59, 700)]
        [DataRow(3, 30, 59, 999, 8, 29, 0, 1)]
        [DataRow(12, 0, 0, 1, 0, 0, 0, 1)]
        [DataRow(0, 0, 0, 1, 11, 59, 59, 999)]
        [DataRow(23, 59, 59, 490, 11, 59, 59, 490)]
        public void Constructor_SecondTimeAtTwelve_CreateNewCalculatedTimePeriodObject(int otherHour, int otherMinute, int otherSecond, int otherMillisecond, int expectedHour,
            int expectedMinute, int expectedSecond, int expectedMillisecond)
        {
            var timeAtTwelve = new Time(12, 0, 0, 0);
            var timeOther = new Time((byte)otherHour, (byte)otherMinute, (byte)otherSecond, otherMillisecond);
            var calculatedTimePeriod = new TimePeriod(timeAtTwelve, timeOther); // Order doesn`t matter

            AssertTimePeriod(calculatedTimePeriod, expectedHour, expectedMinute, expectedSecond, expectedMillisecond);
        }

        #endregion

        #region Equals

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(100.30, 100.31, false)]
        [DataRow(100.001, 100.002, false)]
        [DataRow(100.01, 1000.01, false)]
        [DataRow(100.999, 100.999, true)]
        [DataRow(250.300, 250.3, true)]
        public void Equals_TwoTimePeriods_TrueIfSameAmountOfSecondsFalseOtherwise(double secondsOne, double secondsTwo, bool expectedResult)
        {
            var timePeriodOne = new TimePeriod(secondsOne);
            var timePeriodTwo = new TimePeriod(secondsTwo);

            var result = timePeriodOne.Equals(timePeriodTwo);

            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod, TestCategory("Equals")]
        public void StaticEquals_SameAmountOfSeconds_ReturnTrue()
        {
            var timeOne = new TimePeriod(120, 30, 40.300);
            var timeTwo = new TimePeriod(120, 30, 40.300);

            var result = TimePeriod.Equals(timeOne, timeTwo);

            Assert.AreEqual(result, true);
        }

        [TestMethod, TestCategory("Equals")]
        public void StaticEquals_OtherAmountOfSeconds_ReturnFalse()
        {
            var timeOne = new TimePeriod(120, 30, 40.489);
            var timeTwo = new TimePeriod(1, 0, 40.290);

            var result = TimePeriod.Equals(timeOne, timeTwo);

            Assert.AreEqual(result, false);
        }
        #endregion

        #region Operators

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(24, 0, 0.00, true)]
        [DataRow(24, 12, 12.999, false)]
        [DataRow(24, 0, 19.300, false)]
        [DataRow(0, 0, 0.19, false)]
        public void Equality_CompareToTimePeriodEqualsTwentyFourHours_TrueIfSameFalseIfOther(int hour, int minute, double second,
            bool expectedResult)
        {
            var timeTested = new TimePeriod(hour, minute, second);
            var timeCompared = new TimePeriod(24, 0, 0.0);

            var result = timeTested == timeCompared;

            Assert.AreEqual(result, expectedResult);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(24, 0, 0.00, false)]
        [DataRow(24, 12, 12.32, true)]
        [DataRow(24, 0, 19.1, true)]
        [DataRow(0, 0, 0.999, true)]
        public void EqualityNegation_CompareToTimePeriodEqualsTwentyFourHours_TrueIfOtherFalseIfSame(int hour, int minute, double second,
            bool expectedResult)
        {
            var timeTested = new TimePeriod(hour, minute, second);
            var timeCompared = new TimePeriod(24, 0, 0.00);

            var result = timeTested != timeCompared;

            Assert.AreEqual(result, expectedResult);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0, 0, 0.00, false)]
        [DataRow(24, 0, 0.000, false)]
        [DataRow(24, 0, 1.300, true)]
        [DataRow(23, 59, 59.999, false)]
        [DataRow(36, 12, 12.001, true)]
        public void GreaterThen_CompareToTimePeriodEqualsTwentyFourHours_TrueIfGreaterFalseIfLessOrEqual(int hour, int minute, double second,
            bool expectedResult)
        {
            var timeTested = new TimePeriod(hour, minute, second);
            var timeCompared = new TimePeriod(24, 0, 0);

            var result = timeTested > timeCompared;

            Assert.AreEqual(result, expectedResult);

        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(24, 0, 0.000, true)]
        [DataRow(24, 0, 1.999, true)]
        [DataRow(23, 59, 59.999, false)]
        [DataRow(22, 0, 59.999, false)]
        [DataRow(24, 23, 0.300, true)]
        public void GreaterOrEqualTo_CompareToTimePeriodEqualsTwentyFourHours_TrueIfGreaterOrEqualFalseIfLess(int hour, int minute,
            double second, bool expectedResult)
        {
            var timeTested = new TimePeriod(hour, minute, second);
            var timeCompared = new TimePeriod(24, 0, 0);

            var result = timeTested >= timeCompared;

            Assert.AreEqual(result, expectedResult);
        }


        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0, 0, 0.900, true)]
        [DataRow(24, 0, 0.000, false)]
        [DataRow(24, 0, 1.900, false)]
        [DataRow(23, 59, 59.999, true)]
        [DataRow(74, 12, 12.300, false)]
        public void LessThen_CompareToTimePeriodEqualsTwentyFourHours_TrueIfSmallerFalseIfGreaterOrEqual(int hour, int minute, double second,
            bool expectedResult)
        {
            var timeTested = new TimePeriod(hour, minute, second);
            var timeCompared = new TimePeriod(24, 0, 0);

            var result = timeTested < timeCompared;

            Assert.AreEqual(result, expectedResult);

        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(24, 0, 0.000, true)]
        [DataRow(24, 0, 1.999, false)]
        [DataRow(23, 59, 59.999, true)]
        [DataRow(0, 0, 59.93, true)]
        [DataRow(77, 23, 0.300, false)]
        public void SmallerOrEqualTo_CompareToTimePeriodEqualsTwentyFourHours_TrueIfSmallerOrEqualsFalseIfGreater(int hour, int minute,
            double second, bool expectedResult)
        {
            var timeTested = new TimePeriod(hour, minute, second);
            var timeCompared = new TimePeriod(24, 0, 0);

            var result = timeTested <= timeCompared;

            Assert.AreEqual(result, expectedResult);
        }



        #endregion

        #region ArithmeticOperations

        [DataTestMethod, TestCategory("ArithmeticOperations")]
        [DataRow("12:00:00.100", "01:00:00.100", "13:00:00.200")]
        [DataRow("12:00:00.600", "12:00:00.600", "24:00:01.200")]
        [DataRow("12:00:00.999", "24:12:12.999", "36:12:13.998")]
        [DataRow("12:00:00.500", "72:00:00.499", "84:00:00.999")]
        [DataRow("12:00:00.250", "15:59:59.250", "27:59:59.500")]
        [DataRow("12:59:59.003", "00:00:2.003", "13:00:01.006")]
        [DataRow("15:30:00.150", "02:35:30.150", "18:05:30.300")]
        public void PlusSign_AddTwoTimePeriods_ReturnsSumOfTwoTimePeriod(string left, string right, string expectedResult)
        {
            var timePeriodLeft = new TimePeriod(left);
            var timePeriodRight = new TimePeriod(right);

            var result = timePeriodLeft + timePeriodRight;

            Assert.AreEqual(result.ToString(), expectedResult);
        }

        [DataTestMethod, TestCategory("ArithmeticOperations")]
        [DataRow("12:00:00.999", "01:00:00.599", "11:00:00.400")]
        [DataRow("12:00:00.000", "12:00:00.000", "00:00:00.000")]
        [DataRow("24:12:12.100", "12:00:00.200", "12:12:11.900")]
        [DataRow("72:00:00.500", "12:00:00.250", "60:00:00.250")]
        [DataRow("50:00:00.300", "15:59:59.300", "34:00:01.000")]
        [DataRow("00:00:03.300", "00:00:02.900", "00:00:00.400")]
        [DataRow("15:30:00.000", "02:35:30.000", "12:54:30.000")]
        public void MinusSign_SubtractTwoTimePeriods_ReturnsSubtractedTimePeriod(string left, string right, string expectedResult)
        {
            var timePeriodOne = new TimePeriod(left);
            var timePeriodTwo = new TimePeriod(right);

            var result = timePeriodOne - timePeriodTwo;

            Assert.AreEqual(result.ToString(), expectedResult);
        }

        [DataTestMethod, TestCategory("ArithmeticOperations")]
        [DataRow("12:00:00", "31:00:00")]
        [DataRow("12:00:00.0", "122:00:00.000")]
        [DataRow("24:12:12.999", "24:13:00.000")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MinusSign_SubtractSmallerTimePeriodFromBigger_ThrowsArgumentOutOfRangeException(string left, string right)
        {
            var timePeriodOne = new TimePeriod(left);
            var timePeriodTwo = new TimePeriod(right);

            var result = timePeriodOne - timePeriodTwo;
        }

        [DataTestMethod, TestCategory("ArithmeticOperations")]
        [DataRow("03:00:00.500", 2, "06:00:01.000")]
        [DataRow("03:00:00.499", 2, "06:00:00.998")]
        [DataRow("03:00:00.300", 3, "09:00:00.900")]
        [DataRow("03:30:00.250", 2, "07:00:00.500")]
        [DataRow("03:59:59.050", 2, "07:59:58.100")]
        public void Multiply_TimePeriodByInteger_ReturnsTimePeriodMultipliedByInteger(string timePeriodInput, int multiplier,
            string expectedResult)
        {
            var timePeriod = new TimePeriod(timePeriodInput);

            var multiplied = timePeriod * multiplier;

            Assert.AreEqual(expectedResult, multiplied.ToString());
        }

        [TestMethod, TestCategory("ArithmeticOperations")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Multiply_ByNegative_ThrowsArgumentOutOfRangeException()
        {
            var timePeriod = new TimePeriod(120.12);

            var result = timePeriod * -1;
        }

        [DataTestMethod, TestCategory("ArithmeticOperations")]
        [DataRow("01:00:00.500", 2, "00:30:00.250")]
        [DataRow("01:00:00.999", 1, "01:00:00.999")]
        [DataRow("12:00:00.550", 2, "06:00:00.275")]
        [DataRow("20:30:50.999", 2, "10:15:25.499")]
        [DataRow("20:20:20.200", 4, "05:05:05.050")]
        public void Divide_TimePeriodByInteger_ReturnsTimePeriodDividedByInteger(string timePeriodInput, int divider,
            string expectedResult)
        {
            var timePeriod = new TimePeriod(timePeriodInput);

            var divided = timePeriod / divider;

            Assert.AreEqual(expectedResult, divided.ToString());
        }

        [TestMethod, TestCategory("ArithmeticOperations")]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            var timePeriod = new TimePeriod(300.321);

            var result = timePeriod / 0;
        }

        [TestMethod, TestCategory("ArithmeticOperations")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Divide_ByNegative_ThrowsArgumentOutOfRangeException()
        {
            var timePeriod = new TimePeriod(120.31);

            var result = timePeriod / -1;
        }

        #endregion

        #region ToString

        [DataTestMethod, TestCategory("ToString")]
        [DataRow("23:01:09.999", "23:01:09.999")]
        [DataRow("55:01:09.003", "55:01:09.003")]
        [DataRow("55:59:09.030", "55:59:09.030")]
        [DataRow("55:59:9.031", "55:59:09.031")]
        [DataRow("01:01:01.300", "01:01:01.300")]
        [DataRow("123:01:01.000", "123:01:01.000")]
        public void ToString_DifferentValues_ReturnsCorrectStringRepresentation(string input,
            string expectedResult)
        {
            var timePeriod = new TimePeriod(input);

            Assert.AreEqual(timePeriod.ToString(), expectedResult);
        }

        #endregion
    }
}
