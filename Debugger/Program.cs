using Pensum;
using System;
using System.Collections.Generic;
using System.Linq;
using Think;

namespace Debugger
{
    class Program
    {
        public static List<TestMaterial> materials
        {
            get
            {
                var m = new List<TestMaterial>();
                for (int i = 0; i < 300; i++)
                {
                    m.Add(new TestMaterial());
                }
                return m;
            }
        }

        public static int Counter = 0;

        static void Main(string[] args)
        {
            var a = new Matrix(2, 3);
            var b = new Matrix(3, 2);

            a.Random();
            b.Random();

            PrintMatrix(a.Data, 3);
            Console.WriteLine($"-----------------------------------------------------------------");

            //PrintMatrix(b.Matrix, 2);
            //Console.WriteLine($"-----------------------------------------------------------------");

            PrintMatrix(a.Transpose(),2);
            Console.WriteLine($"-----------------------------------------------------------------");

            Console.ReadKey();
        }


        public static void PrintMatrix(int[,] m, int numOfCols)
        {
            Counter = 0;
            var rowNum = 0;
            int colNum = 0;
            foreach (var row in m)
            {
                colNum = (Counter % numOfCols) + 1;
                var currentRow = (Counter / numOfCols) + 1;
                if (rowNum < currentRow)
                {

                    rowNum = (Counter / numOfCols) + 1;
                    Console.WriteLine($"");
                    Console.WriteLine($"Row {rowNum}");
                }

                Console.Write($"Col {colNum}:  {row}  ");


                Counter++;
            }
            Console.WriteLine($"");

        }

        public static void TestTraining(Perceptron brain)
        {
            foreach (var m in materials)
            {
                var res = brain.Guess(m.Values.ToList());
                var target = m.Label;

                Console.WriteLine("**************************______________************************");

                Console.WriteLine($"{m.Values[0]} + {m.Values[1]} is below 100: {GuessResult(res)}");
                Console.WriteLine($"Guess was {GetResult(res, target)}!");

                Console.WriteLine($"Weights 1: {brain.Weights[0]} 2: {brain.Weights[1]}");

                Console.WriteLine($"The error is {brain.CalcError(res, target)}");

                Console.WriteLine("**************************______________************************");

                if (GetResult(res, target).Equals("CORRECT")) Counter++;

                brain.Train(m.Values.ToList(), target);
                Console.WriteLine("Training...");
            }
            Console.WriteLine($"Percentage correct: {(double)Counter / materials.Count}");
            var input = Console.ReadKey();

            if (!input.ToString().ToLower().Equals("n"))
            {
                Counter = 0;
                TestTraining(brain);
            }
            else
            {
                Console.WriteLine("END");
            }

        }

        public static string GuessResult(int res)
        {
            return res == 1
                ? "False"
                : "True";
        }

        public static string GetResult(int actual, int expected) => actual == expected ? "CORRECT" : "WRONG";

        public static void TestGuess(Perceptron brain)
        {
            Console.WriteLine("Guess: " + brain.Guess(new List<double> { -1, 0.5 }));
        }

        public static void TestPensum(TestMaterial m)
            => Console.WriteLine($"Material VALUE: {m.Values} LABEL: {m.Label}");
    }


}
