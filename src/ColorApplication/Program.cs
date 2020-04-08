using System;
using System.Collections.Generic;
using System.Globalization;
using WeC;
namespace ColorApplication
{
    class Program
    {
        Dictionary<string, (IntH, IntH, IntH)> colors = new Dictionary<string, (IntH, IntH, IntH)>(){
            {"aqua",(1,2,3)}
        };

        static IntH nbre = "FA";

        

        static void Main(string[] args)
        {

            var c = Convert.ToDouble("750.",CultureInfo.InvariantCulture);
            var colors = WeC.WeColor.Random(6);
           /* Console.WriteLine($"nbre:{255*IntH.MaxValue}");
            nbre += 2;
            Console.WriteLine($"nbre:{nbre.ToStringHex()}");
            Console.WriteLine($"Color:{WeColor.AliceBlue}");
            Console.WriteLine($"Color:{WeColor.AliceBlue.ToString()}");
            Console.WriteLine($"Color:{(WeHSL)WeColor.AliceBlue}");
            Console.WriteLine($"Color:{(WeHSL)WeColor.From(24,98,118,1)}");
            Console.WriteLine($"Color:{WeColor.From(24, 98, 118, 1).ToString()}");
             Console.WriteLine($"Color:{WeColor.From("hsla(193,67%,28%,0.2)")}");
            Console.WriteLine($"Color:{WeColor.From("hsla(193,67%,28%,0.5)")}");
            Console.WriteLine($"Color:{WeColor.From("#FA2")}");
            Console.WriteLine($"Color:{WeColor.From("#FA25")}");
            Console.WriteLine($"Color:{WeColor.From("#FA2564")}");
            Console.WriteLine($"Color:{WeColor.From("#FA2564AB")}");
            Console.WriteLine($"Color:{WeColor.From("rgba(255,50%,50,0.4)")}");
            Console.WriteLine($"Color:{WeColor.From("rgb(5,50%,50)")}");

             Console.WriteLine("***************************************");
             var mixeds = WeColor.From("#0000FF").MixWith(WeColor.From("#FF0000"),12);
             foreach (var mixed in mixeds)
             {
                 Console.WriteLine($"Color:{mixed}");
             }

            //Console.WriteLine($"HSL:{WeHSL.From("hsl(193,67%,28%)")}");
            */
        }
    }
}
