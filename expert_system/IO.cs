using System;
using System.IO;

namespace expert_system
{
    class IO
    {
        private string line; //will be used in all functions to read rules, facts & query
        public string file_path{ get; set; } 

        public void main_rules()
        {
            string  condition, result;
            Rules rule_obj = new Rules();
            StreamReader file = new StreamReader(file_path);
            line = file.ReadLine();

            while (line != String.Empty)
            {
                condition = line.Substring(0, line.IndexOf("=>"));
                condition = condition.Replace(" ", String.Empty); //Trims all whitespaces
                result = line.Substring(line.IndexOf(">") + 1);
                result = result.Replace(" ", String.Empty); //Trims all whitespaces
                rule_obj.add(condition, result);
                line = file.ReadLine();
            }
        }
        public void initial_facts()
        {
            Facts fact_obj = new Facts();
            StreamReader file = new StreamReader(file_path);
            bool status = false;

            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("="))
                {
                    line = line.Substring(1); //skips the '=' char
                    foreach (char c in line)
                    {
                        if (c != '!' && status == false)
                            fact_obj.setValue(c, 1);
                        else if (c == '!')
                            status = true;
                        else if (c != '!' && status == true)
                        {
                            fact_obj.setValue(c, 0);
                            status = false;
                        }
                    }
                    break ;
                }
            }
        }
    }
}