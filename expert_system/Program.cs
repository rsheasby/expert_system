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
                io_class.file_path = args[0];
                io_class.main_rules();
                io_class.initial_facts();
            }
        }
    }
}
