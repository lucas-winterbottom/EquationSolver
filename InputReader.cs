using System;
using System.Collections.Generic;
using System.Linq;

namespace Equ
{
    //Class to read ther user input and convert it to a form that can be easily parsed
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

        //Check args to make sure they contain = and X
        private void ValidateArgs(string[] args)
        {
            bool noX = true;
            if (!args.Contains(Constants.eq.ToString())) ErrorHandler.ExitWithMessage(Error.NoEquals, " Equation is missing = value (args)");
            foreach (string s in args)
            {
                if (s.Contains(Constants.X)) noX = false;
            }
            if (noX) ErrorHandler.ExitWithMessage(Error.NoPronumeral, " Equation is missing X or X^2 value (args)");
        }

        //Convert the input into seperate strings utilising the format of operator space opeartor
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
                    ErrorHandler.ExitWithMessage(Error.InvalidCharacters, " At index:" + consoleInput.IndexOf(c));
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
            else ErrorHandler.ExitWithMessage(Error.MissingCalc, " The command is missing the calc prefix");
            if (!hasEquals) ErrorHandler.ExitWithMessage(Error.NoEquals, " Equation is missing = value");
            if (!hasX) ErrorHandler.ExitWithMessage(Error.NoPronumeral, " Equation is missing X or X^2 value");
        }

        //Checks if calc is present if so removes the calc prefix from the input
        public void RemoveCalc()
        {
            if (input[0].Equals(Constants.calc) && input.Count > 0)
                input.RemoveAt(0);
            else
                ErrorHandler.ExitWithMessage(Error.MissingCalc, " The command is missing the calc prefix");
        }
    }

}

