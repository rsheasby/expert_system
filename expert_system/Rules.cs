using System;
using System.Collections.Generic;

namespace expert_system
{
    class Rules
    {
		public struct	Rule
		{
			public string	condition;
			public string	result;
		}

		private List<Rule>	rules = new List<Rule>();

		public void		add(string condition, string result)
		{
			Rule temp = new Rule();
			temp.condition = condition;
			temp.result = result;
			rules.Add(temp);
		}

		public Rule	get(int index)
		{
			return (rules[index]);
		}
    }
}