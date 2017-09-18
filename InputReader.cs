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
            string consoleInput = Console.ReadLine();
            ValidateInput(consoleInput);
            input = consoleInput.Split(' ').ToList();
            input = ConcatBrackets(input);
            RemoveCalc();
        }

        public InputReader(string[] args)
        {
            ValidateInput(String.Join("", args));
            input = ConcatBrackets(args.ToList());
            RemoveCalc();
        }

        //Checks if the input contains X, = and has no invalid characters
        private void ValidateInput(string s)
        {
            if (!s.Contains(Constants.eq)) ErrorHandler.ExitWithMessage(Error.NoEquals, " Equation is missing = value ");
            if (!s.Contains(Constants.X) && !s.Contains(Constants.x)) ErrorHandler.ExitWithMessage(Error.NoPronumeral, " Equation is missing X or X^2 value");
            foreach (char c in s)
            {
                if (!Constants.symbolcheck.Contains(c)) ErrorHandler.ExitWithMessage(Error.InvalidCharacters, " Invalid Character:" + c);
            }
        }

        //Concatinates brackets to ensure they are condensed to one string
        private List<string> ConcatBrackets(List<string> s)
        {
            List<string> tempList = new List<string>();
            bool inBrackets = false;
            string tempString = "";
            for (int i = 0; i < s.Count; i++)
            {
                if (s[i].Contains(Constants.rb) && s[i].Contains(Constants.lb))
                {
                    if (tempString.Length != 0) tempList.Add(tempString);
                    tempString = "";
                    if (s[i].Split('(')[0].Length > 0)
                    {
                        tempList.Add(s[i].Split('(')[0]);
                        tempList.Add("*");
                        tempList.Add('(' + s[i].Split('(')[1]);
                    }
                    else tempList.Add(s[i]);
                    tempString = "*";
                }
                else if (s[i].Contains(Constants.lb))
                {
                    if (tempString.Length != 0) tempList.Add(tempString);
                    tempString = "";
                    if (s[i].Split('(')[0].Length > 1)
                    {
                        tempList.Add(s[i].Split('(')[0]);
                        tempList.Add("*");
                        tempString += '(' + s[i].Split('(')[1];
                    }
                    else
                    {
                        tempString += s[i];
                    }
                    inBrackets = true;
                }
                else if (s[i].Contains(Constants.rb))
                {
                    tempString += s[i];
                    tempList.Add(tempString);
                    inBrackets = false;
                    tempString = "*";
                }
                else if (inBrackets)
                {
                    tempString += s[i];
                }
                else
                {
                    tempList.Add(s[i]);
                    tempString = "";
                }
            }
            return tempList;
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

