using System;

namespace Equ
{
  //TODO:
  // make sure that it throws correct errors
  // check for terms on lhs and rhs
    class Program
    {
        private static InputReader reader;

        static void Main(string[] args)
        {
            reader = new InputReader(args);
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
