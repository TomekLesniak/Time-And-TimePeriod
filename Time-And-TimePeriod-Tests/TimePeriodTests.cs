using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Time_And_TimePeriod_Lib;

namespace Time_And_TimePeriod_Tests.TimePeriod_Tests
{
    [TestClass]
    public class TimePeriodTests
    {
        #region Constructors

        private void AssertTimePeriod(TimePeriod timePeriod, int expectedHours, int expectedMinutes,
            int expectedSeconds)
        {
            Assert.AreEqual(timePeriod.Hours, expectedHours);
            Assert.AreEqual(timePeriod.Minutes, expectedMinutes);
            Assert.AreEqual(timePeriod.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(600, 0, 10, 0)]
        [DataRow(3600, 1, 0, 0)]
        [DataRow(3601, 1, 0, 1)]
        [DataRow(312, 0, 5, 12)]
        [DataRow(312, 0, 5, 12)]
        [DataRow(86400, 24, 0, 0)]
        public void Constructor_OneParam_CreateNewTimePeriodObject(long seconds, int expectedHours, int expectedMinutes,
            int expectedSeconds)
        {
            var timePeriod = new TimePeriod(seconds);
            
            AssertTimePeriod(timePeriod, expectedHours, expectedMinutes, expectedSeconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_OneParamNegativeData_ThrowsArgumentOutOfRangeException()
        {
            var timePeriod = new TimePeriod(-1);
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

            AssertTimePeriod(timePeriod, expectedHour, expectedMinute, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(23, 59, 59, 23, 59, 59)]
        [DataRow(23, 0, 59, 23, 0, 59)]
        [DataRow(0, 0, 59, 0, 0, 59)]
        [DataRow(0, 0, 0, 0, 0, 0)]
        [DataRow(0, 59, 0, 0, 59, 0)]
        [DataRow(120, 23, 48, 120, 23, 48)]
        [DataRow(72, 03, 59, 72, 03, 59)]
        [DataRow(222, 21, 20, 222, 21, 20)]
        [DataRow(24, 24, 24, 24, 24, 24)]
        public void Constructor_ThreeParams_CreateTimePeriodObject(int hour, int minute, int second, int expectedHour,
            int expectedMinute, int expectedSecond)
        {
            var timePeriod = new TimePeriod(hour, minute, second);

            AssertTimePeriod(timePeriod, expectedHour, expectedMinute, expectedSecond);
        }

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_NoParams_CreateTimePeriodObjectWithDefaultValues()
        {
            var timePeriod = new TimePeriod();

            AssertTimePeriod(timePeriod, 0,0,0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("00:00:00", 0, 0, 0)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("24:59:50", 24, 59, 50)]
        [DataRow("72:00:00", 72, 0, 0)]
        [DataRow("78:31:30", 78, 31, 30)]
        [DataRow("01:05:05", 1, 5, 5)]
        public void Constructor_ValidStringInput_CreateNewTimePeriodObject(string input, int expectedHour,
            int expectedMinute, int expectedSecond)
        {
            var timePeriod = new TimePeriod(input);

            AssertTimePeriod(timePeriod, expectedHour, expectedMinute, expectedSecond);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("0;0:0")]
        [DataRow("241;10")]
        [DataRow("1:10;12")]
        [DataRow("0:10;13")]
        [DataRow("1:59")]
        [DataRow("59")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_InvalidFormat_ThrowsFormatException(string input)
        {
            var timePeriod = new TimePeriod(input);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("24:00:100")]
        [DataRow("24:01:120")]
        [DataRow("1:60:10")]
        [DataRow("1:59:60")]
        [DataRow("59:01:60")]
        [DataRow("-59:01:50")]
        [DataRow("-59:-01:50")]
        [DataRow("-59:-01:-50")]
        [DataRow("59:-01:-50")]
        [DataRow("59:01:-50")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ValidStringInvalidData_ThrowsArgumentOutOfRangeException(string input)
        {
            var timePeriod = new TimePeriod(input);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(3, 0, 0, 9, 0, 0)]
        [DataRow(3, 30, 0, 8, 30, 0)]
        [DataRow(3, 30, 59, 8, 29, 1)]
        [DataRow(12, 0, 0, 0, 0, 0)]
        [DataRow(0, 0, 0, 12, 0, 0)]
        [DataRow(23, 59, 59, 11, 59, 59)]
        public void Constructor_SecondTimeAtTwelve_CreateNewCalculatedTimePeriodObject(int otherHour, int otherMinute, int otherSecond, int expectedHour,
            int expectedMinute, int expectedSecond)
        {
            var timeAtTwelve = new Time(12, 0, 0);
            var timeOther = new Time((byte)otherHour, (byte)otherMinute, (byte)otherSecond);
            var calculatedTimePeriod = new TimePeriod(timeAtTwelve, timeOther); // Order doesn`t matter

            AssertTimePeriod(calculatedTimePeriod, expectedHour, expectedMinute, expectedSecond);
        }

        #endregion

        #region Equals

        [DataTestMethod, TestCategory("Equals")]
        [DataRow(100, 200, false)]
        [DataRow(100, 10, false)]
        [DataRow(100, 1000, false)]
        [DataRow(100, 100, true)]
        [DataRow(250, 250, true)]
        public void Equals_TwoTimePeriods_TrueIfSameAmountOfSecondsFalseOtherwise(long secondsOne, long secondsTwo, bool expectedResult)
        {
            var timePeriodOne = new TimePeriod(secondsOne);
            var timePeriodTwo = new TimePeriod(secondsTwo);

            var result = timePeriodOne.Equals(timePeriodTwo);

            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod, TestCategory("Equals")]
        public void StaticEquals_SameAmountOfSeconds_ReturnTrue()
        {
            var timeOne = new TimePeriod(120, 30, 40);
            var timeTwo = new TimePeriod(120, 30, 40);

            var result = TimePeriod.Equals(timeOne, timeTwo);

            Assert.AreEqual(result, true);
        }

        [TestMethod, TestCategory("Equals")]
        public void StaticEquals_OtherAmountOfSeconds_ReturnFalse()
        {
            var timeOne = new TimePeriod(120, 30, 40);
            var timeTwo = new TimePeriod(1, 0, 40);

            var result = TimePeriod.Equals(timeOne, timeTwo);

            Assert.AreEqual(result, false);
        }
        #endregion

        #region Operators

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(24, 0, 0, true)]
        [DataRow(24, 12, 12, false)]
        [DataRow(24, 0, 19, false)]
        [DataRow(0, 0, 0, false)]
        public void Equality_CompareToTimePeriodEqualsTwentyFourHours_TrueIfSameFalseIfOther(int hour, int minute, int second,
            bool expectedResult)
        {
            var timeTested = new TimePeriod(hour, minute, second);
            var timeCompared = new TimePeriod(24, 0, 0);

            var result = timeTested == timeCompared;

            Assert.AreEqual(result, expectedResult);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(24, 0, 0, false)]
        [DataRow(24, 12, 12, true)]
        [DataRow(24, 0, 19, true)]
        [DataRow(0, 0, 0, true)]
        public void EqualityNegation_CompareToTimePeriodEqualsTwentyFourHours_TrueIfOtherFalseIfSame(int hour, int minute, int second,
            bool expectedResult)
        {
            var timeTested = new TimePeriod(hour, minute, second);
            var timeCompared = new TimePeriod(24, 0, 0);

            var result = timeTested != timeCompared;

            Assert.AreEqual(result, expectedResult);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0, 0, 0, false)]
        [DataRow(24, 0, 0, false)]
        [DataRow(24, 0, 1, true)]
        [DataRow(23, 59, 59, false)]
        [DataRow(36, 12, 12, true)]
        public void GreaterThen_CompareToTimePeriodEqualsTwentyFourHours_TrueIfGreaterFalseIfLessOrEqual(int hour, int minute, int second,
            bool expectedResult)
        {
            var timeTested = new TimePeriod(hour, minute, second);
            var timeCompared = new TimePeriod(24, 0, 0);

            var result = timeTested > timeCompared;

            Assert.AreEqual(result, expectedResult);

        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(24, 0, 0, true)]
        [DataRow(24, 0, 1, true)]
        [DataRow(23, 59, 59, false)]
        [DataRow(22, 0, 59, false)]
        [DataRow(24, 23, 0, true)]
        public void GreaterOrEqualTo_CompareToTimePeriodEqualsTwentyFourHours_TrueIfGreaterOrEqualFalseIfLess(int hour, int minute,
            int second, bool expectedResult)
        {
            var timeTested = new TimePeriod(hour, minute, second);
            var timeCompared = new TimePeriod(24, 0, 0);

            var result = timeTested >= timeCompared;

            Assert.AreEqual(result, expectedResult);
        }


        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0, 0, 0, true)]
        [DataRow(24, 0, 0, false)]
        [DataRow(24, 0, 1, false)]
        [DataRow(23, 59, 59, true)]
        [DataRow(74, 12, 12, false)]
        public void LessThen_CompareToTimePeriodEqualsTwentyFourHours_TrueIfSmallerFalseIfGreaterOrEqual(int hour, int minute, int second,
            bool expectedResult)
        {
            var timeTested = new TimePeriod(hour, minute, second);
            var timeCompared = new TimePeriod(24, 0, 0);

            var result = timeTested < timeCompared;

            Assert.AreEqual(result, expectedResult);

        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(24, 0, 0, true)]
        [DataRow(24, 0, 1, false)]
        [DataRow(23, 59, 59, true)]
        [DataRow(0, 0, 59, true)]
        [DataRow(77, 23, 0, false)]
        public void SmallerOrEqualTo_CompareToTimePeriodEqualsTwentyFourHours_TrueIfSmallerOrEqualsFalseIfGreater(int hour, int minute,
            int second, bool expectedResult)
        {
            var timeTested = new TimePeriod(hour, minute, second);
            var timeCompared = new TimePeriod(24, 0, 0);

            var result = timeTested <= timeCompared;

            Assert.AreEqual(result, expectedResult);
        }



        #endregion

        #region ArithmeticOperations

        [DataTestMethod, TestCategory("ArithmeticOperations")]
        [DataRow("12:00:00", "01:00:00", "13:00:00")]
        [DataRow("12:00:00", "12:00:00", "24:00:00")]
        [DataRow("12:00:00", "24:12:12", "36:12:12")]
        [DataRow("12:00:00", "72:00:00", "84:00:00")]
        [DataRow("12:00:00", "15:59:59", "27:59:59")]
        [DataRow("12:59:59", "00:00:2", "13:00:01")]
        [DataRow("15:30:00", "02:35:30", "18:05:30")]
        public void PlusSign_AddTwoTimePeriods_ReturnsSumOfTwoTimePeriod(string left, string right, string expectedResult)
        {
            var timePeriodLeft = new TimePeriod(left);
            var timePeriodRight = new TimePeriod(right);

            var result = timePeriodLeft + timePeriodRight;

            Assert.AreEqual(result.ToString(), expectedResult);
        }

        [DataTestMethod, TestCategory("ArithmeticOperations")]
        [DataRow("12:00:00", "01:00:00", "11:00:00")]
        [DataRow("12:00:00", "12:00:00", "00:00:00")]
        [DataRow("24:12:12", "12:00:00", "12:12:12")]
        [DataRow("72:00:00", "12:00:00", "60:00:00")]
        [DataRow("50:00:00", "15:59:59", "34:00:01")]
        [DataRow("00:00:03", "00:00:02", "00:00:01")]
        [DataRow("15:30:00", "02:35:30", "12:54:30")]
        public void MinusSign_SubtractTwoTimePeriods_ReturnsSubtractedTimePeriod(string left, string right, string expectedResult)
        {
            var timePeriodOne = new TimePeriod(left);
            var timePeriodTwo = new TimePeriod(right);

            var result = timePeriodOne - timePeriodTwo;

            Assert.AreEqual(result.ToString(), expectedResult);
        }

        [DataTestMethod, TestCategory("ArithmeticOperations")]
        [DataRow("12:00:00", "31:00:00")]
        [DataRow("12:00:00", "122:00:00")]
        [DataRow("24:12:12", "24:13:00")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void MinusSign_SubtractSmallerTimePeriodFromBigger_ThrowsArgumentOutOfRangeException(string left, string right)
        {
            var timePeriodOne = new TimePeriod(left);
            var timePeriodTwo = new TimePeriod(right);

            var result = timePeriodOne - timePeriodTwo;
        }

        [DataTestMethod, TestCategory("ArithmeticOperations")]
        [DataRow("03:00:00", 2, "06:00:00")]
        [DataRow("03:00:00", 3, "09:00:00")]
        [DataRow("03:00:00", 1, "03:00:00")]
        [DataRow("03:30:00", 2, "07:00:00")]
        [DataRow("03:59:59", 2, "07:59:58")]
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
            var timePeriod = new TimePeriod(120);

            var result = timePeriod * -1;
        }

        [DataTestMethod, TestCategory("ArithmeticOperations")]
        [DataRow("01:00:00", 2, "00:30:00")]
        [DataRow("01:00:00", 1, "01:00:00")]
        [DataRow("12:00:00", 2, "06:00:00")]
        [DataRow("20:30:50", 2, "10:15:25")]
        [DataRow("20:20:20", 4, "05:05:05")]
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
            var timePeriod = new TimePeriod(300);

            var result = timePeriod / 0;
        }

        [TestMethod, TestCategory("ArithmeticOperations")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Divide_ByNegative_ThrowsArgumentOutOfRangeException()
        {
            var timePeriod = new TimePeriod(120);

            var result = timePeriod / -1;
        }

        #endregion

        #region ToString

        [DataTestMethod, TestCategory("ToString")]
        [DataRow("23:01:09", "23:01:09")]
        [DataRow("55:01:09", "55:01:09")]
        [DataRow("55:59:09", "55:59:09")]
        [DataRow("55:59:9", "55:59:09")]
        [DataRow("01:01:01", "01:01:01")]
        [DataRow("123:01:01", "123:01:01")]
        public void ToString_DifferentValues_ReturnsCorrectStringRepresentation(string input,
            string expectedResult)
        {
            var timePeriod = new TimePeriod(input);

            Assert.AreEqual(timePeriod.ToString(), expectedResult);
        }

        #endregion
    }
}
