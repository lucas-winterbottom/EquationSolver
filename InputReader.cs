using System;
using System.Collections.Generic;
using System.Linq;

namespace Equ
{
    public class InputReader
    {

        private List<string> input;
        private static string symbolcheck = "*-+/%X^1234567890calc()= ";

        public List<string> Input { get => input; set => input = value; }

        public InputReader()
        {
            input = new List<string>();
            InputToStrings();
            RemoveCalc();
            PrintInput();
        }

        public InputReader(string[] args)
        {
            input = args.ToList();
            RemoveCalc();
            PrintInput();
        }

        public void PrintInput()
        {
            foreach (string s in input)
            {
                Console.WriteLine(s);
            }
        }

        private void InputToStrings()
        {
            List<char> consoleInput = Console.ReadLine().ToList();
            string current = "";
            bool hasEquals = false;
            bool hasX = false;
            bool inBrackets = false;
            foreach (char c in consoleInput)
            {
                if (!symbolcheck.Contains(c))
                {
                    ErrorHandler.ExitWithMessage(Error.InvalidCharacters);
                }
                switch (c)
                {
                    case ' ':
                        if (inBrackets) current += c;
                        else if (!current.Equals(""))
                        {
                            input.Add(current);
                            current = "";
                        }
                        break;
                    case '(':
                        inBrackets = true;
                        current += c;
                        break;
                    case ')':
                        input.Add(current + c);
                        inBrackets = false;
                        current = "";
                        break;
                    case '=':
                        hasEquals = true;
                        current += c;
                        break;
                    case 'X':
                        hasX = true;
                        current += c;
                        break;
                    default:
                        current += c;
                        break;
                }
            }
            if (!current.Equals("")) input.Add(current);
            else ErrorHandler.ExitWithMessage(Error.MissingCalc);
            if (!hasEquals) ErrorHandler.ExitWithMessage(Error.NoEquals);
            if (!hasX) ErrorHandler.ExitWithMessage(Error.NoPronumeral);
        }

        public void RemoveCalc()
        {
            if (input[0].Equals("calc") && input.Count > 0)
                input.RemoveAt(0);
            else
                ErrorHandler.ExitWithMessage(Error.MissingCalc);
        }
    }

}

