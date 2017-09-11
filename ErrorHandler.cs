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
            Console.WriteLine(e.ToString() + s);
            Environment.Exit(0);
        }
    }

    internal enum Error
    {
        NaN, NoEquals, NoPronumeral, InvalidCharacters, MissingCalc, DivByZero,
        ErrorParsingDouble,
        TrailingOperator
    }
}
