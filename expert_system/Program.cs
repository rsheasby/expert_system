using System;

namespace expert_system
{
    class Program
    {
        static void Main(string[] args)
        {
            IO io_class = new IO();

            foreach (string arg in args)
            {
                Console.WriteLine("Reading file \"{0}\"", arg);
                io_class.file_path = arg;
                Rules rule_obj = (Rules)io_class.main_rules();
                Facts fact_obj = (Facts)io_class.initial_facts();
                if (rule_obj == null || fact_obj == null)
                    continue;
                Logic.evaluateQueries(ref rule_obj, ref fact_obj, io_class.query());
                Console.WriteLine("");
            }
        }
    }
}
