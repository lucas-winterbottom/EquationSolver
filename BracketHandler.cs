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
            string prefix = s.Split('(')[0];
            string interior = s.Split('(')[1];
            List<string> inside = ProcessInterior(interior);
            temp.Type = ProcessPrefix(prefix, temp);
            InputParser parser2 = new InputParser(inside);
            temp.BracketsContent = parser2.Lhs;
            return temp;
        }

        private static TermType ProcessPrefix(string prefix, TermWithBrackets temp)
        {
            if (prefix.Contains(Constants.X.ToString()))
            {
                prefix = prefix.Split('X')[0];
                return TermType.variable;
            }
            if (prefix.Contains(Constants.xSq.ToString()))
            {
                prefix = prefix.Split('X')[0];
                return TermType.sqVariable;
            }
            if (Double.TryParse(prefix, out double value)) temp.Coeff = value;
            else ErrorHandler.ExitWithMessage(Error.ErrorParsingDouble, " Cannot Parse:" + prefix);
            return TermType.number;

        }

        private static List<string> ProcessInterior(string s)
        {
            List<String> inside = new List<string>();
            string holder = "";
            foreach (char c in s)
            {
                if (Char.IsDigit(c))
                {
                    holder += c;
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
            return inside;
        }
    }
}

