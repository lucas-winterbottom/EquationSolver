using System;

namespace Equ
{
    internal class ErrorHandler
    {
        internal static void ExitWithMessage(Error e)
        {
            Console.WriteLine(e.ToString());
            Environment.Exit(0);
        }

        internal static void ExitWithMessage(Error e, string s)
        {
            Console.WriteLine("ErrorType: " + e.ToString());
            Console.WriteLine("ErrorMessage: " + s);
            Environment.Exit(0);
        }
    }

    internal enum Error
    {
        NotANumber, NoEquals, NoPronumeral, InvalidCharacters, MissingCalc, DivByZero,
        ErrorParsingDouble,
        TrailingDivisionOperator,
        DivByPronumeral,
        NoLHSContent,
        NoRHSContent,
        UnparseableCombination
    }
}
