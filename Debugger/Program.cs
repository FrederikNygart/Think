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
            var nn = new NeuralNetwork(2, 2, 2);
            var input = new List<double> { 1, 0 };
            var targets =  new List<double> { 1, 0 };

            PrintMatrix(nn.Train(input, targets));

            //foreach (var put in output)
            //{
            //    Console.WriteLine(put);
            //}

            Console.ReadKey();
        }


        public static void TestMatrix()
        {
            var a = new Matrix(2, 3);
            var b = new Matrix(3, 2);

            a.Random();

            PrintMatrix(a.Data);

            Matrix.Map(a.Data , x => x * 2);

            PrintMatrix(a.Data);
        }

        public static void PrintMatrix(double[,] m)
        {
            var counter = 0;
            var numOfCols = m.GetLength(1);
            var rowNum = 0;
            int colNum = 0;
            Console.WriteLine($"");
            Console.WriteLine($"");
            Console.WriteLine($"MATRIX");
            Console.WriteLine($"-----------------------------------------------------------------");
            foreach (var field in m)
            {
                colNum = (counter % numOfCols) + 1;
                var currentRow = (counter / numOfCols) + 1;
                if (rowNum < currentRow)
                {

                    rowNum = (counter / numOfCols) + 1;
                    Console.WriteLine($"");
                }

                Console.Write($"   {field}   ");

                counter++;
            }
            Console.WriteLine($"");
            Console.WriteLine($"");
            Console.WriteLine($"-----------------------------------------------------------------");
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
