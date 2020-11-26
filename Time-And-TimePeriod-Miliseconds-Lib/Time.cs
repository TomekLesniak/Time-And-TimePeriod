﻿using System;

namespace Time_And_TimePeriod_Milliseconds_Lib
{
    /// <summary>
    /// <c>Time</c> is a simple representation of time.
    /// Contains basic methods to work with Time and TimePeriod
    /// </summary>
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {
        /// <summary>
        /// Get the hour of the time represented by that instance.
        /// </summary>
        public byte Hours { get; }

        /// <summary>
        /// Get the minute of the time represented by that instance
        /// </summary>
        public byte Minutes { get; }

        /// <summary>
        /// Get the second of the time represented by that instance
        /// </summary>
        public byte Seconds { get; }

        /// <summary>
        /// Get the millisecond of the time represented by that instance
        /// </summary>
        public int Milliseconds { get; }

        /// <summary>
        /// Initializes a new instance of Time struct.
        /// </summary>
        /// <param name="hours">Hour (0-23)</param>
        /// <param name="minutes">Minutes (0-59)</param>
        /// <param name="seconds">Seconds (0-59)</param>
        public Time(byte hours = 0, byte minutes = 0, byte seconds = 0, int milliseconds = 0)
        {
            if (hours >= 24 || minutes >= 60 || seconds >= 60 || milliseconds < 0 || milliseconds >= 1000)
                throw new FormatException("Invalid time format");

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }

        /// <summary>
        /// Initializes a new instance of Time struct
        /// </summary>
        /// <param name="time">Time representation formatted in hh:mm:ss.mmm</param>
        /// <example>12:1:23.999 is valid format as well</example>
        public Time(string time)
        {
            byte hours, minutes, seconds;
            int milliseconds;
            try
            {
                // I assumed that it is not required to write additional 0 in time input between(0-9),
                // so input like 12:1:30:450 is valid whilst still will be printed as :01:
                var hoursAndMinutes = time.Split(':');
                var secondsAndMilliSeconds = hoursAndMinutes[2].Split('.');
                hours = byte.Parse(hoursAndMinutes[0]);
                minutes = byte.Parse(hoursAndMinutes[1]);
                seconds = byte.Parse(secondsAndMilliSeconds[0]);
                milliseconds = int.Parse(secondsAndMilliSeconds[1]);
            }
            catch (OverflowException)
            {
                throw new OverflowException();
            }
            catch
            {
                throw new FormatException("Invalid string representation of Time");
            }

            if (hours >= 24 || minutes >= 60 || seconds >= 60 || milliseconds < 0 || milliseconds >= 1000)
                throw new ArgumentOutOfRangeException();

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
            Milliseconds = milliseconds;
        }

        public override int GetHashCode() => (Hours, Minutes, Seconds, Milliseconds).GetHashCode();

        /// <summary>
        /// String representation of time
        /// </summary>
        /// <returns>String formatted in hh:mm:ss.fff</returns>
        public override string ToString() => $"{Hours:00}:{Minutes:00}:{Seconds:00}.{Milliseconds:000}";

        /// <summary>
        /// Compare two time objects if they are 'pointing' at the same time
        /// </summary>
        /// <param name="other">Time object this object is compared to</param>
        /// <returns>True if both 'points' at the same time, false otherwise</returns>
        public bool Equals(Time other) => (Hours, Minutes, Seconds, Milliseconds) == (other.Hours, other.Minutes, other.Seconds, other.Milliseconds);

        public override bool Equals(object obj) => obj is Time time && Equals(time);

        /// <summary>
        /// Compare two time objects if they are 'pointing' at the same time
        /// </summary>
        /// <param name="t1">Time instance</param>
        /// <param name="t2">Time instance</param>
        /// <returns>True if both 'points' at the same time, false otherwise</returns>
        public static bool Equals(Time t1, Time t2) => t1.Equals(t2);

        /// <summary>
        /// Compare two time objects if they are 'pointing' at the same time
        /// </summary>
        /// <returns>True if both 'points' at the same time, false otherwise</returns>
        public static bool operator ==(Time t1, Time t2) => t1.Equals(t2);

        /// <summary>
        /// Compare two time objects if they are 'pointing' at different time
        /// </summary>
        /// <returns>True if time are different, false if equal</returns>
        public static bool operator !=(Time t1, Time t2) => !(t1 == t2);

        /// <summary>
        /// Compare if left hand side time is earlier than right hand side time
        /// </summary>
        /// <returns>True if earlier, false if equal or later</returns>
        public static bool operator <(Time t1, Time t2) => t1.CompareTo(t2) < 0;

        /// <summary>
        /// Compare if left hand side time is equal or earlier than the right hand side time
        /// </summary>
        /// <returns>True if earlier or equal, false if greater</returns>
        public static bool operator <=(Time t1, Time t2) => t1.CompareTo(t2) <= 0;

        /// <summary>
        /// Compare if left hand side time is later than the right hand side time 
        /// </summary>
        /// <returns>True if later, false if earlier or equal</returns>
        public static bool operator >(Time t1, Time t2) => t1.CompareTo(t2) > 0;

        /// <summary>
        /// Compare if left hand side time is equal or later than the right hand side time
        /// </summary>
        /// <returns>True if later or equal, false if earlier</returns>
        public static bool operator >=(Time t1, Time t2) => t1.CompareTo(t2) >= 0;

        /// <summary>
        /// Compare two objects and determine which one is greater.
        /// Greater is the one with bigger values, starting from hour, then minutes,
        /// then seconds, then milliseconds
        /// </summary>
        /// <param name="other">Time object this object is compared to</param>
        /// <returns>Negative integer if smaller, 0 if equal, positive if greater</returns>
        public int CompareTo(Time other)
        {
            var hoursComparison = Hours.CompareTo(other.Hours);
            if (hoursComparison != 0) return hoursComparison;
            var minutesComparison = Minutes.CompareTo(other.Minutes);
            if (minutesComparison != 0) return minutesComparison;
            var secondsComparison = Seconds.CompareTo(other.Seconds);
            if (secondsComparison != 0) return secondsComparison;
            return Milliseconds.CompareTo(other.Milliseconds);
        }


        /// <summary>
        /// Add given period of time to Time instance
        /// </summary>
        /// <param name="time">Time instance</param>
        /// <param name="timePeriod">TimePeriod instance</param>
        /// <returns>New calculated time instance</returns>
        public static Time operator +(Time time, TimePeriod timePeriod) => Plus(time, timePeriod);

        /// <summary>
        /// Subtract given period of time from Time instance
        /// </summary>
        /// <param name="time">Time instance</param>
        /// <param name="timePeriod">TimePeriod instance</param>
        /// <returns>New calculated time instance</returns>
        public static Time operator -(Time time, TimePeriod timePeriod) => Minus(time, timePeriod);

        /// <summary>
        /// Add given period of time to Time instance
        /// </summary>
        /// <param name="timePeriod">TimePeriod instance</param>
        /// <returns>New calculated time instance</returns>
        public Time Plus(TimePeriod timePeriod) => Plus(this, timePeriod);

        /// <summary>
        /// Add given period of time to Time instance
        /// </summary>
        /// <param name="time">Time instance</param>
        /// <param name="timePeriod">TimePeriod instance</param>
        /// <returns>New calculated time instance</returns>
        public static Time Plus(Time time, TimePeriod timePeriod)
        {
            var hours = time.Hours + timePeriod.Hours;
            var minutes = time.Minutes + timePeriod.Minutes;
            var seconds = time.Seconds + timePeriod.Seconds;
            var milliseconds = time.Milliseconds + timePeriod.Milliseconds;

            if (milliseconds >= 1000)
            {
                milliseconds %= 1000;
                seconds++;
            }

            if (seconds >= 60)
            {
                seconds %= 60;
                minutes++;
            }

            if (minutes >= 60)
            {
                minutes %= 60;
                hours++;
            }

            hours %= 24;
            return new Time((byte)hours, (byte)minutes, (byte)seconds, milliseconds);
        }

        /// <summary>
        /// Subtract given period of time from Time instance
        /// </summary>
        /// <param name="timePeriod">TimePeriod instance</param>
        /// <returns>New calculated time instance</returns>
        public Time Minus(TimePeriod timePeriod) => Minus(this, timePeriod);

        /// <summary>
        /// Subtract given period of time from Time instance
        /// </summary>
        /// <param name="time">Time instance</param>
        /// <param name="timePeriod">TimePeriod instance</param>
        /// <returns>New calculated time instance</returns>
        public static Time Minus(Time time, TimePeriod timePeriod)
        {
            var hours = (time.Hours - timePeriod.Hours) % 24;
            var minutes = time.Minutes - timePeriod.Minutes;
            var seconds = time.Seconds - timePeriod.Seconds;
            var milliseconds = time.Milliseconds - timePeriod.Milliseconds;

            while (milliseconds < 0)
            {
                milliseconds += 1000;
                seconds--;
            }

            while (seconds < 0)
            {
                seconds += 60;
                minutes--;
            }

            while (minutes < 0)
            {
                minutes += 60;
                hours--;
            }

            if (hours < 0)
                hours += 24;

            return new Time((byte)hours, (byte)minutes, (byte)seconds, milliseconds);
        }

    }
}
