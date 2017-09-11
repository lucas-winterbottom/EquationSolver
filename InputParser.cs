using System;
using System.Collections.Generic;
using System.Linq;

namespace Equ
{
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

        public InputParser(List<string> input)
        {
            rhs = new List<Term>();
            lhs = new List<Term>();
            this.input = input;
            isLhs = true;
            ParseInput();
        }

        //TODO:
        //Ending +-/* */
        private void ParseInput()
        {
            Term temp = new Term();
            isNegative = false;
            foreach (string s in input)
            {
                if (s.Contains("(") || s.Contains(")"))
                {
                    AddTerm((Term)BracketHandler.Process(s, temp.Modifier));
                    temp = new Term();
                    temp.Modifier = Modifier.MUL;
                }
                else if (s.Contains("X^2"))
                {
                    temp.Type = TermType.sqVariable;
                    AddTerm(ProcessPronumeral(s, temp));
                    temp = new Term();
                }
                else if (s.Contains("X"))
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
                else if (s.Equals("+"))
                {
                    temp.Modifier = Modifier.NONE;
                }
                else if (s.Equals("/"))
                {
                    temp.Modifier = Modifier.DIV;
                }
                else if (s.Equals("-"))
                {
                    temp.Modifier = Modifier.NONE;
                    if (isNegative) isNegative = false;
                    else isNegative = true;
                }
                else if (s.Equals("*"))
                {
                    temp.Modifier = Modifier.MUL;
                }
                else if (s.Equals("%"))
                {
                    temp.Modifier = Modifier.MOD;
                }
                else if (s.Equals(Constants.eq))
                {
                    if(operators.Contains(input[input.IndexOf(s) - 1])) ErrorHandler.ExitWithMessage(Error.TrailingOperator);
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
                    ErrorHandler.ExitWithMessage(Error.InvalidCharacters);
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
                    else ErrorHandler.ExitWithMessage(Error.ErrorParsingDouble, tempno);
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
                else
                {
                    if (Double.TryParse(tempno, out double value))
                    {
                        if (isNegative) temp.Coeff = -value;
                        else temp.Coeff = value;
                    }
                    else ErrorHandler.ExitWithMessage(Error.ErrorParsingDouble, tempno);
                }
            }
            return temp;
        }

        private void PrintParsedInput()
        {
            Console.WriteLine("PrintParsedInput");
            foreach (Term i in lhs)
            {
                Console.Write(i.ToString());
            }
            Console.Write(Constants.eq);
            foreach (Term i in rhs)
            {
                Console.Write(i.ToString());
            }
            Console.WriteLine("");
            Console.WriteLine("------------");
        }

    }

}

