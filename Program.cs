﻿using System;
using System.Collections;

namespace Equ
{
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
