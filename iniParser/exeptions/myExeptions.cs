using System;
using System.Collections.Generic;
using System.Text;

namespace iniParser.exeptions
{
    class myExeptions : System.Exception
    {
        public myExeptions(string message) : base(message) {}

        public class InvalidFormat : myExeptions
        {
            public InvalidFormat (string message)
                : base("Error. Format is not .ini") { }
        }

        public class InvalidParametr : myExeptions
        {
            public InvalidParametr(string message)
                : base("Invalid parameter type") { }
        }
    }
}

