using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using Time_And_TimePeriod_Lib.Basic;
using ms = Time_And_TimePeriod_Milliseconds_Lib;

namespace Time_And_TimePeriod
{
    class Program
    {
        static void Main(string[] args)
        {
            SetCulture();

            Console.WriteLine("\n\tTIME & TIMEPERIOD BEZ MILISEKUND");
            PrzykladowaAplikacjaBezMilisekund();

            Console.WriteLine("\n\n\tTIME & TIMEPERIOD ROZSZERZONE O MILISEKUNDY");
            PrzykladowaAplikacjaRozszerzonaOMilisekundy();
        }

        private static void PrzykladowaAplikacjaBezMilisekund()
        {
            Console.WriteLine("\n======== Time ========");
            var timeOneParam = new Time(12);
            var timeTwoParams = new Time(23, 59);
            var timeThreeParams = new Time(6, 12, 59);
            var timeString = new Time("15:05:29");

            Console.WriteLine($"Konstruktor jeden argument (12): {timeOneParam}");
            Console.WriteLine($"Konstruktor dwa argumenty (23,59): {timeTwoParams}");
            Console.WriteLine($"Konstruktor trzy argumenty (6,12,59): {timeThreeParams}");
            Console.WriteLine($"Konstruktor string (15:05:29): {timeString}");

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

            Console.WriteLine($"Konstruktor jeden argument (600): {timePeriodOneParam}");
            Console.WriteLine($"Konstruktor dwa argumenty (24, 30): {timePeriodTwoParams}");
            Console.WriteLine($"Konstruktor trzy argumenty (30, 20, 10): {timePeriodThreeParams}");
            Console.WriteLine($"Konstruktor string (24:24:24): {timePeriodString}");
            Console.WriteLine($"Konstruktor dwa obiekty Time(10,30,0) (20,0,0) : {timePeriodTwoTimeObjects}");

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

        private static void PrzykladowaAplikacjaRozszerzonaOMilisekundy()
        {
            Console.WriteLine("\n======== Time ========");
            var timeOneParam = new ms.Time(12);
            var timeTwoParams = new ms.Time(23, 59);
            var timeThreeParams = new ms.Time(6, 12, 59);
            var timeFourParams = new ms.Time(20, 40, 30, 900);
            var timeString = new ms.Time("15:05:29.300");

            Console.WriteLine($"Konstruktor jeden argument (12): {timeOneParam}");
            Console.WriteLine($"Konstruktor dwa argumenty (23,59): {timeTwoParams}");
            Console.WriteLine($"Konstruktor trzy argumenty (6,12,59): {timeThreeParams}");
            Console.WriteLine($"Konstruktor cztery argumenty (20, 40, 30, 900): {timeFourParams}");
            Console.WriteLine($"Konstruktor string (15:05:29.300): {timeString}");

            var timeLater = new ms.Time(20, 0, 0, 900);
            var timeEarlier = new ms.Time(10, 30, 0, 500);

            Console.WriteLine($"\n{timeLater} > {timeEarlier} : {timeLater > timeEarlier}");
            Console.WriteLine($"{timeLater} >= {timeEarlier} : {timeLater >= timeEarlier}");
            Console.WriteLine($"{timeLater} < {timeEarlier} : {timeLater < timeEarlier}");
            Console.WriteLine($"{timeLater} <= {timeEarlier} : {timeLater <= timeEarlier}");
            Console.WriteLine($"{timeLater} == {timeEarlier} : {timeLater == timeEarlier}");
            Console.WriteLine($"{timeLater} != {timeEarlier} : {timeLater != timeEarlier}");

            var fiveHoursTimePeriod = new ms.TimePeriod(5, 0);
            Console.WriteLine($"\n{timeLater} + {fiveHoursTimePeriod} = {timeLater + fiveHoursTimePeriod}");
            Console.WriteLine($"{timeLater} - {fiveHoursTimePeriod} = {timeLater - fiveHoursTimePeriod}");


            Console.WriteLine("\n======== TimePeriod ========");

            var timePeriodOneParam = new ms.TimePeriod(600.300);
            var timePeriodTwoParams = new ms.TimePeriod(24, 30);
            var timePeriodThreeParams = new ms.TimePeriod(30, 20, 10.999);
            var timePeriodTwoTimeObjects = new ms.TimePeriod(timeEarlier, timeLater);
            var timePeriodString = new ms.TimePeriod("24:24:24.024");

            Console.WriteLine($"Konstruktor jeden argument (600.300): {timePeriodOneParam}");
            Console.WriteLine($"Konstruktor dwa argumenty (24, 30): {timePeriodTwoParams}");
            Console.WriteLine($"Konstruktor trzy argumenty (30, 20, 10.999): {timePeriodThreeParams}");
            Console.WriteLine($"Konstruktor string (24:24:24.024): {timePeriodString}");
            Console.WriteLine($"Konstruktor dwa obiekty Time(10,30,0,900) (20,0,0,500) : {timePeriodTwoTimeObjects}");

            var timePeriodLonger = new ms.TimePeriod(86400.200);
            var timePeriodShorter = new ms.TimePeriod(35000.999);

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

        private static void SetCulture()
        {
            // '.' instead of ','
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }
    }
}
