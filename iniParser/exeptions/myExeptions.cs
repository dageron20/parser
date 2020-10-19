using System;
using System.Collections.Generic;
using System.Text;

namespace iniParser.exeptions
{
    class iniExeptions : System.Exception
    {
        public iniExeptions(string message) : base(message) { }

        public class InvalidFormat : iniExeptions
        {
            public InvalidFormat(string message)
                : base("Error. Format is not .ini") { }
        }

        public class InvalidParametr : iniExeptions
        {
            public InvalidParametr(string message)
                : base("Invalid parameter type") { }
        }
    }


}

