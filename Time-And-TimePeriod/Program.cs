using System;
using Time_And_TimePeriod_Lib;

namespace Time_And_TimePeriod
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Time("06:59:50");
            var t2 = new Time("06:10:00");
            Console.WriteLine(t == t2);

            var test = new TimePeriod(t, t2);
            Console.WriteLine(test);
        }
    }
}
