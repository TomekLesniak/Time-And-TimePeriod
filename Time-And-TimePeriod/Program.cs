using System;
using Time_And_TimePeriod_Lib;

namespace Time_And_TimePeriod
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Time("00:20:50");
            var t2 = new Time("00:20:50");
            Console.WriteLine(t >= t2);

            Console.WriteLine(t.Equals(null));
            var x = Time.Equals(t, t2);
            Console.WriteLine(x);
            object.Equals(t, t2);
        }
    }
}
