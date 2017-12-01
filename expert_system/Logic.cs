using System;
using System.Text.RegularExpressions;

namespace expert_system
{
    static class Logic
    {
		private static sbyte	evaluateRule(ref Facts facts, ref Rules rules, string condition)
		{
			Match	match;
			string	spattern = @"\(([01A-Z!\+\^\|]+)\)";

			Regex regex = new Regex(spattern);
			match = regex.Match(condition);

			//	Recurse on the parts within parentheses.
			while (match.Success)
			{
				string temp = match.Value.Remove(0, 1).Remove(match.Value.Length - 2, 1);
				condition = condition.Replace(match.Value, (evaluateRule(ref facts, ref rules, temp)).ToString());
				match = regex.Match(condition);
			}

			//	Replace all the letters with their values.
			for (int i = 0; i < condition.Length; ++i)
			{
				if (char.IsUpper(condition[i]))
					condition = condition.Replace(condition[i].ToString(), (evaluateFact(ref facts, ref rules, condition[i]).ToString()));
			}

			//	Invert all the values preceded by a '!'.
			for (int i = 0; i < condition.Length; ++i)
			{
				if (condition[i] == '!')
					if (condition[i + 1] == '1')
					{
						condition = condition.Remove(i, 2);
						condition = condition.Insert(i, "0");
					}
					else if (condition[i + 1] == '0')
					{
						condition = condition.Remove(i, 2);
						condition = condition.Insert(i, "1");
					}
			}

			//	Combine values separated by '+'.
			for (int i = 0; i < condition.Length; ++i)
			{
				if (condition[i] == '+')
					if (condition[i - 1] == '1' && condition[i + 1] == '1')
					{
						--i;
						condition = condition.Remove(i, 3);
						condition = condition.Insert(i, "1");
					}
					else
					{
						--i;
						condition = condition.Remove(i, 3);
						condition = condition.Insert(i, "0");
					}
			}

			//	Combine values separated by '|'.
			for (int i = 0; i < condition.Length; ++i)
			{
				if (condition[i] == '|')
					if (condition[i - 1] == '1' || condition[i + 1] == '1')
					{
						--i;
						condition = condition.Remove(i, 3);
						condition = condition.Insert(i, "1");
					}
					else
					{
						--i;
						condition = condition.Remove(i, 3);
						condition = condition.Insert(i, "0");
					}
			}

			//	Combine values separated by '^'.
			for (int i = 0; i < condition.Length; ++i)
			{
				if (condition[i] == '^')
					if ((condition[i - 1] == '1' && condition[i + 1] == '0')
					||	(condition[i - 1] == '0' && condition[i + 1] == '1'))
					{
						--i;
						condition = condition.Remove(i, 3);
						condition = condition.Insert(i, "1");
					}
					else
					{
						--i;
						condition = condition.Remove(i, 3);
						condition = condition.Insert(i, "0");
					}
			}
			if (condition.Length == 1)
				return ((sbyte)int.Parse(condition));
			else
				return (-1);
		}

		private static sbyte	processResult(string result, char letter)
		{
			if (result.Contains("!" + letter))
				return (0);
			else if (result.Contains(letter.ToString()))
				return (1);
			else
				return (0);
		}

		private static sbyte		evaluateFact(ref Facts facts, ref Rules rules, char letter)
		{
			sbyte		temp;
			sbyte		result = -1;
			Rules.Rule	rule;

			temp = facts.getValue(letter);
			if (temp == -1)
				for (int i = 0; true; ++i)
				{
					rule = rules.get(i);
					if (rule == null)
					{
						if (result == -1)
							result = 0;
						facts.setValue(letter, result);
						return (result);
					}
					else if (rule.result.Contains(letter.ToString()))
					{
						temp = evaluateRule(ref facts, ref rules, rule.condition);
						if (temp == 1)
							temp = processResult(rule.result, letter);
						if (result != -1 && temp != -1 && temp != result)
						{
							Console.Error.WriteLine("Rule/Data contradiction!");
							return (-1);
						}
						else if (temp != -1)
							result = temp;
					}
				}
			else
				return (temp);
		}

		public static void evaluateQueries(ref Rules rules, ref Facts facts, string queries)
		{
			if (queries == null || queries.Length == 0)
				return ;
			foreach (char letter in queries)
			{
				if (char.IsUpper(letter))
				{
					sbyte	temp;

					temp = evaluateFact(ref facts, ref rules, letter);
					switch (temp)
					{
						case -1:
							Console.WriteLine("{0}: Unknown.", letter);
							break;
						case 0:
							Console.WriteLine("{0}: False.", letter);
							break;
						case 1:
							Console.WriteLine("{0}: True.", letter);
							break;
					}
				}
			}
		}
    }
}