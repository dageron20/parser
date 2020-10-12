using System;

namespace iniParser
{
    class Program
    {
        static void Main(string[] args)
        {
            iniparser inipars = new iniparser(@"E:\test.ini");

            var f1 = inipars.readData("COMMON", "StatisterTimeMs");
            Console.WriteLine(f1);

            var f2 = inipars.TryGetDouble("ADC_DEV", "BufferLenSecons");
            Console.WriteLine(f2);

            /*var f3 = inipars.TryGetInt("ADC_DEV", "BufferLenSecons");
            Console.WriteLine(f3);*/

            /*var f4 = inipars.TryGetDouble("QWE", "BufferLenSecons");
            Console.WriteLine(f4);*/

            /*var f5 = inipars.TryGetDouble("qwer", "rewq");
            Console.WriteLine(f5);*/



        }
    }
}
