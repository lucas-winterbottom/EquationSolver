using System;
using System.Collections.Generic;

namespace Equ
{
    internal class BracketHandler
    {
        public static string operators = "-+*/%";
        //make it handle variables
        internal static TermWithBrackets Process(string s, Modifier m)
        {
            TermWithBrackets temp = new TermWithBrackets();
            temp.Modifier = m;
            List<string> inside = new List<string>();
            string holder = "";
            foreach (char c in s)
            {
                if (Char.IsDigit(c))
                {
                    holder += c;
                }
                else if (c == '(')
                {
                    if (holder.Length > 0) temp.Coeff = Double.Parse(holder);
                    holder = "";
                }
                else if (operators.Contains(c.ToString()))
                {
                    if (!holder.Equals("")) inside.Add(holder);
                    inside.Add(c.ToString());
                    holder = "";
                }
                else if (c == ')')
                {
                    inside.Add(holder);
                }
                else
                {
                    holder += c;
                }
            }
            InputParser parser2 = new InputParser(inside);
            temp.BracketsContent = parser2.Lhs;
            return temp;
        }
    }
}

