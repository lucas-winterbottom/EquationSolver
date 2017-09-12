// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace Equ
// {
//     class Test
//     {
//         InputReader reader = new InputReader();
//         InputParser solver;

//         List<String> tests = new List<String>();
//         List<double> answers = new List<double>();

//         public Test()
//         {
//             Initialise();
//             RunTests();
//         }

//         /**
//          * Loop through the test cases and output to the console
//          */
//         private void RunTests()
//         {
//             String ans;
//             int passed = 0, failed = 0;
//             List<int> fails = new List<int>();

//             for (int i = 0; i < tests.Count; i++)
//             {


//             }
//                 else
//                 {
//                 Console.WriteLine("Invalid command! (?)");
//             }
//             Console.WriteLine();



//         }

//         /**
//          * Test cases
//          */
//         private void Initialise()
//         {
//             //addition and subtraction
//             tests.Add("calc a + 7 = 15");
//             answers.Add(8);

//             tests.Add("calc y + 12 = 23");
//             answers.Add(11);

//             tests.Add("calc x + 4 = 14");
//             answers.Add(10);

//             tests.Add("calc 6 + g = 13");
//             answers.Add(7);

//             tests.Add("calc 7 + w = 3");
//             answers.Add(-4);

//             tests.Add("calc 11 + z = -2");
//             answers.Add(-13);

//             tests.Add("calc b - 5 = 4");
//             answers.Add(9);

//             tests.Add("calc m - 9 = 9");
//             answers.Add(18);

//             tests.Add("calc c - 6 = 15");
//             answers.Add(21);

//             tests.Add("calc 12 - q = 5");
//             answers.Add(7);

//             tests.Add("calc 8 - h = 2");
//             answers.Add(6);

//             tests.Add("calc -3 = n - - 2");
//             answers.Add(-5);

//             //multiplication and division
//             tests.Add("calc 5n = 15");
//             answers.Add(3);

//             tests.Add("calc 6x = 30");
//             answers.Add(5);

//             tests.Add("calc 3c = - 12");
//             answers.Add(-4);

//             tests.Add("calc 2b = 15");
//             answers.Add(7.5);

//             tests.Add("calc 4m = -21");
//             answers.Add(-5.25);

//             tests.Add("calc -25 = 6b");
//             answers.Add(-4.16666666666667);

//             tests.Add("calc s/2 = 8");
//             answers.Add(16);

//             tests.Add("calc y/5 = 6");
//             answers.Add(30);

//             tests.Add("calc a/11 = -7");
//             answers.Add(77);

//             tests.Add("calc 12 = g/2");
//             answers.Add(24);

//             tests.Add("calc c/-15 = -3");
//             answers.Add(45);

//             tests.Add("calc 3 = k/-3");
//             answers.Add(-9);

//             //linear equations with int answers
//             tests.Add("calc 5w + 4 = 29");
//             answers.Add(5);

//             tests.Add("calc 2t + 6 = 12");
//             answers.Add(t = 3);

//             tests.Add("calc 7x - 6 = 22");
//             answers.Add(4);

//             tests.Add("calc 5y - 10 = -15");
//             answers.Add(-1);

//             tests.Add("calc 9m - 2 = -11");
//             answers.Add(-1);

//             tests.Add("calc 12 = 3a - 9");
//             answers.Add(7);

//             tests.Add("calc 5 + 2e = 13");
//             answers.Add(4);

//             tests.Add("calc 4 + 3b = 7");
//             answers.Add(1);

//             tests.Add("calc 32 = 17 - 3k");
//             answers.Add(-5);

//             tests.Add("calc 16 = 10 + 2w");
//             answers.Add(3);

//             tests.Add("calc 70 - 10d = 80");
//             answers.Add(-1);

//             tests.Add("calc 98 = 38 - 10z");
//             answers.Add(-6);

//             //linear equations with fraction answers
//             tests.Add("calc 3q + 6 = 19");
//             answers.Add("q = 4.33333333333333");

//             tests.Add("calc 4r + 2 = 27");
//             answers.Add("r = 6.25");

//             tests.Add("calc 2x + 11 = 14");
//             answers.Add("x = 1.5");

//             tests.Add("calc 31 = 4 + 4a");
//             answers.Add("a = 6.75");

//             tests.Add("calc 18 = 17 + 5m");
//             answers.Add("m = 0.2");

//             tests.Add("calc 12 = 29 + 2k");
//             answers.Add("k = -8.5");

//             tests.Add("calc 2c - 7 = 6");
//             answers.Add("c = 6.5");

//             tests.Add("calc 5e - 1 = 15");
//             answers.Add("e = 3.2");

//             tests.Add("calc 6p - 4 = 11");
//             answers.Add("p = 2.5");

//             tests.Add("calc 28 = 2r + 29");
//             answers.Add("r = 4.5");

//             tests.Add("calc 37 = 3w - 7");
//             answers.Add("w = 14.6666666666667");

//             tests.Add("calc 16 = 9c + 8");
//             answers.Add("c = 0.888888888888889");

//             //linear equations with multiple pronums
//             tests.Add("calc 4d + 4 = 3d + 9");
//             answers.Add("d = 5");

//             tests.Add("calc 5x + 3 = 4x - 5");
//             answers.Add("x = -8");

//             tests.Add("calc 7a + 2 = 4a - 10");
//             answers.Add("a = -4");

//             tests.Add("calc 10y - 4 = 2 + 4y");
//             answers.Add("y = 1");

//             tests.Add("calc 8m + 2 = -17 + 3m");
//             answers.Add("m = -3.8");

//             tests.Add("calc 7c - 4 = 7 + c");
//             answers.Add("c = 1.83333333333333");

//             tests.Add("calc 7 + s = 8s - - 9");
//             answers.Add("s = -0.285714285714286");

//             tests.Add("calc 13 + 2z = 6z + 3");
//             answers.Add("z = 2.5");

//             tests.Add("calc 1 + 5b = 8 - 2b");
//             answers.Add("b = 1");

//             tests.Add("calc -5 + 2x = -3 + 8x");
//             answers.Add("x = -0.333333333333333");

//             tests.Add("calc 4 - - 2a = 5a + 1");
//             answers.Add("a = 1");

//             tests.Add("calc 2w - 7 + 4w = 8");
//             answers.Add("w = 2.5");

//             //brackets

//             //quadratics

//             //garbage input

//             //sample questions from assessment paper
//         }
//     }
// }
