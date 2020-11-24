using System;
using Time_And_TimePeriod_Lib;

namespace Time_And_TimePeriod
{
    class Program
    {
        static void Main(string[] args)
        {
            var time = new Time("02:10:00");
            var time2 = new Time("02:00:00");

            var x = new TimePeriod(time2, time);
            Console.WriteLine(x);
        }
    }
}
