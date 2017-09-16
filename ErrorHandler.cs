using System;

namespace Equ
{
  //TODO:
  //Handles errors in a way which produces a better explanatory result
    internal class ErrorHandler
    {
        internal static void ExitWithMessage(Error e)
        {
            Console.WriteLine(e.ToString());
            Environment.Exit(0);
        }

        internal static void ExitWithMessage(Error e, string s)
        {
            Console.WriteLine(e.ToString() + s);
            Environment.Exit(0);
        }
    }

    internal enum Error
    {
        NotANumber, NoEquals, NoPronumeral, InvalidCharacters, MissingCalc, DivByZero,
        ErrorParsingDouble,
        TrailingOperator,
        DivByPronumeral,
        NoLHSContent,
        NoRHSContent
    }
}
