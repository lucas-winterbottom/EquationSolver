using System;
using System.Collections.Generic;

namespace Equ
{
    public class TermWithBrackets : Term
    {

        public override List<Term> BracketsContent { get; internal set; }

        public TermWithBrackets()
        {
            coeff = 1;
            BracketsContent = new List<Term>();
        }

        //Extention on the tostring method in Term, just adds the interior brackets content
        public override string ToString()
        {
            string s = "";
            if (modifier == Modifier.DIV) s += Constants.div;
            else if (modifier == Modifier.MUL) s += Constants.mul;
            else if (modifier == Modifier.MOD) s += Constants.mod;
            else if (modifier == Modifier.NONE) s += "";
            if (coeff > 0) s += "+";
            if (coeff != 1) s += coeff;
            s += Constants.lb;
            foreach (Term t in BracketsContent)
            {
                s += t.ToString();
            }
            s += Constants.rb;
            if (type == TermType.variable) s += "X";
            if (type == TermType.sqVariable) s += Constants.xSq;
            return s;
        }
        internal bool IsBrackets()
        {
            return BracketsContent.Count > 0;
        }

        //Multiples the exterior term to the brakcets through the interior ones
        internal override void WorkBrackets()
        {
            Solver solver = new Solver(BracketsContent);
            solver.SolveInterior();
            BracketsContent = solver.Lhs;
            if (coeff != 1 || type != TermType.number)
            {
                List<Term> temp = this.BracketsContent;
                BracketsContent = null;
                for (int i = 0; i < temp.Count; i++)
                {
                    temp[i] = this * temp[i];
                }
                this.BracketsContent = temp;
                this.Coeff = 1;
            }
        }
    }
}
