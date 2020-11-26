using System;
using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Time_And_TimePeriod_Milliseconds_Lib;

namespace Time_And_TimePeriod_MilliSeconds_Tests
{

    [TestClass]
    public static class InitializeCulture
    {
        [AssemblyInitialize]
        public static void SetEnglishCultureOnAllUnitTest(TestContext context)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }
    }


    [TestClass]
    public class TimeTests
    {
        #region Constructors

        private void AssertTime(Time t, byte expectedHour, byte expectedMinute, byte expectedSecond, int expectedMillisecond)
        {
            Assert.AreEqual(t.Hours, expectedHour);
            Assert.AreEqual(t.Minutes, expectedMinute);
            Assert.AreEqual(t.Seconds, expectedSecond);
            Assert.AreEqual(t.Milliseconds, expectedMillisecond);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(23, 59, 59, 359)]
        [DataRow(23, 0, 59, 1)]
        [DataRow(0, 0, 59, 100)]
        [DataRow(0, 0, 0, 300)]
        [DataRow(0, 59, 0, 999)]
        [DataRow(12, 23, 48, 258)]
        [DataRow(18, 03, 2, 998)]
        [DataRow(22, 21, 0, 0)]
        [DataRow(6, 59, 53, 500)]
        public void Constructor_FourParams_CreateTimeObject(int hour, int minute, int second, int millisecond)
        {
            var time = new Time((byte)hour, (byte)minute, (byte)second, millisecond);

            AssertTime(time, (byte)hour, (byte)minute, (byte)second, millisecond);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, 0, 0)]
        [DataRow(23, 59, 59)]
        [DataRow(0, 59, 30)]
        [DataRow(23, 0, 0)]
        [DataRow(12, 05, 15)]
        [DataRow(05, 05, 5)]
        public void Constructor_ThreeParams_CreateTimeObjectWithZeroMilliseconds(int hour, int minute, int second)
        {
            var time = new Time((byte)hour, (byte)minute, (byte)second);

            AssertTime(time, (byte)hour, (byte)minute, (byte)second, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0, 12)]
        [DataRow(23, 0)]
        [DataRow(22, 59)]
        [DataRow(10, 9)]
        [DataRow(6, 40)]
        public void Constructor_TwoParams_CreateTimeObjectWithZeroMinutesSecondAndMilliseconds(int hour, int minute)
        {
            var time = new Time((byte)hour, (byte)minute);

            AssertTime(time, (byte)hour, (byte)minute, 0, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(0)]
        [DataRow(23)]
        [DataRow(22)]
        [DataRow(10)]
        [DataRow(6)]
        public void Constructor_OneParam_CreateTimeObjectWithZeroMinutesSecondsAndMilliseconds(int hour)
        {
            var time = new Time((byte)hour);

            AssertTime(time, (byte)hour, 0, 0, 0);
        }

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_NoParams_CreateTimeObjectWithZeroHoursMinutesSecondsMilliseconds()
        {
            var time = new Time();

            AssertTime(time, 0, 0, 0, 0);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(23, 0, 0,1000)]
        [DataRow(-1, 23, 23, -1)]
        [DataRow(0, 60, 3, 0)]
        [DataRow(0, 59, 50, -100)]
        [DataRow(0, -1, 40, 0)]
        [DataRow(-2, -1, -10, 999)]
        [DataRow(2, 1, -10, 01)]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_InvalidHour_ThrowsFormatException(int hour, int minute, int second, int millisecond)
        {
            var time = new Time((byte)hour, (byte)minute, (byte)second, millisecond);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("00:00:00.000", 0, 0, 0, 0)]
        [DataRow("23:59:59.999", 23, 59, 59, 999)]
        [DataRow("23:59:00.500", 23, 59, 0, 500)]
        [DataRow("23:00:00.001", 23, 0, 0, 1)]
        [DataRow("12:00:00.348", 12, 0, 0, 348)]
        [DataRow("01:05:05.985", 1, 5, 5, 985)]
        public void Constructor_ValidStringFormat_CreateTimeObject(string input, int expectedHour, int expectedMinute,
            int expectedSecond, int expectedMillisecond)
        {
            var time = new Time(input);

            AssertTime(time, (byte)expectedHour, (byte)expectedMinute, (byte)expectedSecond, expectedMillisecond);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("0:0:0:0")]
        [DataRow("241.10")]
        [DataRow("1:10:12;00")]
        [DataRow("0:10;13")]
        [DataRow("1:59.90:200")]
        [DataRow("59:59:59:59")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_InvalidString_ThrowsFormatException(string input)
        {
            var time = new Time(input);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("23:00:00.1000")]
        [DataRow("24:1:10.1000")]
        [DataRow("1:60:10.500")]
        [DataRow("1:59:60.0")]
        [DataRow("59:59:59.5900")]
        [DataRow("59:59:59.-900")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ValidStringDataOutOfRange_ThrowsArgumentOutOfRangeException(string input)
        {
            var time = new Time(input);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("-54:00:00:300")]
        [DataRow("-23:1:10:-300")]
        [DataRow("1:60:-10:-300")]
        [DataRow("-1:-59:-50:-1000")]
        [DataRow("9:-59:59:-1")]
        [ExpectedException(typeof(OverflowException))]
        public void Constructor_ValidStringNegativeData_ThrowsArgumentOutOfRangeException(string input)
        {
            // OverflowException as byte can`t convert negative number
            var time = new Time(input);
        }


        #endregion

        #region Operators

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(12, 0, 0, 0, true)]
        [DataRow(12, 12, 12, 12, false)]
        [DataRow(12, 0, 19, 900, false)]
        [DataRow(0, 0, 0,320,  false)]
        public void Equality_CompareToTimeAtTwelve_TrueIfSameFalseIfOther(int hour, int minute, int second, int millisecond,
            bool expectedResult)
        {
            var timeTested = new Time((byte)hour, (byte)minute, (byte)second, millisecond);
            var timeCompared = new Time(12, 0, 0, 0);

            var result = timeTested == timeCompared;

            Assert.AreEqual(result, expectedResult);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(12, 0, 0, 0, false)]
        [DataRow(12, 12, 12,300, true)]
        [DataRow(12, 0, 19,400, true)]
        [DataRow(0, 0, 0, 999, true)]
        public void EqualityNegation_CompareToTimeAtTwelve_TrueIfOtherFalseIfSame(int hour, int minute, int second, int millisecond,
            bool expectedResult)
        {
            var timeTested = new Time((byte)hour, (byte)minute, (byte)second, millisecond);
            var timeCompared = new Time(12, 0, 0, 0);

            var result = timeTested != timeCompared;

            Assert.AreEqual(result, expectedResult);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0, 0, 0, 0, false)]
        [DataRow(12, 0, 0, 0, false)]
        [DataRow(12, 0, 1, 999, true)]
        [DataRow(11, 59, 59, 999, false)]
        [DataRow(23, 12, 12, 342, true)]
        public void GreaterThen_CompareToTimeAtTwelve_TrueIfGreaterFalseIfLessOrEqual(int hour, int minute, int second, int millisecond,
            bool expectedResult)
        {
            var timeTested = new Time((byte)hour, (byte)minute, (byte)second, millisecond);
            var timeCompared = new Time(12, 0, 0);

            var result = timeTested > timeCompared;

            Assert.AreEqual(result, expectedResult);

        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(12, 0, 0, 0, true)]
        [DataRow(12, 0, 1, 999, true)]
        [DataRow(11, 59, 59, 999, false)]
        [DataRow(0, 0, 59, 300, false)]
        [DataRow(22, 23, 0, 500, true)]
        public void GreaterOrEqualTo_CompareToTimeAtTwelve_TrueIfGreaterOrEqualFalseIfLess(int hour, int minute,
            int second, int millisecond, bool expectedResult)
        {
            var timeTested = new Time((byte)hour, (byte)minute, (byte)second, millisecond);
            var timeCompared = new Time(12, 0, 0);

            var result = timeTested >= timeCompared;

            Assert.AreEqual(result, expectedResult);
        }


        [DataTestMethod, TestCategory("Operators")]
        [DataRow(0, 0, 0, 0, true)]
        [DataRow(12, 0, 0, 0, false)]
        [DataRow(12, 0, 1, 900, false)]
        [DataRow(11, 59, 59, 999, true)]
        [DataRow(23, 12, 12, 300, false)]
        public void LessThen_CompareToTimeAtTwelve_TrueIfSmallerFalseIfGreaterOrEqual(int hour, int minute, int second, int millisecond,
            bool expectedResult)
        {
            var timeTested = new Time((byte)hour, (byte)minute, (byte)second, millisecond);
            var timeCompared = new Time(12, 0, 0);

            var result = timeTested < timeCompared;

            Assert.AreEqual(result, expectedResult);

        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow(12, 0, 0, 0, true)]
        [DataRow(12, 0, 1, 0, false)]
        [DataRow(11, 59, 59, 999, true)]
        [DataRow(0, 0, 59, 302, true)]
        [DataRow(22, 23, 0, 459, false)]
        public void SmallerOrEqualTo_CompareToTimeAtTwelve_TrueIfSmallerOrEqualsFalseIfGreater(int hour, int minute,
            int second, int millisecond, bool expectedResult)
        {
            var timeTested = new Time((byte)hour, (byte)minute, (byte)second, millisecond);
            var timeCompared = new Time(12, 0, 0, 0);

            var result = timeTested <= timeCompared;

            Assert.AreEqual(result, expectedResult);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow("12:00:00.000", "01:00:00.000", "13:00:00.000")]
        [DataRow("12:00:00.000", "12:00:00.000", "00:00:00.000")]
        [DataRow("12:00:00.500", "24:12:12.000", "12:12:12.500")]
        [DataRow("12:00:00.000", "72:00:00.000", "12:00:00.000")]
        [DataRow("12:00:00.000", "15:59:59.999", "03:59:59.999")]
        [DataRow("12:59:59.000", "00:00:2.500", "13:00:01.500")]
        [DataRow("15:30:00.251", "02:35:30.249", "18:05:30.500")]
        public void PlusSign_AddTimePeriodToTime_ReturnsCalculatedTime(string timeInput, string period, string expectedResult)
        {
            var time = new Time(timeInput);
            var timePeriod = new TimePeriod(period);

            var result = time + timePeriod;

            Assert.AreEqual(result.ToString(), expectedResult);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow("12:00:00.500", "01:00:00.500", "11:00:00.000")]
        [DataRow("12:00:00.370", "12:00:00.000", "00:00:00.370")]
        [DataRow("12:00:00.000", "24:12:12.500", "11:47:47.500")]
        [DataRow("12:00:00.000", "72:00:00.000", "12:00:00.000")]
        [DataRow("12:00:01.000", "15:59:59.001", "20:00:01.999")]
        [DataRow("00:00:01.100", "00:00:02.100", "23:59:59.000")]
        [DataRow("15:30:00.999", "02:35:30.500", "12:54:30.499")]
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
            var timeOne = new Time(12, 0, 0, 001);
            var timeTwo = new Time(12, 0, 0, 001);

            var result = timeOne.Equals(timeTwo);

            Assert.AreEqual(result, true);
        }

        [TestMethod, TestCategory("Equals")]
        public void Equals_OtherHour_False()
        {
            var timeOne = new Time(12, 30, 59, 300);
            var timeTwo = new Time(23, 12, 00, 100);

            var result = timeOne.Equals(timeTwo);

            Assert.AreEqual(result, false);
        }

        [TestMethod, TestCategory("Equals")]
        public void StaticEquals_SameHour_True()
        {
            var timeOne = new Time(12, 30, 40, 399);
            var timeTwo = new Time(12, 30, 40, 399);

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

        [DataTestMethod, TestCategory("Operators")]
        [DataRow("12:00:00.000", "01:00:00.000", "13:00:00.000")]
        [DataRow("12:00:00.000", "12:00:00.000", "00:00:00.000")]
        [DataRow("12:00:00.500", "24:12:12.000", "12:12:12.500")]
        [DataRow("12:00:00.000", "72:00:00.000", "12:00:00.000")]
        [DataRow("12:00:00.000", "15:59:59.999", "03:59:59.999")]
        [DataRow("12:59:59.000", "00:00:2.500", "13:00:01.500")]
        [DataRow("15:30:00.251", "02:35:30.249", "18:05:30.500")]
        public void Plus_AddTimePeriodToTime_ReturnsCalculatedTime(string timeInput, string period, string expectedResult)
        {
            var time = new Time(timeInput);
            var timePeriod = new TimePeriod(period);

            var result = time.Plus(timePeriod);

            Assert.AreEqual(result.ToString(), expectedResult);
        }

        [DataTestMethod, TestCategory("Operators")]
        [DataRow("12:00:00.500", "01:00:00.500", "11:00:00.000")]
        [DataRow("12:00:00.370", "12:00:00.000", "00:00:00.370")]
        [DataRow("12:00:00.000", "24:12:12.500", "11:47:47.500")]
        [DataRow("12:00:00.000", "72:00:00.000", "12:00:00.000")]
        [DataRow("12:00:01.000", "15:59:59.001", "20:00:01.999")]
        [DataRow("00:00:01.100", "00:00:02.100", "23:59:59.000")]
        [DataRow("15:30:00.999", "02:35:30.500", "12:54:30.499")]
        public void Minus_SubtractTimePeriodFromTime_ReturnsCalculatedTime(string timeInput, string period, string expectedResult)
        {
            var time = new Time(timeInput);
            var timePeriod = new TimePeriod(period);

            var result = time.Minus(timePeriod);

            Assert.AreEqual(result.ToString(), expectedResult);
        }

        #endregion

        #region ToString

        [DataTestMethod, TestCategory("ToString")]
        [DataRow(1, 1, 1,999, "01:01:01.999")]
        [DataRow(12, 0, 0, 1, "12:00:00.001")]
        [DataRow(23, 59, 0, 10,"23:59:00.010")]
        [DataRow(0, 0, 0,11, "00:00:00.011")]
        [DataRow(15, 59, 59, 0, "15:59:59.000")]
        public void ToString_DifferentValues_AlwaysReturnStringInSameFormat(int hour, int minute, int second, int millisecond,
            string expectedStringRepresentation)
        {
            var time = new Time((byte)hour, (byte)minute, (byte)second, millisecond);

            Assert.AreEqual(time.ToString(), expectedStringRepresentation);
        }

        #endregion
    }

}
