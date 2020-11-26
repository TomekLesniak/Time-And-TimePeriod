using System;

namespace Time_And_TimePeriod_Lib.Basic
{
    /// <summary>
    /// <c>TimePeriod</c> represents time interval between two 'points' of time
    /// </summary>
    public readonly struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private readonly long _seconds;

        /// <summary>
        /// Amount of hours interval represented by this instance
        /// </summary>
        public int Hours => (int)(_seconds / 3600);
        /// <summary>
        /// Amount of minutes interval represented by this instance
        /// </summary>
        public int Minutes => (int)((_seconds - Hours * 3600) / 60);
        /// <summary>
        /// Amount of seconds interval represented by this instance
        /// </summary>
        public int Seconds => (int)(_seconds - Minutes * 60 - Hours * 3600);

        /// <summary>
        /// Initializes a new instance of TimePeriod struct.
        /// </summary>
        /// <param name="seconds">Amount of seconds</param>
        public TimePeriod(long seconds)
        {
            if(seconds < 0)
                throw new ArgumentOutOfRangeException();

            _seconds = seconds;
        }

        /// <summary>
        /// Initializes a new instance of TimePeriod struct
        /// </summary>
        /// <param name="hours">Amount of hours</param>
        /// <param name="minutes">Amount of minutes (0-59)</param>
        public TimePeriod(int hours, int minutes) : this(hours, minutes, 0) { }


        /// <summary>
        /// Initializes a new instance of TimePeriod struct
        /// </summary>
        /// <param name="hours">Amount of hours</param>
        /// <param name="minutes">Amount of minutes (0-59)</param>
        /// <param name="seconds">Amount of seconds (0-59)</param>
        public TimePeriod(int hours, int minutes, int seconds)
        {
            if(hours < 0 || minutes < 0 || seconds < 0 || minutes >= 60 || seconds >= 60) 
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
        /// <param name="timePeriod">String formatted in "##hh:mm:ss</param>
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
            
            if(hours < 0 || minutes < 0 || seconds < 0 || minutes >= 60 || seconds >= 60)
                throw new ArgumentOutOfRangeException();

            var totalSeconds = hours * 3600 + minutes * 60 + seconds;
            _seconds = totalSeconds;
        }

        /// <summary>
        /// String representation of TimePeriod
        /// </summary>
        /// <returns>String formatted in ##hh:mm:ss</returns>
        public override string ToString() => $"{Hours:##00}:{Minutes:00}:{Seconds:00}";

        /// <summary>
        /// Compare two time periods if they have same time interval
        /// </summary>
        /// <param name="other">TimePeriod instance</param>
        /// <returns>True if both interval are the same, false otherwise</returns>
        public bool Equals(TimePeriod other) => _seconds == other._seconds;
        public override bool Equals(object obj) => obj is TimePeriod other && Equals(other);
        public override int GetHashCode() => _seconds.GetHashCode();

        /// <summary>
        /// Compare two time periods if they have same time interval
        /// </summary>
        /// <returns>True if both interval are the same, false otherwise</returns>
        public static bool operator ==(TimePeriod first, TimePeriod second) => first.Equals(second);
        
        /// <summary>
        /// Compare two time periods if they have different time interval
        /// </summary>
        /// <returns>True if intervals are different, false otherwise</returns>
        public static bool operator !=(TimePeriod first, TimePeriod second) => !first.Equals(second);
        
        /// <summary>
        /// Compare if left hand side time period is longer than the right hand side time period
        /// </summary>
        /// <returns>True if longer, false if equal or shorter</returns>
        public static bool operator >(TimePeriod first, TimePeriod second) => first.CompareTo(second) > 0;
        
        /// <summary>
        /// Compare if left hand side time period is equal or longer than the right hand side time period
        /// </summary>
        /// <returns>True if longer or equal, false if shorter</returns>
        public static bool operator >=(TimePeriod first, TimePeriod second) => first.CompareTo(second) >= 0;
       
        /// <summary>
        /// Compare if left hand side time period is shorter than the right hand side time period
        /// </summary>
        /// <returns>True if shorter, false if equal or longer</returns>
        public static bool operator <(TimePeriod first, TimePeriod second) => first.CompareTo(second) < 0;
        
        /// <summary>
        /// Compare if left hand side time period is equal or shorter than the right hand side time period
        /// </summary>
        /// <returns>True if shorter or equal, false if longer</returns>
        public static bool operator <=(TimePeriod first, TimePeriod second) => first.CompareTo(second) <= 0;
      
        /// <summary>
        /// Compare two objects and determine which one is longer.
        /// Longer is the one with bigger values, starting from hours, then minutes, then seconds 
        /// </summary>
        /// <param name="other">TimePeriod instance</param>
        /// <returns>Negative integer if shorter, 0 if equal, positive if longer</returns>
        public int CompareTo(TimePeriod other) => _seconds.CompareTo(other._seconds);

        /// <summary>
        /// Add two given time periods
        /// </summary>
        /// <returns>New calculated time period instance</returns>
        public static TimePeriod operator +(TimePeriod first, TimePeriod second) => Plus(first, second);
        
        /// <summary>
        /// Subtract two given time periods
        /// </summary>
        /// <returns>New calculated time period instance</returns>
        public static TimePeriod operator -(TimePeriod first, TimePeriod second) => Minus(first, second);
        
        /// <summary>
        /// Multiply given time period by integer value
        /// </summary>
        /// <returns>New calculated time period instance</returns>
        public static TimePeriod operator *(TimePeriod timePeriod, int multiplier) => Multiply(timePeriod, multiplier);
        
        /// <summary>
        /// Divide given time period by integer value
        /// </summary>
        /// <returns>New calculated time period instance</returns>
        public static TimePeriod operator /(TimePeriod timePeriod, int divider) => Divide(timePeriod, divider);

        /// <summary>
        /// Add two given time periods
        /// </summary>
        /// <param name="other">TimePeriod instance</param>
        /// <returns>New calculated time period instance</returns>
        public TimePeriod Plus(TimePeriod other) => Plus(this, other);

        /// <summary>
        /// Add two given time periods
        /// </summary>
        /// <param name="first">TimePeriod instance</param>
        /// <param name="second">TimePeriod instance</param>
        /// <returns>New calculated time period instance</returns>
        public static TimePeriod Plus(TimePeriod first, TimePeriod second)
        {
            long secondsOfTwoTimePeriods = first._seconds + second._seconds;
            return new TimePeriod(secondsOfTwoTimePeriods);
        }

        /// <summary>
        /// Subtract given time period from this instance
        /// </summary>
        /// <param name="other">TimePeriod instance</param>
        /// <returns>New calculated time period instance</returns>
        public TimePeriod Minus(TimePeriod other) => Minus(this, other);

        /// <summary>
        /// Subtract two given time periods
        /// </summary>
        /// <param name="first">TimePeriod instance</param>
        /// <param name="second">TimePeriod instance</param>
        /// <returns>New calculated time period instance</returns>
        public static TimePeriod Minus(TimePeriod first, TimePeriod second)
        {
            long secondsBetweenTwoTimePeriods = first._seconds - second._seconds;
            return new TimePeriod(secondsBetweenTwoTimePeriods);
        }

        /// <summary>
        /// Multiply given time period by integer value
        /// </summary>
        /// <returns>New calculated time period instance</returns>
        public TimePeriod Multiply(int multiplier) => Multiply(this, multiplier);

        /// <summary>
        /// Multiply given time period by integer value
        /// </summary>
        /// <returns>New calculated time period instance</returns>
        public static TimePeriod Multiply(TimePeriod timePeriod, int multiplier)
        {
            if(multiplier < 0)
                throw new ArgumentOutOfRangeException("multiplier", "Can`t multiply timeperiod by negative number");

            var secondsMultiplied = timePeriod._seconds * multiplier;
            return new TimePeriod(secondsMultiplied);
        }

        /// <summary>
        /// Divide given time period by integer value
        /// </summary>
        /// <returns>New calculated time period instance</returns>
        public TimePeriod Divide(int divider) => Divide(this, divider);

        /// <summary>
        /// Divide given time period by integer value
        /// </summary>
        /// <returns>New calculated time period instance</returns>
        public static TimePeriod Divide(TimePeriod timePeriod, int divider)
        {
            if(divider == 0)
                throw new DivideByZeroException("Can not divide by 0");
            if(divider < 0)
                throw new ArgumentOutOfRangeException("divider", "Can`t divide timePeriod by negative number");

            var secondsDivided = timePeriod._seconds / divider;
            return new TimePeriod(secondsDivided);
        }
    }
}
