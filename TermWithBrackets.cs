using System;
using System.Collections.Generic;

namespace Equ
{
    public class TermWithBrackets : Term
    {

        public List<Term> BracketsContent { get; internal set; }

        public TermWithBrackets()
        {
            BracketsContent = new List<Term>();
        }

        public override string ToString()
        {
            string s = "";
            if (modifier == Modifier.DIV) s += "/";
            else if (modifier == Modifier.MUL) s += "*";
            else if (modifier == Modifier.MOD) s += "%";
            else if (modifier == Modifier.NONE) s += "";
            if (coeff > 0) s += "+";
            s += coeff;
            s += "(";
            foreach (Term t in BracketsContent)
            {
                s += t.ToString();
            }
            s += ")";
            if (type == TermType.variable) s += "X";
            if (type == TermType.sqVariable) s += "X^2";
            return s;
        }
        internal bool IsBrackets()
        {
            return BracketsContent.Count > 0;
        }

        internal void WorkBrackets()
        {
            if (coeff > 1)
            {
                foreach (Term t in BracketsContent)
                {

                }

            }
        }
    }
}
