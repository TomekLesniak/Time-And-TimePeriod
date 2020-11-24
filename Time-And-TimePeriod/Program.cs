using System;
using Time_And_TimePeriod_Lib;

namespace Time_And_TimePeriod
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Time("-1:59:50");
            var t2 = new Time("06:10:00");
            Console.WriteLine(t == t2);

            var test = new TimePeriod("23:00:00");
            var test2 = new TimePeriod("00:59:59");
            Console.WriteLine(test2 - test);
        }
    }
}
