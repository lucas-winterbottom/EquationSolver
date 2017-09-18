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
            throw new InvalidEquationException(e, s);
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
        UnparseableCombination,
        TooLargeIndices
    }
}
