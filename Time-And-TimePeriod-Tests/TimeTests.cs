using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Time_And_TimePeriod_Lib;

namespace Time_And_TimePeriod_Tests.Time_Tests
{
    [TestClass]
    public class TimeTests
    {
        #region Constructors

        private void AssertTime(Time t, byte expectedHour, byte expectedMinute, byte expectedSecond)
        {
            Assert.AreEqual(t.Hours, expectedHour);
            Assert.AreEqual(t.Minutes, expectedMinute);
            Assert.AreEqual(t.Seconds, expectedSecond);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(23, 59, 59)]
        [DataRow(23, 0, 59)]
        [DataRow(0, 0, 59)]
        [DataRow(0, 0, 0)]
        [DataRow(0, 59, 0)]
        [DataRow(12, 23, 48)]
        [DataRow(18,03, 2)]
        [DataRow(22,21, 0)]
        [DataRow(6,59, 53)]
        public void Constructor_ThreeParams_CreateTimeObject(int hour, int minute, int second)
        {
            var time = new Time((byte) hour, (byte) minute, (byte) second);

            AssertTime(time, (byte)hour, (byte)minute, (byte)second);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, 0)]
        [DataRow(23, 59)]
        [DataRow(0, 59)]
        [DataRow(23, 0)]
        [DataRow(12, 05)]
        [DataRow(05, 05)]
        public void Constructor_TwoParams_CreateTimeObjectWithZeroSeconds(int hour, int minute)
        {
            var time = new Time((byte) hour, (byte) minute);

            AssertTime(time, (byte) hour, (byte) minute, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0)]
        [DataRow(23)]
        [DataRow(22)]
        [DataRow(10)]
        [DataRow(6)]
        public void Constructor_OneParam_CreateTimeObjectWithZeroMinutesAndSecond(int hour)
        {
            var time = new Time((byte)hour);

            AssertTime(time, (byte) hour, 0, 0);
        }

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_NoParams_CreateTimeObjectWithZeroHoursMinutesSeconds()
        {
            var time = new Time();

            AssertTime(time, 0, 0,0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(24, 0, 0)]
        [DataRow(-1, 23, 23)]
        [DataRow(0, 60, 3)]
        [DataRow(0, 59, 60)]
        [DataRow(0, -1, 40)]
        [DataRow(-2, -1, -10)]
        [DataRow(2, 1, -10)]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_InvalidHour_ThrowsFormatException(int hour, int minute, int second)
        {
            var time = new Time((byte) hour, (byte) minute, (byte) second);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("00:00:00", 0, 0, 0)]
        [DataRow("23:59:59", 23, 59, 59)]
        [DataRow("23:59:00", 23, 59, 0)]
        [DataRow("23:00:00", 23, 0, 0)]
        [DataRow("12:00:00", 12, 0, 0)]
        [DataRow("01:05:05", 1, 5, 5)]
        public void Constructor_ValidStringFormat_CreateTimeObject(string input, int expectedHour, int expectedMinute,
            int expectedSecond)
        {
            var time = new Time(input);

            AssertTime(time, (byte) expectedHour, (byte) expectedMinute, (byte) expectedSecond);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("0;0;0")]
        [DataRow("241;10")]
        [DataRow("1:10;12")]
        [DataRow("0:10;13")]
        [DataRow("1:59")]
        [DataRow("59")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_InvalidString_ThrowsFormatException(string input)
        {
            var time = new Time(input);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("24:00:00")]
        [DataRow("24:1:10")]
        [DataRow("1:60:10")]
        [DataRow("1:59:60")]
        [DataRow("59:59:59")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ValidStringInvalidNonNegativeData_ThrowsArgumentOutOfRangeException(string input)
        {
            var time = new Time(input);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("-54:00:00")]
        [DataRow("23:-1:10")]
        [DataRow("1:60:-10")]
        [DataRow("-1:-59:-50")]
        [DataRow("9:-59:59")]
        [ExpectedException(typeof(OverflowException))]
        public void Constructor_ValidStringNegativeData_ThrowsArgumentOutOfRangeException(string input)
        {
            // OverflowException as byte can`t convert negative number
            var time = new Time(input);
        }


        #endregion

        #region Operators

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(12, 0, 0, true)]
        [DataRow(12, 12, 12, false)]
        [DataRow(12, 0, 19, false)]
        [DataRow(0, 0, 0, false)]
        public void Equality_CompareToTimeAtTwelve_TrueIfSameFalseIfOther(int hour, int minute, int second,
            bool expectedResult)
        {
            var timeTested = new Time((byte)hour, (byte)minute, (byte)second);
            var timeCompared = new Time(12, 0, 0);

            var result = timeTested == timeCompared;

            Assert.AreEqual(result, expectedResult);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(12, 0, 0, false)]
        [DataRow(12, 12, 12, true)]
        [DataRow(12, 0, 19, true)]
        [DataRow(0, 0, 0, true)]
        public void EqualityNegation_CompareToTimeAtTwelve_TrueIfOtherFalseIfSame(int hour, int minute, int second,
            bool expectedResult)
        {
            var timeTested = new Time((byte)hour, (byte)minute, (byte)second);
            var timeCompared = new Time(12, 0, 0);

            var result = timeTested != timeCompared;

            Assert.AreEqual(result, expectedResult);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0, 0, 0, false)]
        [DataRow(12, 0, 0, false)]
        [DataRow(12, 0, 1, true)]
        [DataRow(11, 59, 59, false)]
        [DataRow(23, 12, 12, true)]
        public void GreaterThen_CompareToTimeAtTwelve_TrueIfGreaterFalseIfLessOrEqual(int hour, int minute, int second,
            bool expectedResult)
        {
            var timeTested = new Time((byte) hour, (byte) minute, (byte) second);
            var timeCompared = new Time(12, 0, 0);

            var result = timeTested > timeCompared;

            Assert.AreEqual(result, expectedResult);

        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(12, 0, 0, true)]
        [DataRow(12, 0, 1, true)]
        [DataRow(11, 59, 59, false)]
        [DataRow(0, 0, 59, false)]
        [DataRow(22, 23, 0, true)]
        public void GreaterOrEqualTo_CompareToTimeAtTwelve_TrueIfGreaterOrEqualFalseIfLess(int hour, int minute,
            int second, bool expectedResult)
        {
            var timeTested = new Time((byte) hour, (byte) minute, (byte) second);
            var timeCompared = new Time(12, 0, 0);

            var result = timeTested >= timeCompared;

            Assert.AreEqual(result, expectedResult);
        }


        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0, 0, 0, true)]
        [DataRow(12, 0, 0, false)]
        [DataRow(12, 0, 1, false)]
        [DataRow(11, 59, 59, true)]
        [DataRow(23, 12, 12, false)]
        public void LessThen_CompareToTimeAtTwelve_TrueIfSmallerFalseIfGreaterOrEqual(int hour, int minute, int second,
            bool expectedResult)
        {
            var timeTested = new Time((byte)hour, (byte)minute, (byte)second);
            var timeCompared = new Time(12, 0, 0);

            var result = timeTested < timeCompared;

            Assert.AreEqual(result, expectedResult);

        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(12, 0, 0, true)]
        [DataRow(12, 0, 1, false)]
        [DataRow(11, 59, 59, true)]
        [DataRow(0, 0, 59, true)]
        [DataRow(22, 23, 0, false)]
        public void SmallerOrEqualTo_CompareToTimeAtTwelve_TrueIfSmallerOrEqualsFalseIfGreater(int hour, int minute,
            int second, bool expectedResult)
        {
            var timeTested = new Time((byte)hour, (byte)minute, (byte)second);
            var timeCompared = new Time(12, 0, 0);

            var result = timeTested <= timeCompared;

            Assert.AreEqual(result, expectedResult);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow("12:00:00", "01:00:00", "13:00:00")]
        [DataRow("12:00:00", "12:00:00", "00:00:00")]
        [DataRow("12:00:00", "24:12:12", "12:12:12")]
        [DataRow("12:00:00", "72:00:00", "12:00:00")]
        [DataRow("12:00:00", "15:59:59", "03:59:59")]
        [DataRow("12:59:59", "00:00:2", "13:00:01")]
        [DataRow("15:30:00", "02:35:30", "18:05:30")]
        public void PlusSign_AddTimePeriodToTime_ReturnsCalculatedTime(string timeInput, string period, string expectedResult)
        {
            var time = new Time(timeInput);
            var timePeriod = new TimePeriod(period);

            var result = time + timePeriod;

            Assert.AreEqual(result.ToString(), expectedResult);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow("12:00:00", "01:00:00", "11:00:00")]
        [DataRow("12:00:00", "12:00:00", "00:00:00")]
        [DataRow("12:00:00", "24:12:12", "11:47:48")]
        [DataRow("12:00:00", "72:00:00", "12:00:00")]
        [DataRow("12:00:00", "15:59:59", "20:00:01")]
        [DataRow("00:00:01", "00:00:02", "23:59:59")]
        [DataRow("15:30:00", "02:35:30", "12:54:30")]
        public void MinusSign_SubtractTimePeriodFromTime_ReturnsCalculatedTime(string timeInput, string period, string expectedResult)
        {
            var time = new Time(timeInput);
            var timePeriod = new TimePeriod(period);

            var result = time - timePeriod;

            Assert.AreEqual(result.ToString(), expectedResult);
        }

        #endregion

        #region Equals

        [TestMethod, TestCategory("Equals")]
        public void Equals_SameHour_True()
        {
            var timeOne = new Time(12, 0, 0);
            var timeTwo = new Time(12, 0, 0);

            var result = timeOne.Equals(timeTwo);

            Assert.AreEqual(result, true);
        }

        [TestMethod, TestCategory("Equals")]
        public void Equals_OtherHour_False()
        {
            var timeOne = new Time(12, 30, 59);
            var timeTwo = new Time(23, 12, 00);

            var result = timeOne.Equals(timeTwo);

            Assert.AreEqual(result, false);
        }

        [TestMethod, TestCategory("Equals")]
        public void StaticEquals_SameHour_True()
        {
            var timeOne = new Time(12, 30, 40);
            var timeTwo = new Time(12, 30, 40);

            var result = Time.Equals(timeOne, timeTwo);

            Assert.AreEqual(result, true);
        }

        [TestMethod, TestCategory("Equals")]
        public void StaticEquals_OtherHour_False()
        {
            var timeOne = new Time(12, 30, 40);
            var timeTwo = new Time(1, 0, 40);

            var result = Time.Equals(timeOne, timeTwo);

            Assert.AreEqual(result, false);
        }

        #endregion

        #region ArithmeticOperations

        [DataTestMethod, TestCategory("ArithmeticOperations")]
        [DataRow("12:00:00", "01:00:00", "13:00:00")]
        [DataRow("12:00:00", "12:00:00", "00:00:00")]
        [DataRow("12:00:00", "24:12:12", "12:12:12")]
        [DataRow("12:00:00", "72:00:00", "12:00:00")]
        [DataRow("12:00:00", "15:59:59", "03:59:59")]
        [DataRow("12:59:59", "00:00:2", "13:00:01")]
        [DataRow("15:30:00", "02:35:30", "18:05:30")]
        public void Plus_AddTimePeriodToTime_ReturnsCalculatedTime(string timeInput, string period, string expectedResult)
        {
            var time = new Time(timeInput);
            var timePeriod = new TimePeriod(period);

            var result = time.Plus(timePeriod);

            Assert.AreEqual(result.ToString(), expectedResult);
        }

        [DataTestMethod, TestCategory("ArithmeticOperations")]
        [DataRow("12:00:00", "01:00:00", "11:00:00")]
        [DataRow("12:00:00", "12:00:00", "00:00:00")]
        [DataRow("12:00:00", "24:12:12", "11:47:48")]
        [DataRow("12:00:00", "72:00:00", "12:00:00")]
        [DataRow("12:00:00", "15:59:59", "20:00:01")]
        [DataRow("00:00:01", "00:00:02", "23:59:59")]
        [DataRow("15:30:00", "02:35:30", "12:54:30")]
        public void Minus_SubtractTimePeriodFromTime_ReturnsCalculatedTime(string timeInput, string period, string expectedResult)
        {
            var time = new Time(timeInput);
            var timePeriod = new TimePeriod(period);

            var result = time.Minus(timePeriod);

            Assert.AreEqual(result.ToString(), expectedResult);
        }

        #endregion

        #region ToString

        [DataTestMethod]
        [DataRow(1, 1, 1, "01:01:01")]
        [DataRow(12, 0, 0, "12:00:00")]
        [DataRow(23, 59, 0, "23:59:00")]
        [DataRow(0, 0, 0, "00:00:00")]
        [DataRow(15, 59, 59, "15:59:59")]
        public void ToString_DifferentValues_AlwaysReturnStringInSameFormat(int hour, int minute, int second,
            string expectedStringRepresentation)
        {
            var time = new Time((byte)hour, (byte)minute, (byte)second);

            Assert.AreEqual(time.ToString(), expectedStringRepresentation);
        }

        #endregion
    }
}
