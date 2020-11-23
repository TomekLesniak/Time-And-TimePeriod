using System;
using System.Collections.Generic;
using System.Text;

namespace Time_And_TimePeriod_Lib
{
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {
        public byte Hours { get; }
        public byte Minutes { get; }
        public byte Seconds { get; }

        public Time(byte hours = 0, byte minutes = 0, byte seconds = 0)
        {
            if (hours >= 24 || minutes >= 60 ||  seconds >= 60)
                throw new FormatException("Invalid time format");

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public Time(string time)
        {
            byte hours, minutes, seconds;
            try
            {
                var split = time.Split(':');
                hours = byte.Parse(split[0]);
                minutes = byte.Parse(split[1]);
                seconds = byte.Parse(split[2]);
            }
            catch
            {
                throw new FormatException("Invalid string representation of Time");
            }

            if (hours >= 24 || minutes >= 60 || seconds >= 60)
                throw new FormatException("Invalid time format");

            Hours = hours;
            Minutes = minutes;
            Seconds = seconds;
        }

        public override int GetHashCode() => (Hours, Minutes, Seconds).GetHashCode();
        public override string ToString() => $"{Hours:00}:{Minutes:00}:{Seconds:00}";

        public bool Equals(Time other) => (Hours, Minutes, Seconds) == (other.Hours, other.Minutes, other.Seconds);
        public override bool Equals(object obj) => obj is Time time && Equals(time);
        public static bool Equals(Time t1, Time t2) => t1.Equals(t2);

        public static bool operator ==(Time t1, Time t2) => t1.Equals(t2);
        public static bool operator !=(Time t1, Time t2) => !(t1 == t2);
        public static bool operator <(Time t1, Time t2) => t1.CompareTo(t2) < 0;
        public static bool operator <=(Time t1, Time t2) => t1.CompareTo(t2) <= 0;
        public static bool operator >(Time t1, Time t2) => t1.CompareTo(t2) > 0;
        public static bool operator >=(Time t1, Time t2) => t1.CompareTo(t2) >= 0;

        public int CompareTo(Time other)
        {
            var hoursComparison = Hours.CompareTo(other.Hours);
            if (hoursComparison != 0) return hoursComparison;
            var minutesComparison = Minutes.CompareTo(other.Minutes);
            if (minutesComparison != 0) return minutesComparison;
            return Seconds.CompareTo(other.Seconds);
        }
    } 
}
