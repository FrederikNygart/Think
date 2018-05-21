using Pensum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Think;
using Think.Util;
using static Pensum.ImageReader;

namespace Debugger
{
    class Program
    {
        public static NeuralNetwork nn = new NeuralNetwork(784, 64, 3);
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
            TestDoodleClassifier();
            Console.ReadKey();
        }

        public static void TestDoodleClassifier()
        {
            Stopwatch stopwatch = Stopwatch.StartNew(); //creates and start the instance of Stopwatch
            var data = new List<double>
                            {
                                DoodleType.apple,
                                DoodleType.bird,
                                DoodleType.compass
                            }.Select(GetDoodleData);

            Console.WriteLine($"Data prepared finished after: {stopwatch.ElapsedMilliseconds}ms");
            var error = TestRunPredict(data, stopwatch);
            Console.WriteLine($"Error predicted {error} after: {stopwatch.ElapsedMilliseconds}ms");

            do
            {
                TestRunEpoch(data, stopwatch);
                error = TestRunPredict(data, stopwatch);
                Console.WriteLine($"Error predicted {error} after: {stopwatch.ElapsedMilliseconds}ms");
            }
            while (error > 0.10);

        }

        public static double TestRunPredict(IEnumerable<Dictionary<DATA_SET_TYPE, List<Doodle>>> data, Stopwatch stopwatch)
        {
            var testData = data.Select(dataSet => dataSet[DATA_SET_TYPE.test])
                                .Aggregate((acc, item) => acc.Concat(item).ToList());
            var errorRates = new List<double>();

            testData.Shuffle();
            for (var i = 0; i < testData.Count; i++)
            {
                var doodle = testData[i];
                var targets = new List<double>();
                for (int t = 0; t < 3; t++)
                {
                    targets.Add(t == doodle.Labels[0] - 1 ? 1 : 0);
                }
                var result = nn.FeedForward(doodle.Pixels);
                var total = result.Aggregate(0.0, (acc, res) => acc + res);
                var percentiles = result.Select(res => res / total).ToList();
                var errorRate = targets[(int)doodle.Labels[0] - 1] - percentiles[(int)doodle.Labels[0] - 1];
                errorRates.Add(errorRate);
            }
            return errorRates.Aggregate(0.0, (acc, errors) => acc + errors) / errorRates.Count;

        }

        public static void TestRunEpoch(IEnumerable<Dictionary<DATA_SET_TYPE, List<Doodle>>> data, Stopwatch stopwatch)
        {

            var trainingData = data.Select(doodleSet => doodleSet[DATA_SET_TYPE.training])
                                    .Aggregate((acc, item) => acc.Concat(item).ToList());
            trainingData.Shuffle();

            for (var i = 0; i < trainingData.Count; i++)
            {
                var doodle = trainingData[i];
                var inputs = doodle.Pixels.Select(pixel => pixel / 255.0).ToList();
                var targets = new List<double>();
                for (int t = 0; t < 3; t++)
                {
                    targets.Add(t == doodle.Labels[0] - 1 ? 1 : 0);
                }
                nn.Train(inputs, targets);
            }
            Console.WriteLine($"Trained for one epoch finishe after: {stopwatch.ElapsedMilliseconds}ms");
        }

        public static void TestNeuralNetwork()
        {
            var trainingData = new TestMaterial().TestData;
            var nn = new NeuralNetwork(2, 4, 1);
            for (var i = 0; i < 50000; i++)
            {
                var num = RandomGenerator.Integer(trainingData.Count);
                var data = trainingData[num];
                nn.Train(data["inputs"], data["targets"]);
            }
            Console.WriteLine(nn.FeedForward(new List<double> { 1, 0 })[0]);
            Console.WriteLine(nn.FeedForward(new List<double> { 0, 1 })[0]);
            Console.WriteLine(nn.FeedForward(new List<double> { 1, 1 })[0]);
            Console.WriteLine(nn.FeedForward(new List<double> { 0, 0 })[0]);
        }


        public static void TestMatrix()
        {
            var a = new Matrix(2, 3);
            var b = new Matrix(3, 2);

            a.Random();

            PrintMatrix(a.Data);

            Matrix.Map(a.Data, x => x * 2);

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
