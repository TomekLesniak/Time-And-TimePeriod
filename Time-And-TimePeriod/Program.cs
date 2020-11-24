using System;
using Time_And_TimePeriod_Lib;

namespace Time_And_TimePeriod
{
    class Program
    {
        static void Main(string[] args)
        {
            var time = new Time("02:10:00");
            var test2 = new TimePeriod("03:05:59");
            Console.WriteLine(time - test2);
        }
    }
}
