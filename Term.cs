using System;
using System.Collections.Generic;

namespace Equ
{
    //Term OO object to better handle each term if the equation and its interactions with other terms
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
            if (type == TermType.sqVariable) s += Constants.XSq;
            return s;
        }

        //used to invert the value when changing sides of the equation
        internal void InvertValue()
        {
            coeff = -coeff;
        }

        /**
        *
        *
        *These next few method are just to make it easier to determine the type of a term
        *
        *
        */
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
            else if (t1.IsNumberz() && t2.IsVariable() || (t1.IsVariable() && t2.IsNumberz())) tempTerm.Type = TermType.variable;
            else if (t1.IsNumberz() && t2.IsSqVariable() || t1.IsSqVariable() && t2.IsNumberz()) tempTerm.Type = TermType.sqVariable;
            else tempTerm.Type = TermType.number;
            return tempTerm;
        }


        ///<summary>
        ///Handles the division of term such that it retains the x or makes it become x^2 depending on the circumstances, throwing a divide by zero error when neccessary
        ///</summary>
        public static Term operator /(Term t1, Term t2)
        {
            Term tempTerm = new Term();
            if (t2.Coeff == 0) ErrorHandler.ExitWithMessage(Error.DivByZero);
            tempTerm.Coeff = t1.Coeff / t2.Coeff;
            if (t1.IsVariable() && t2.IsVariable()) tempTerm.Type = TermType.number;
            else if (t1.IsVariable() && t2.IsNumberz()) tempTerm.Type = TermType.variable;
            else if ((t1.IsSqVariable() && t2.IsNumberz())) tempTerm.Type = TermType.sqVariable;
            else if (t1.IsSqVariable() && t2.IsVariable()) tempTerm.type = TermType.variable;
            else if (t1.BracketsContent != null && t2.BracketsContent != null)
            {
                tempTerm = new TermWithBrackets();
                for (int i = 0; i < t1.BracketsContent.Count; i++)
                {
                    for (int j = 0; j < t2.BracketsContent.Count; j++)
                    {
                        tempTerm.BracketsContent.Add(t1.BracketsContent[i] / t2.BracketsContent[j]);
                    }
                }
            }
            else if (t1.BracketsContent != null)
            {
                tempTerm = new TermWithBrackets();
                for (int i = 0; i < t1.BracketsContent.Count; i++)
                {
                    tempTerm.BracketsContent.Add(t1.BracketsContent[i] / t2);
                }
            }
            else if (t2.BracketsContent != null)
            {
                tempTerm = new TermWithBrackets();
                for (int i = 0; i < t2.BracketsContent.Count; i++)
                {
                    tempTerm.BracketsContent.Add(t2.BracketsContent[i] / t1);
                }
            }
            else if (t1.IsNumberz() && t2.IsVariable() || t1.IsNumberz() && t2.IsSqVariable()) ErrorHandler.ExitWithMessage(Error.DivByPronumeral, " Cannot Divide numeral by X or X^2 :" + t1.ToString() + t2.ToString());
            else tempTerm.Type = TermType.number;
            return tempTerm;
        }

        ///<summary>
        ///Handles the modulus of term such that throws errors under circumstances where modulus cannot be performed
        ///</summary>
        public static Term operator %(Term t1, Term t2)
        {
            Term tempTerm = new Term();
            if (t2.Coeff == 0) ErrorHandler.ExitWithMessage(Error.DivByZero);
            tempTerm.Coeff = t1.Coeff * t2.Coeff;
            if (t1.IsVariable() || t2.IsVariable() || t1.IsSqVariable() || t2.IsSqVariable()) ErrorHandler.ExitWithMessage(Error.NotANumber);
            else tempTerm.Type = TermType.number;
            return tempTerm;
        }
    }

    //Enum to detmine the modifier/operator of the term
    public enum Modifier
    {
        NONE, MOD, MUL, DIV
    }
    //Enum to determine the Terms Type
    public enum TermType
    {
        number, variable, sqVariable
    }
}
