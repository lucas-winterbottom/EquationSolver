using System;
using System.Collections;

namespace Equ
{
    class Program
    {

        static void Main(string[] args)
        {
            string consoleInput = String.Join(" ", args);
            do
            {
                try
                {
                    InputReader reader = new InputReader(consoleInput);
                    InputParser parser = new InputParser(reader.Input);
                    Solver solver = new Solver(parser.Lhs, parser.Rhs);
                    solver.Solve();
                    Console.WriteLine(Constants.prompt);
                    consoleInput = Console.ReadLine();
                }
                catch (InvalidEquationException e)
                {
                    Console.WriteLine("ErrorType: " + e.ErrorType.ToString());
                    Console.WriteLine("ErrorMessage: " + e.MessageString);
                    Console.WriteLine(Constants.prompt);
                    consoleInput = Console.ReadLine();
                }
            } while (!consoleInput.Equals("exit"));

        }

    }
}
