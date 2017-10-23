using System;

namespace expert_system
{
    class Facts
    {
		private sbyte[]	facts;
        public			Facts()
		{
			facts = new sbyte[26];
			for (int i = 0; i < 26; ++i)
				facts[i] = -1;
		}

		public void		setValue(char letter, sbyte value)
		{
			if (char.IsUpper(letter) && value >= -1 && value <= 1)
				facts[letter - 'A'] = value;
		}

		public sbyte	getValue(char letter)
		{
			if (char.IsUpper(letter))
				return (facts[letter - 'A']);
			else
				return (-1);
		}
    }
}
