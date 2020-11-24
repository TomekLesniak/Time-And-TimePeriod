using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;

namespace Time_And_TimePeriod_Lib
{
    public readonly struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private readonly long _seconds;

        public int Hours => (int)(_seconds / 3600);
        public int Minutes => (int)((_seconds - Hours * 3600) / 60);
        public int Seconds => (int)(_seconds - Minutes * 60 - Hours * 3600);

        public TimePeriod(long seconds)
        {
            if(seconds < 0)
                throw new ArgumentOutOfRangeException();

            _seconds = seconds;
        }

        public TimePeriod(int hours, int minutes) : this(hours, minutes, 0) { }

        public TimePeriod(int hours, int minutes, int seconds)
        {
            if(hours < 0 || minutes < 0 || seconds < 0 || minutes >= 60 || seconds >= 60) 
                throw new ArgumentOutOfRangeException();

            var totalSeconds = hours * 3600 + minutes * 60 + seconds;
            _seconds = totalSeconds;
        }

        public TimePeriod(Time timeOne, Time timeTwo)
        {
            long timeOneSeconds = timeOne.Hours * 3600 + timeOne.Minutes * 60 + timeOne.Seconds;
            long timeTwoSeconds = timeTwo.Hours * 3600 + timeTwo.Minutes * 60 + timeTwo.Seconds;

            var timeDifference = Math.Abs(timeOneSeconds - timeTwoSeconds);
            _seconds = timeDifference;
        }

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

        public override string ToString()
        {
            return $"{Hours:###00}:{Minutes:00}:{Seconds:00}";
        }

        public bool Equals(TimePeriod other) => _seconds == other._seconds;
        public override bool Equals(object obj) => obj is TimePeriod other && Equals(other);
        public override int GetHashCode() => _seconds.GetHashCode();

        public static bool operator ==(TimePeriod first, TimePeriod second) => first.Equals(second);
        public static bool operator !=(TimePeriod first, TimePeriod second) => !first.Equals(second);
        public static bool operator >(TimePeriod first, TimePeriod second) => first.CompareTo(second) > 0;
        public static bool operator >=(TimePeriod first, TimePeriod second) => first.CompareTo(second) >= 0;
        public static bool operator <(TimePeriod first, TimePeriod second) => first.CompareTo(second) < 0;
        public static bool operator <=(TimePeriod first, TimePeriod second) => first.CompareTo(second) <= 0;
      
        public int CompareTo(TimePeriod other) => _seconds.CompareTo(other._seconds);

        public static TimePeriod operator +(TimePeriod first, TimePeriod second) => Plus(first, second);
        public static TimePeriod operator -(TimePeriod first, TimePeriod second) => Minus(first, second);
        public static TimePeriod operator *(TimePeriod timePeriod, int multiplier) => Multiply(timePeriod, multiplier);
        public static TimePeriod operator /(TimePeriod timePeriod, int divider) => Divide(timePeriod, divider);

        public TimePeriod Plus(TimePeriod other) => Plus(this, other);

        public static TimePeriod Plus(TimePeriod first, TimePeriod second)
        {
            long secondsOfTwoTimePeriods = first._seconds + second._seconds;
            return new TimePeriod(secondsOfTwoTimePeriods);
        }

        public TimePeriod Minus(TimePeriod other) => Minus(this, other);

        public static TimePeriod Minus(TimePeriod first, TimePeriod second)
        {
            long secondsBetweenTwoTimePeriods = first._seconds - second._seconds;
            return new TimePeriod(secondsBetweenTwoTimePeriods);
        }

        public TimePeriod Multiply(int multiplier) => Multiply(this, multiplier);

        public static TimePeriod Multiply(TimePeriod timePeriod, int multiplier)
        {
            if(multiplier < 0)
                throw new ArgumentOutOfRangeException("multiplier", "Can`t multiply timeperiod by negative number");

            var secondsMultiplied = timePeriod._seconds * multiplier;
            return new TimePeriod(secondsMultiplied);
        }

        public TimePeriod Divide(int divider) => Divide(this, divider);

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
