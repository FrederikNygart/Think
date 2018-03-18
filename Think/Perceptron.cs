using System;
using System.Collections.Generic;
using System.Linq;
using Think.Util;

namespace Think
{
    public class Perceptron
    {
        public List<double> Weights = new List<double> { 0, 0};
        public double LearningRate = 0.01;


        public Perceptron()
        {
            InitWeights();
        }

        public int Guess(List<double> inputs)
        {
            double sum = Weights.Select((weight, i) => weight * inputs[i]).Sum();
            return Sign(sum);
        }

        //Activation
        private int Sign(double sum)
        {
            return sum > 0 ? 1 : -1;
        }

        //Weight initiation - random for now
        private void InitWeights()
        {
            Weights = Weights.Select(w => w = RandomGenerator.Double(-1, 1)).ToList();
        }

        public void Train(List<double> inputs, int target)
        {
            int error = CalcError(target, Guess(inputs));

            for (int i = 0; i < Weights.Count; i++)
            {
                var adjustment = error * inputs[i] * LearningRate;
                Weights[i] += adjustment;
            }
            //tune weights
            //Weights.Select((weight, i) => weight += error * inputs[i] * LearningRate);
        }

        //Error calculation
        public int CalcError(int expected, int actual)
        {
            return expected - actual;
        }
    }
}
