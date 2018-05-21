using Pensum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Think;
using Think.Util;
using static Pensum.ImageReader;

namespace ThinkRest
{
    public class Util
    {

        public static NeuralNetwork Brain = new NeuralNetwork(784, 64, 3);
        public static IEnumerable<Dictionary<DATA_SET_TYPE, List<Doodle>>> Data
        {
            get
            {
                return new List<double>
                            {
                                DoodleType.apple,
                                DoodleType.bird,
                                DoodleType.compass
    }.Select(GetDoodleData);
            }
        }

        public static void SetLearningRate(double learningRate)
        {
            Brain.LearningRate = learningRate;
        }

        public static void TrainEpoch()
        {

            var trainingData = Data.Select(doodleSet => doodleSet[DATA_SET_TYPE.training])
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
                Brain.Train(inputs, targets);
            }
        }

        public static double GetErrorRate()
        {
            var data = new List<double>
                            {
                                DoodleType.apple,
                                DoodleType.bird,
                                DoodleType.compass
                            }.Select(GetDoodleData);

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
                var result = Brain.FeedForward(doodle.Pixels);
                var total = result.Aggregate(0.0, (acc, res) => acc + res);
                var percentiles = result.Select(res => res / total).ToList();
                var errorRate = targets[(int)doodle.Labels[0] - 1] - percentiles[(int)doodle.Labels[0] - 1];
                errorRates.Add(errorRate);
            }
            return errorRates.Aggregate(0.0, (acc, errors) => acc + errors) / errorRates.Count;
        }
    }
}