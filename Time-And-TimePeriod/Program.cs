using System;
using Time_And_TimePeriod_Lib;

namespace Time_And_TimePeriod
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Time("00:20:49");
            var t2 = new Time("00:20:49");
            Console.WriteLine(t.Equals(t2));
        }
    }
}
