using System;
using System.Collections;

namespace Equ
{
    class Program
    {

        static void Main(string[] args)
        {
            InputReader reader = new InputReader(args);
            while (true)
            {
                InputParser parser = new InputParser(reader.Input);
                Solver solver = new Solver(parser.Lhs, parser.Rhs);
                solver.Solve();
                Console.WriteLine(Constants.prompt);
                reader = new InputReader();
            }

        }
    }
}
