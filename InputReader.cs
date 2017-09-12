using System;
using System.Collections.Generic;
using System.Linq;

namespace Equ
{
    public class InputReader
    {

        private List<string> input;

        public List<string> Input { get => input; set => input = value; }

        public InputReader()
        {
            input = new List<string>();
            InputToStrings();
            RemoveCalc();
        }

        public InputReader(string[] args)
        {
            ValidateArgs(args);
            input = args.ToList();
            RemoveCalc();
        }

        private void ValidateArgs(string[] args)
        {
            bool noX = true;
            if (!args.Contains(Constants.eq)) ErrorHandler.ExitWithMessage(Error.NoEquals);
            foreach (string s in args)
            {
                if (s.Contains(Constants.X)) noX = false;
            }
            if (noX) ErrorHandler.ExitWithMessage(Error.NoPronumeral);
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
                if (!Constants.symbolcheck.Contains(c))
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
            if (input[0].Equals(Constants.calc) && input.Count > 0)
                input.RemoveAt(0);
            else
                ErrorHandler.ExitWithMessage(Error.MissingCalc);
        }
    }

}

