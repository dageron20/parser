using System;

namespace iniParser
{
    class Program
    {
        static void Main(string[] args)
        {
            iniparser inipars = new iniparser(@"E:\test.ini");

            /*var f1 = inipars.readData("COMMON", "StatisterTimeMs");
            Console.WriteLine(f1);*/

            var f2 = inipars.TakeDouble("COMMON", "StatisterTimeMs");
            Console.WriteLine(f2);


            /*var f22 = inipars.TakeDouble("ADC_DEV", "BufferLenSecons");
            Console.WriteLine(f2);*/

            /*var f3 = inipars.TakeInt("ADC_DEV", "BufferLenSecons");
            Console.WriteLine(f3);*/

            /*var f4 = inipars.TakeDouble("QWE", "BufferLenSecons");
            Console.WriteLine(f4);*/

            /*var f5 = inipars.TakeDouble("qwer", "rewq");
            Console.WriteLine(f5);*/



        }
    }
}
