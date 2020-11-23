using System;
using System.Collections.Generic;
using System.Text;

namespace Time_And_TimePeriod_Lib
{
    public readonly struct TimePeriod
    {
        private readonly long _seconds;

        public int Hours => (int)_seconds / 3600;
        public int Minutes => (int)(_seconds - Hours * 3600) / 60;
        public int Seconds => (int)(_seconds - Minutes * 60 - Hours * 3600);

        public TimePeriod(long seconds) : this(0, 0, seconds) { }

        public TimePeriod(int hours, int minutes) : this(hours, minutes, 0) { }

        public TimePeriod(int hours, int minutes, long seconds)
        {
            if(hours < 0 || minutes < 0 || seconds < 0)
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

        public override string ToString()
        {
            return $"{Hours:##00}:{Minutes:00}:{Seconds:00}";
        }
    }
}
