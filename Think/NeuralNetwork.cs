using System;
using System.Collections.Generic;
using System.Text;

namespace Think
{
    public class NeuralNetwork
    {
        private int Inputs;
        private int HiddenNodes;
        private int Outputs;
        private double[,] HiddenProduct;
        private double[,] OutputProduct;

        public double LearningRate { get; }
        public Matrix WeightsInputToHidden { get; }
        public Matrix WeightsHiddenToOutput { get; }
        public Matrix BiasHidden { get; }
        public Matrix BiasOutput { get; }

        public NeuralNetwork(
            int inputs,
            int hiddenNodes,
            int outputs 
            )
        {
            Inputs = inputs;
            HiddenNodes = hiddenNodes;
            Outputs = outputs;

            LearningRate = 0.1;

            WeightsInputToHidden = new Matrix(hiddenNodes, inputs);
            WeightsHiddenToOutput = new Matrix(outputs, hiddenNodes);

            BiasHidden = new Matrix(hiddenNodes, 1);
            BiasOutput = new Matrix(outputs, 1);

            InitWeights();
            InitBias();
        }

        public List<double> FeedForward(List<double> input)
        {
            var inputs = Matrix.ToMatrix(input);
            var hidden = Matrix.Multiply(WeightsInputToHidden.Data, inputs);
            var hiddenWithBias = Matrix.Add(hidden, BiasHidden.Data);
            HiddenProduct = Matrix.Map(hiddenWithBias, Sigmoid);

            var output = Matrix.Multiply(WeightsHiddenToOutput.Data, HiddenProduct);
            var outputWithBias = Matrix.Add(output, BiasOutput.Data);
            OutputProduct = Matrix.Map(outputWithBias, Sigmoid);

            return Matrix.ToArray(OutputProduct);
        }

        public double Sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }

        public double SigmoidDeriviative(double sig)
        {
            return sig*(1-sig);
        }

        public double[,] Train(List<double> input, List<double> targets)
        {
            var outputProduct = Matrix.ToMatrix(FeedForward(input));
            var targetMatrix = Matrix.ToMatrix(targets);
            var inputs = Matrix.ToMatrix(input);

            //Calc error Hidden to Output
            var outputError = Matrix.Subtract(targetMatrix, outputProduct);
            
            //Set Hidden to Output weights based on the delta
            WeightsHiddenToOutput.Data = Matrix.Add(CalcDelta(HiddenProduct, outputProduct, outputError), WeightsHiddenToOutput.Data);

            //Calc error input to hidden
            var transposedHtOWeights = Matrix.Transpose(WeightsHiddenToOutput.Data);
            var hiddenError = Matrix.Multiply(transposedHtOWeights, outputError);

            //Set Input to Hidden weights based on the delta
            WeightsInputToHidden.Data = Matrix.Add(CalcDelta(inputs, HiddenProduct, hiddenError), WeightsInputToHidden.Data);

            return hiddenError;
        }

        private double[,] CalcDelta(double[,] cause, double[,] result, double[,] error)
        {
            var gradients = Matrix.Map(result, SigmoidDeriviative);
            gradients = Matrix.Multiply(gradients, error);
            gradients = Matrix.Multiply(gradients, LearningRate);
            var transposedCause = Matrix.Transpose(cause);
            return Matrix.Multiply(gradients, transposedCause);
        }


        ///**************** PRIVATE *******************////

        private void InitBias()
        {
            //Randomize bias
            BiasHidden.Random();
            BiasOutput.Random();
        }

        private void InitWeights()
        {
            //Randomize weights
            WeightsInputToHidden.Random();
            WeightsHiddenToOutput.Random();
        }
    }
}
