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
            PrintParsedInput();
        }

        private void PrintParsedInput()
        {
            Console.WriteLine("PrintParsedInput");
            Console.WriteLine("LHS");
            foreach (Term i in lhs)
            {
                Console.WriteLine(i.ToString());
            }
            Console.WriteLine("RHS");
            foreach (Term i in rhs)
            {
                Console.WriteLine(i.ToString());
            }
        }

        private void ParseInput()
        {
            double numericalValue;
            Term temp = new Term();
            isNegative = false;
            foreach (string s in input)
            {
                if (s.Contains("(") || s.Contains(")"))
                {
                    BracketHandler.Process(s, temp);
                    AddTerm(temp);
                    temp = new Term();
                    temp.Modifier = Modifier.MUL;
                }
                else if (s.Contains("X^2"))
                {
                    temp.Type = TermType.sqVariable;
                    ProcessPronumeral(s, temp);
                    temp = new Term();
                }
                else if (s.Contains("X"))
                {
                    temp.Type = TermType.variable;
                    ProcessPronumeral(s, temp);
                    temp = new Term();
                }
                else if (s.Contains("^2"))
                {
                    ProcessSquare(s, temp);
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
                else if (s.Equals("="))
                {
                    isLhs = false;
                    temp = new Term();
                }
                else if (Double.TryParse(s, out numericalValue))
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
        private void AddTerm(Term test)
        {
            if (isLhs)
            {
                lhs.Add(test);
            }
            else
            {
                rhs.Add(test);
            }
            isNegative = false;
        }

        private void ProcessSquare(string s, Term temp)
        {
            string tempno = "";
            foreach (char c in s)
            {
                if (c != '^')
                {
                    tempno += c;
                }
                else
                {
                    if (!tempno.Equals(""))
                    {
                        if (isNegative) temp.Coeff = -Double.Parse(tempno) * Double.Parse(tempno);
                        else temp.Coeff = Double.Parse(tempno) * Double.Parse(tempno);
                    }
                }
            }
            AddTerm(temp);
        }

        private void ProcessPronumeral(string s, Term temp)
        {
            string tempno = "";
            foreach (char c in s)
            {
                if (c != 'X')
                {
                    tempno += c;
                }
                else
                {
                    if (!tempno.Equals(""))
                    {
                        if (isNegative) temp.Coeff = -Double.Parse(tempno);
                        else temp.Coeff = Double.Parse(tempno);
                    }
                }
            }
            AddTerm(temp);

        }

    }

}

