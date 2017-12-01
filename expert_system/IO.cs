using System;
using System.IO;

namespace expert_system
{
    class IO
    {
        private string line;
        public string file_path{ get; set; } 

        public Object main_rules()
        {
            string  condition, result;
            Rules rule_obj = new Rules();
            StreamReader file = new StreamReader(file_path);
            string temp;
            
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("#"))
                    temp = line.Substring(0, line.IndexOf("#"));
                else
                    temp = line;
                if (!temp.Contains("=>"))
                    continue;
                condition = temp.Substring(0, temp.IndexOf("=>"));
                condition = condition.Replace(" ", String.Empty);
                result = temp.Substring(temp.IndexOf(">") + 1);
                result = result.Replace(" ", String.Empty);
                foreach(char letter in condition)
                    if (char.IsUpper(letter) && result.Contains(letter.ToString()))
                    {
                        Console.Error.WriteLine("Rule has potential recursion loop:\n{0}", line);
                        return (null);
                    }
                if (result.Contains("|") || result.Contains("^") || temp.Contains("<=>"))
                {
                    Console.Error.WriteLine("Rule \"{0}\" is invalid.\n", line);
                    file.Close();
                    return (null);
                }
                rule_obj.add(condition, result);
            }
            file.Close();
            return (rule_obj);
        }
        public Object initial_facts()
        {
            Facts fact_obj = new Facts();
            StreamReader file = new StreamReader(file_path);
            bool status = false;

            while ((line = file.ReadLine()) != null)
            {
                string temp;
                if (line.StartsWith("="))
                {
                    if (line.Contains("#"))
                        temp = line.Substring(0, line.IndexOf("#"));
                    else
                        temp = line;
                    temp = temp.Substring(1);
                    foreach (char c in temp)
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
            file.Close();
            return (fact_obj);
        }
        public string query()
        {
            StreamReader file = new StreamReader(file_path);
            string temp;

            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("?"))
                {
                    if (line.Contains("#"))
                        temp = line.Substring(0, line.IndexOf("#"));
                    else
                        temp = line;
                    return (temp.Substring(1));
                }
            }
            return (null);
        }
    }
}