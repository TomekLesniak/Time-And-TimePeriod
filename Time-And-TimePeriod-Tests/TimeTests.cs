using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Time_And_TimePeriod_Lib;

namespace Time_And_TimePeriod_Tests
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
        [DataRow("24:00:00")]
        [DataRow("24:1:10")]
        [DataRow("-1:1:10")]
        [DataRow("1:60:10")]
        [DataRow("1:59:60")]
        [DataRow("59:59:59")]
        [ExpectedException(typeof(FormatException))]
        public void Constructor_InvalidString_ThrowsFormatException(string input)
        {
            var time = new Time(input);
        }

        #endregion

        #region Operators

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


        [DataTestMethod]
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
    }
}
