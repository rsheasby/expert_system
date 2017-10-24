using System;
using System.IO;

namespace expert_system
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
            {
                string  line, condition, result;
                StreamReader file = new StreamReader(args[0]);
                Rules rule_obj = new Rules();

                while ((line = file.ReadLine()) != null)
                {
                    condition = line.Substring(0, line.IndexOf("=>"));
                    result = line.Substring(line.IndexOf("=>"));
                    rule_obj.add(condition, result);
                }
            }
        }
    }
}
