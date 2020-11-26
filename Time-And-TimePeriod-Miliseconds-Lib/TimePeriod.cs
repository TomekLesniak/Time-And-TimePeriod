using System;
using System.Collections.Generic;
using System.Text;

namespace Time_And_TimePeriod_Milliseconds_Lib
{
    /// <summary>
    /// <c>TimePeriod</c> represents time interval between two 'points' of time
    /// </summary>
    public readonly struct TimePeriod //: IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private readonly double _seconds;

        /// <summary>
        /// Amount of hours interval represented by this instance
        /// </summary>
        public int Hours => (int) (_seconds / 3600);

        /// <summary>
        /// Amount of minutes interval represented by this instance
        /// </summary>
        public int Minutes => (int) ((_seconds - Hours * 3600) / 60);

        /// <summary>
        /// Amount of seconds interval represented by this instance
        /// </summary>
        public int Seconds => (int) (_seconds - Minutes * 60 - Hours * 3600);

        public double Milliseconds => Math.Round(_seconds - (int)_seconds, 4) * 1000;

        /// <summary>
        /// Initializes a new instance of TimePeriod struct.
        /// </summary>
        /// <param name="seconds">Amount of seconds comma milliseconds</param>
        public TimePeriod(double seconds)
        {
            if (seconds < 0)
                throw new ArgumentOutOfRangeException();

            _seconds = seconds;
        }

        /// <summary>
        /// Initializes a new instance of TimePeriod struct
        /// </summary>
        /// <param name="hours">Amount of hours</param>
        /// <param name="minutes">Amount of minutes (0-59)</param>
        public TimePeriod(int hours, int minutes) : this(hours, minutes, 0.0)
        {
        }


        /// <summary>
        /// Initializes a new instance of TimePeriod struct
        /// </summary>
        /// <param name="hours">Amount of hours</param>
        /// <param name="minutes">Amount of minutes (0-59)</param>
        /// <param name="seconds">Amount of seconds (0.000-59.999)</param>
        public TimePeriod(int hours, int minutes, double seconds)
        {
            if (hours < 0 || minutes < 0 || seconds < 0 || minutes >= 60 || seconds >= 60.0)
                throw new ArgumentOutOfRangeException();

            var totalSeconds = hours * 3600 + minutes * 60 + seconds;
            _seconds = totalSeconds;
        }

        /// <summary>
        /// Initializes a new instance of TimePeriod struct
        /// </summary>
        /// <param name="timeOne">Time instance</param>
        /// <param name="timeTwo">Time instance</param>
        public TimePeriod(Time timeOne, Time timeTwo)
        {
            long timeOneSeconds = timeOne.Hours * 3600 + timeOne.Minutes * 60 + timeOne.Seconds;
            long timeTwoSeconds = timeTwo.Hours * 3600 + timeTwo.Minutes * 60 + timeTwo.Seconds;

            var timeDifference = Math.Abs(timeOneSeconds - timeTwoSeconds);
            _seconds = timeDifference;
        }

        /// <summary>
        /// Initializes a new instance of TimePeriod struct
        /// </summary>
        /// <param name="timePeriod">String formatted in "##hh:mm:ss.fff</param>
        /// <example>123:1:23 is valid time period format as well</example>
        public TimePeriod(string timePeriod)
        {
            int hours, minutes, seconds;
            try
            {
                // I assumed that it is not required to write additional 0 in timePeriod input between(0-9),
                // so input like 12:1:30 is valid whilst still will be printed as :01:
                var split = timePeriod.Split(':');
                hours = int.Parse(split[0]);
                minutes = int.Parse(split[1]);
                seconds = int.Parse(split[2]);
            }
            catch
            {
                throw new FormatException("Invalid TimePeriod format");
            }

            if (hours < 0 || minutes < 0 || seconds < 0 || minutes >= 60 || seconds >= 60)
                throw new ArgumentOutOfRangeException();

            var totalSeconds = hours * 3600 + minutes * 60 + seconds;
            _seconds = totalSeconds;
        }

        /// <summary>
        /// String representation of TimePeriod
        /// </summary>
        /// <returns>String formatted in ##hh:mm:ss</returns>
        public override string ToString() => $"{Hours:##00}:{Minutes:00}:{Seconds:00}.{Milliseconds:000}";
    }
}