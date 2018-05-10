using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public static class DumpExtension
    {
        public static void Dump(this Object @this)
        {
            Console.WriteLine(@this);
        }
    }
}