using System;
using System.Collections.Generic;

namespace Equ
{
    public class Term
    {
        protected TermType type;
        protected double coeff;
        protected Modifier modifier;
        public Modifier Modifier { get => modifier; set => modifier = value; }
        public double Coeff { get => coeff; set => this.coeff = value; }
        public TermType Type { get => type; set => type = value; }
        public virtual List<Term> BracketsContent { get; internal set; }

        public Term()
        {
            coeff = 1;
            type = TermType.number;
        }

        public Term(double v)
        {
            this.coeff = v;
            type = TermType.number;
        }

        internal virtual void WorkBrackets()
        {//to be accessed by term with brackets
        }
        internal virtual void WorkBrackets(Term t)
        {//to be accessed by term with brackets
        }


        public override string ToString()
        {
            string s = "";
            if (modifier == Modifier.DIV) s += Constants.div;
            else if (modifier == Modifier.MUL) s += Constants.mul;
            else if (modifier == Modifier.MOD) s += Constants.mod;
            else if (modifier == Modifier.NONE) s += "";
            if (coeff > 0) s += "+";
            s += coeff;
            if (type == TermType.variable) s += "X";
            if (type == TermType.sqVariable) s += Constants.xSq;
            return s;
        }

        internal void InvertValue()
        {
            coeff = -coeff;
        }

        internal bool IsVariable()
        {
            return type == TermType.variable;
        }
        internal bool IsSqVariable()
        {
            return type == TermType.sqVariable;
        }

        internal bool IsNumberz()
        {
            return type == TermType.number;
        }


        ///<summary>
        ///Handles the multiplication of term such that it retains the x or makes it become x^2 depending on the circumstances
        ///</summary>
        public static Term operator *(Term t1, Term t2)
        {
            Term tempTerm = new Term();
            tempTerm.Coeff = t1.Coeff * t2.Coeff;
            if (t1.IsVariable() && t2.IsVariable()) tempTerm.Type = TermType.sqVariable;
            else if (t1.IsNumberz() && t2.IsVariable() || (t1.IsVariable() && t2.IsNumberz())) tempTerm.Type = TermType.variable;
            else if (t1.IsNumberz() && t2.IsSqVariable() || t1.IsSqVariable() && t2.IsNumberz()) tempTerm.Type = TermType.sqVariable;
            else if (t1.BracketsContent != null && t2.BracketsContent != null)
            {
                tempTerm = new TermWithBrackets();
                for (int i = 0; i < t1.BracketsContent.Count; i++)
                {
                    for (int j = 0; j < t2.BracketsContent.Count; j++)
                    {
                        tempTerm.BracketsContent.Add(t1.BracketsContent[i] * t2.BracketsContent[j]);
                    }
                }
            }
            //TODO: Condsider making a seperate method for these two
            else if (t1.BracketsContent != null)
            {
                tempTerm = new TermWithBrackets();
                for (int i = 0; i < t1.BracketsContent.Count; i++)
                {
                    tempTerm.BracketsContent.Add(t1.BracketsContent[i] * t2);
                }
            }
            else if (t2.BracketsContent != null)
            {
                tempTerm = new TermWithBrackets();
                for (int i = 0; i < t2.BracketsContent.Count; i++)
                {
                    tempTerm.BracketsContent.Add(t2.BracketsContent[i] * t1);
                }
            }
            else tempTerm.Type = TermType.number;
            return tempTerm;
        }
        //TODO: Make sure it handles the brackets
        public static Term operator /(Term t1, Term t2)
        {
            Term tempTerm = new Term();
            if (t2.Coeff == 0) ErrorHandler.ExitWithMessage(Error.DivByZero);
            tempTerm.Coeff = t1.Coeff / t2.Coeff;
            if (t1.IsVariable() && t2.IsVariable()) tempTerm.Type = TermType.number;
            else if (t1.IsNumberz() && t2.IsVariable() || t1.IsVariable() && t2.IsNumberz()) tempTerm.Type = TermType.variable;
            else if (t1.IsNumberz() && t2.IsSqVariable() || (t1.IsSqVariable() && t2.IsNumberz())) tempTerm.Type = TermType.sqVariable;
            else tempTerm.Type = TermType.number;
            return tempTerm;
        }

        public static Term operator %(Term t1, Term t2)
        {
            Term tempTerm = new Term();
            if (t2.Coeff == 0) ErrorHandler.ExitWithMessage(Error.DivByZero);
            tempTerm.Coeff = t1.Coeff * t2.Coeff;
            if (t1.IsVariable() || t2.IsVariable() || t1.IsSqVariable() || t2.IsSqVariable()) ErrorHandler.ExitWithMessage(Error.NaN);
            else tempTerm.Type = TermType.number;
            return tempTerm;
        }
    }


    public enum Modifier
    {
        NONE, MOD, MUL, DIV
    }
    public enum TermType
    {
        number, variable, sqVariable, brackets
    }
}
