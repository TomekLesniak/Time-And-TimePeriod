using System;
using System.Collections.Generic;
using System.Text;

namespace Time_And_TimePeriod_Lib
{
    public struct Time : IEquatable<Time>
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

        public bool Equals(Time other)
        {
            return Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;
        }

        public override string ToString()
        {
            var format = "00";
            return $"{Hours.ToString(format)}:{Minutes.ToString(format)}:{Seconds.ToString(format)}";
        }
    } 
}
