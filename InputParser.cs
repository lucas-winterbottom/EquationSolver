using System;
using System.Collections.Generic;
using System.Linq;

namespace Equ
{
    //Class to convert input to Term object which can then be solved
    public class InputParser
    {
        private List<Term> rhs;
        private List<Term> lhs;
        private List<string> input;
        private bool isNegative = false;
        private bool isLhs = false;
        private static string operators = "*/+-";

        public List<Term> Lhs
        {
            get
            {
                return lhs;
            }
        }
        public List<Term> Rhs
        {
            get
            {
                return rhs;
            }
        }

        //Contructor to prepare for input parsing
        public InputParser(List<string> input)
        {
            rhs = new List<Term>();
            lhs = new List<Term>();
            this.input = input;
            isLhs = true;
            ParseInput();
        }

        //Method to convert the input into term object and insert them into the lhs or rhs array
        private void ParseInput()
        {
            Term temp = new Term();
            isNegative = false;
            foreach (string s in input)
            {
                if (s.Contains(Constants.lb) && s.Contains(Constants.rb))
                {
                    AddTerm((Term)BracketHandler.Process(s, temp.Modifier));
                    temp = new Term();
                    //temp.Modifier = Modifier.MUL;
                }
                else if (s.Contains(Constants.xSq) || s.Contains(Constants.XSq))
                {
                    temp.Type = TermType.sqVariable;
                    AddTerm(ProcessPronumeral(s, temp));
                    temp = new Term();
                }
                else if (s.Contains(Constants.X) || s.Contains(Constants.x))
                {
                    temp.Type = TermType.variable;
                    AddTerm(ProcessPronumeral(s, temp));
                    temp = new Term();
                }
                else if (s.Contains("^2"))
                {
                    AddTerm(ProcessSquare(s, temp));
                    temp = new Term();
                }
                else if (s.Equals(Constants.plus.ToString()))
                {
                    temp.Modifier = Modifier.NONE;
                }
                else if (s.Equals(Constants.minus.ToString()))
                {
                    temp.Modifier = Modifier.NONE;
                    if (isNegative) isNegative = false;
                    else isNegative = true;
                }
                else if (s.Equals(Constants.div.ToString()))
                {
                    temp.Modifier = Modifier.DIV;
                }
                else if (s.Equals(Constants.mul.ToString()))
                {
                    temp.Modifier = Modifier.MUL;
                }
                else if (s.Equals(Constants.mod.ToString()))
                {
                    temp.Modifier = Modifier.MOD;
                }
                //TODO:
                //make sure i handle the * 0 thing
                else if (s.Equals(Constants.eq.ToString()))
                {
                    if (operators.Contains(input[input.IndexOf(s) - 1])) ErrorHandler.ExitWithMessage(Error.TrailingDivisionOperator, " : " + s);
                    if (lhs.Count == 0) ErrorHandler.ExitWithMessage(Error.NoLHSContent, " No Terms in the LHS of the Equation");
                    isLhs = false;
                    temp = new Term();
                }
                else if (Double.TryParse(s, out double numericalValue))
                {
                    if (isNegative) temp.Coeff = -numericalValue;
                    else temp.Coeff = numericalValue;
                    AddTerm(temp);
                    temp = new Term();
                }
                else
                {
                    ErrorHandler.ExitWithMessage(Error.UnparseableCombination, " At input:" + s);
                }
            }
        }

        //Adds the term to the left or right hand side depending on where the parser is up to
        private void AddTerm(Term test)
        {
            if (isLhs) lhs.Add(test);
            else rhs.Add(test);
            isNegative = false;
        }

        //Loop through while it is still a number, or a negative symbol
        //Once it hits a non-number parse the number and store the term
        private Term ProcessSquare(string s, Term temp)
        {
            string tempno = "";
            foreach (char c in s)
            {
                if (Char.IsDigit(c) || c == '-')
                {
                    tempno += c;
                }
                else
                {
                    if (Double.TryParse(tempno, out double value))
                    {
                        if (isNegative) temp.Coeff = -value * value;
                        else temp.Coeff = value * value;
                    }
                    else ErrorHandler.ExitWithMessage(Error.ErrorParsingDouble, " In string:" + s);
                }
            }
            return temp;
        }

        //Loop through while it is still a number, or a negative symbol
        //Once it hits a non-number parse the number and store the term
        private Term ProcessPronumeral(string s, Term temp)
        {
            string tempno = "";
            foreach (char c in s)
            {
                if (Char.IsDigit(c) || c == '-')
                {
                    tempno += c;
                }
                else if (tempno.Length == 0) return temp;
                else if (c == Constants.X || c == Constants.mul || c == Constants.x)
                {
                    if (Double.TryParse(tempno, out double value))
                    {
                        if (isNegative) temp.Coeff = -value;
                        else temp.Coeff = value;
                        return temp;
                    }
                    else ErrorHandler.ExitWithMessage(Error.ErrorParsingDouble, " In string:" + s);
                }
                else
                {
                    ErrorHandler.ExitWithMessage(Error.InvalidCharacters, " Invalid character in pronumeral:" + c);
                }
            }
            return temp;
        }
    }

}

