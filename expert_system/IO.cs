using System;
using System.IO;

namespace expert_system
{
    class IO
    {
        public void conditions(string file_path)
        {
            string  line, condition, result;
            StreamReader file = new StreamReader(file_path);
            Rules rule_obj = new Rules();

            while ((line = file.ReadLine()) != null)
            {
                condition = line.Substring(0, line.IndexOf("=>"));
                condition = condition.Replace(" ", String.Empty);
                result = line.Substring(line.IndexOf(">") + 1);
                result = result.Replace(" ", String.Empty);
                rule_obj.add(condition, result);
                Console.WriteLine("Condition:  " + condition);
                Console.WriteLine("Result:  " + result);
            }
        }
    }
}