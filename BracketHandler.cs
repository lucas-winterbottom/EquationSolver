using System;
using System.Collections.Generic;

namespace Equ
{
    internal class BracketHandler
    {
        public static string operators = "-+*/%";

        //Takes in a string and a modifier and returns a term with a list of terms that are inside the brackets and those that are outside
        //becoome the term itself
        internal static TermWithBrackets Process(string s, Modifier m)
        {
            TermWithBrackets temp = new TermWithBrackets();
            temp.Modifier = m;
            string interior = s.Split('(')[1];
            List<string> inside = ProcessInterior(interior);
            InputParser parser2 = new InputParser(inside);
            temp.BracketsContent = parser2.Lhs;
            return temp;
        }


        //Takes in the interior terms and returns a list of strings for parsing
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

