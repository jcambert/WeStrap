using System;
using System.Collections.Generic;
using System.Text;

namespace ColorApplication
{
    class OtherProgram
    {
        static void Main(string[] args)
        {
            var date =new Date(new DateTime(2020,8,5),null);
            DateTime now = new Date();
            Date renow = DateTime.Now;
            Console.WriteLine(renow.DayInYear);
            Console.WriteLine(date.DayInMonth);
            Console.WriteLine(date.FirstDayOfMonth.ToString());
            Console.WriteLine(date.LastDayOfMonth.ToString());
            Console.ReadLine();

        }
    }
}
