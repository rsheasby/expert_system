using System;
using System.IO;

namespace expert_system
{
    class Program
    {
        static void Main(string[] args)
        {
            IO io_class = new IO();
            if (args.Length == 1)
            {
                io_class.conditions(args[0]);
            }
        }
    }
}
