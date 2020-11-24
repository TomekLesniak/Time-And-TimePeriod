using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
