using System;
using Time_And_TimePeriod_Lib;

namespace Time_And_TimePeriod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n======== Time ========");
            var timeOneParam = new Time(12);
            var timeTwoParams = new Time(23, 59);
            var timeThreeParams = new Time(6, 12, 59);
            var timeString = new Time("15:05:29");

            Console.WriteLine(timeOneParam);
            Console.WriteLine(timeTwoParams);
            Console.WriteLine(timeThreeParams);
            Console.WriteLine(timeString);

            var timeLater = new Time(20);
            var timeEarlier = new Time(10, 30);

            Console.WriteLine($"\n{timeLater} > {timeEarlier} : {timeLater > timeEarlier}");
            Console.WriteLine($"{timeLater} >= {timeEarlier} : {timeLater >= timeEarlier}");
            Console.WriteLine($"{timeLater} < {timeEarlier} : {timeLater < timeEarlier}");
            Console.WriteLine($"{timeLater} <= {timeEarlier} : {timeLater <= timeEarlier}");
            Console.WriteLine($"{timeLater} == {timeEarlier} : {timeLater == timeEarlier}");
            Console.WriteLine($"{timeLater} != {timeEarlier} : {timeLater != timeEarlier}");

            var fiveHoursTimePeriod = new TimePeriod(5, 0);
            Console.WriteLine($"\n{timeLater} + {fiveHoursTimePeriod} = {timeLater + fiveHoursTimePeriod}");
            Console.WriteLine($"{timeLater} - {fiveHoursTimePeriod} = {timeLater - fiveHoursTimePeriod}");


            Console.WriteLine("\n======== TimePeriod ========");

            var timePeriodOneParam = new TimePeriod(600);
            var timePeriodTwoParams = new TimePeriod(24, 30);
            var timePeriodThreeParams = new TimePeriod(30, 20, 10);
            var timePeriodTwoTimeObjects = new TimePeriod(timeEarlier, timeLater);
            var timePeriodString = new TimePeriod("24:24:24");

            Console.WriteLine(timePeriodOneParam);
            Console.WriteLine(timePeriodTwoParams);
            Console.WriteLine(timePeriodThreeParams);
            Console.WriteLine(timePeriodTwoTimeObjects);
            Console.WriteLine(timePeriodString);

            var timePeriodLonger = new TimePeriod(86400);
            var timePeriodShorter = new TimePeriod(35000);

            Console.WriteLine($"\n{timePeriodLonger} > {timePeriodShorter} : {timePeriodLonger > timePeriodShorter}");
            Console.WriteLine($"{timePeriodLonger} >= {timePeriodShorter} : {timePeriodLonger >= timePeriodShorter}");
            Console.WriteLine($"{timePeriodLonger} < {timePeriodShorter} : {timePeriodLonger < timePeriodShorter}");
            Console.WriteLine($"{timePeriodLonger} <= {timePeriodShorter} : {timePeriodLonger <= timePeriodShorter}");
            Console.WriteLine($"{timePeriodLonger} == {timePeriodShorter} : {timePeriodLonger == timePeriodShorter}");
            Console.WriteLine($"{timePeriodLonger} != {timePeriodShorter} : {timePeriodLonger != timePeriodShorter}");


            Console.WriteLine($"\n{timePeriodLonger} + {timePeriodShorter} = {timePeriodLonger + timePeriodShorter}");
            Console.WriteLine($"{timePeriodLonger} - {timePeriodShorter} = {timePeriodLonger - timePeriodShorter}");
            Console.WriteLine($"{timePeriodLonger} * 2 = {timePeriodLonger * 2}");
            Console.WriteLine($"{timePeriodLonger} / 2 = {timePeriodLonger / 2}");
        }
    }
}
