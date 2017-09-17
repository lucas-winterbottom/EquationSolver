using System;
using System.Collections.Generic;

namespace Equ
{
    public class Solver
    {
        private List<Term> lhs, rhs;
        public List<Term> Lhs { get => lhs; set => lhs = value; }
        public List<Term> Rhs { get => rhs; set => rhs = value; }

        //Constructor for working on an equation as a whole.
        public Solver(List<Term> lhs, List<Term> rhs)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            if (rhs.Count == 0) ErrorHandler.ExitWithMessage(Error.NoRHSContent, " Missing values on the right hand side of the equation");

        }

        //Constructor for working on terms within brackets
        public Solver(List<Term> lhs)
        {
            this.lhs = lhs;
        }

        //Runs through the required steps to solve for x given and left and right hand side
        //Solve brackets
        //Handle multiplication, division and modulus
        //Expand the brackets into the regular equation
        //Move all the pronumerals to the left and the numerals to the right
        //Add and subtract the terms on both side
        //Determine the method in which X can be calculated
        //Solve for X
        public void Solve()
        {
            Brackets(lhs);
            Brackets(rhs);
            MulDivideModulus(lhs);
            MulDivideModulus(rhs);
            ExpandBrackets(lhs);
            ExpandBrackets(rhs);
            MovePronumeralsLeft();
            BalanceLeft();
            PlusMinus();
            switch (DecideMethod())
            {
                case 1:
                    Console.WriteLine("Linear");
                    DivideNumeralByXCoeff();
                    break;
                case 2:
                    Console.WriteLine("Quadratic -- Exponential X");
                    DivideNumeralByXCoeff();
                    Squareroot();
                    break;
                case 3:
                    Console.WriteLine("Quadratic -- X & Exponential X");
                    SolveQuadratic();
                    break;
            }
        }

        //Handles the calculation of terms within brakcets
        public void SolveInterior()
        {
            MulDivideModulus(lhs);
        }

        //Once the brackets are solved they can be moved into the regular equation
        private void ExpandBrackets(List<Term> side)
        {
            for (int i = 0; i < side.Count; i++)
            {
                if (side[i].BracketsContent != null)
                {
                    foreach (Term t in side[i].BracketsContent)
                    {
                        side.Insert(i + 1, t);
                    }
                    side.RemoveAt(i);
                }
            }
        }

        //Makes the outside bracket term multiply with the interior terms.
        private void Brackets(List<Term> side)
        {
            foreach (Term t in side)
            {
                if (t.BracketsContent != null)
                {
                    t.WorkBrackets();
                }

            }
        }

        //Reorganises the Equation into ax^2 + bx + c and than executes the quadratic equation
        //Displays both X values from the equation both positive and negative
        private void SolveQuadratic()
        {
            if (rhs.Count == 0) rhs.Add(new Term(0));
            rhs[0].InvertValue();
            lhs.Add(rhs[0]);
            rhs.RemoveAt(0);

            double a = lhs[0].Coeff;
            double b = lhs[1].Coeff;
            double c = lhs[2].Coeff;

            double sqrtpart = b * b - 4 * a * c;
            double x1 = (-b + Math.Sqrt(sqrtpart)) / (2 * a);
            double x2 = (-b - Math.Sqrt(sqrtpart)) / (2 * a);
            PrintXs(x1, x2);

        }

        //Makes sure the x1 and x2 values are real numbers and then prints them in the required format
        private void PrintXs(double x1, double x2)
        {
            if (x1.Equals(Double.NaN) || x2.Equals(Double.NaN)) ErrorHandler.ExitWithMessage(Error.NotANumber, " Cause: " + rhs[0]);
            Console.WriteLine("X = " + x1 + "," + x2);
        }

        //Takes the first and only value on the RHS and takes the square root,
        //then prints the x values
        private void Squareroot()
        {
            if (rhs.Count == 0) rhs.Add(new Term(0));
            double x1 = Math.Sqrt(rhs[0].Coeff);
            double x2 = -Math.Sqrt(rhs[0].Coeff);
            PrintXs(x1, x2);
        }

        //Checks the LHS of the equation to determine what action to take to produce the value/s
        //of x
        private int DecideMethod()
        {
            int i = 0;
            foreach (Term t in lhs)
            {
                if (t.Type == TermType.variable) i++;
                if (t.Type == TermType.sqVariable) i += 2;
            }
            return i;
        }

        //Moves all the pronumeral Terms to the LHS of the equation
        private void MovePronumeralsLeft()
        {
            for (int i = 0; i < rhs.Count; i++)
            {
                if (!rhs[i].IsNumberz())
                {
                    rhs[i].InvertValue();
                    lhs.Add(rhs[i]);
                    rhs.RemoveAt(i);
                    i--;
                }
            }
        }

        //Divides the term on the right by the coefficient of the x term on the left
        private void DivideNumeralByXCoeff()
        {
            if (rhs.Count == 0) rhs.Add(new Term(0));
            rhs[0].Coeff = rhs[0].Coeff / lhs[0].Coeff;
            Console.WriteLine(lhs[0].ToString() + Constants.eq.ToString() + rhs[0].Coeff);
        }

        //Moves all the 'number' variables to the right hand side
        private void BalanceLeft()
        {
            for (int i = 0; i < lhs.Count; i++)
            {
                if (lhs[i].Modifier == Modifier.NONE && lhs[i].Type == TermType.number)
                {
                    lhs[i].InvertValue();
                    rhs.Add(lhs[i]);
                    lhs.RemoveAt(i);
                    i--;
                }
            }

        }

        //Going from left to right, add or subtract each compatible value,
        //since the terms are organised in such a way that they will most if,
        //not all be compatible they all should calculate
        public void PlusMinus()
        {
            lhs.Sort((x, y) => y.Type.CompareTo(x.Type));
            //LHS Plus (Should be only pronumerals)
            for (int i = 1; i < lhs.Count; i++)
            {
                if (lhs[i].Type == lhs[i - 1].Type)
                {
                    lhs[i].Coeff += lhs[i - 1].Coeff;
                    lhs.RemoveAt(i - 1);
                    i--;
                }
            }
            ///RHS Plus
            for (int i = 1; i < rhs.Count; i++)
            {
                if (rhs[i].IsNumberz() && rhs[i - 1].IsNumberz())
                {
                    rhs[i].Coeff += rhs[i - 1].Coeff;
                    rhs.RemoveAt(i - 1);
                    i--;
                }
            }
        }

        //Going from left to right, multiply, divide or modulus each compatible value,
        //terms that conatin pronumerals retain them if multiplied, and the operators
        //are overloaded to help simplify the code at this level
        private void MulDivideModulus(List<Term> side)
        {
            for (int i = 1; i < side.Count; i++)
            {
                switch (side[i].Modifier)
                {
                    case Modifier.MUL:
                        side[i] = side[i] * side[i - 1];
                        side.RemoveAt(i - 1);
                        i--;
                        break;
                    case Modifier.MOD:
                        side[i] = side[i - 1] % side[i];
                        side.RemoveAt(i - 1);
                        i--;
                        break;
                    case Modifier.DIV:
                        side[i] = side[i - 1] / side[i];
                        side.RemoveAt(i - 1);
                        i--;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
