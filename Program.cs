using System;

namespace Equ
{
    class Program
    {
        private const string prompt = "Enter another Equation: ";
        private static InputReader reader;

        static void Main(string[] args)
        {
            reader = new InputReader(args);
            while (true)
            {

                InputParser parser = new InputParser(reader.Input);
                Solver solver = new Solver(parser.Lhs, parser.Rhs);
                solver.Solve();
                reader = new InputReader();
                Console.WriteLine(prompt);

            }

        }
    }
}
