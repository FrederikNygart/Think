using System;
using System.Collections.Generic;
using System.Text;
using Think.Util;

namespace Think
{
    public class Matrix
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public double[,] Data { get; set; }

        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            InitMatrix(rows, cols);
        }

        private double[,] InitMatrix(int rows, int cols)
        {

            Data = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Data[i, j] = 0;
                };
            };

            return Data;
        }


        public static double[,] Add(double[,] matrix, int number)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] += number;
                };
            };
            return matrix;
        }

        public static double[,] Add(double[,] matrixA, double[,] matrixB)
        {

            var aNumRows = matrixA.GetLength(0);
            var aNumCols = matrixA.GetLength(1);
            var bNumRows = matrixB.GetLength(0);
            var bNumCols = matrixB.GetLength(1);

            if (aNumRows != bNumRows || aNumCols != bNumCols)
                throw new FormatException(
                    "Amount of rows and columns in Matrix A must " +
                    "be equal to amount in Matrix B");

            var result = new double[aNumRows, bNumCols];
            for (var r = 0; r < aNumRows; r++)
            {
                for (var c = 0; c < bNumCols; c++)
                {
                    result[r, c] = matrixA[r, c] + matrixB[r, c];
                };
            };
            return result;
        }

        internal static List<double> ToArray(double[,] outputProduct)
        {
            var result = new List<double>();
            for (var r = 0; r < outputProduct.GetLength(0); r++)
            {
                for (var c = 0; c < outputProduct.GetLength(1); c++)
                {
                    result.Add(outputProduct[r, c]);
                };
            };
            return result;
        }

        

        public static double[,] Subtract(double[,] targetMatrix, double[,] outputMatrix)
        {
            var aNumRows = targetMatrix.GetLength(0);
            var aNumCols = targetMatrix.GetLength(1);
            var bNumRows = outputMatrix.GetLength(0);
            var bNumCols = outputMatrix.GetLength(1);

            if (aNumRows != bNumRows || aNumCols != bNumCols)
                throw new FormatException(
                    "Amount of rows and columns in Matrix A must " +
                    "be equal to amount in Matrix B");

            var result = new double[aNumRows, bNumCols];
            for (var r = 0; r < aNumRows; r++)
            {
                for (var c = 0; c < bNumCols; c++)
                {
                    result[r, c] = targetMatrix[r, c] - outputMatrix[r, c];
                };
            };
            return result;
        }

        public static double[,] ToMatrix(List<double> input)
        {
            var matrix = new double[input.Count, 1];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                matrix[i, 0] = input[i];
            };

            return matrix;
        }

        public double[,] Add(int number)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Data[i, j] += number;
                };
            };
            return Data;
        }

        public double[,] Add(int[,] matrix)
        {
            if (Cols != matrix.GetLength(0))
            {
                Console.Write("GET OUT OF HERE!");
                return new double[1, 1];
            }
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Data[i, j] += matrix[i, j];
                };
            };
            return Data;
        }


        public double[,] Multiply(int number)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    Data[i, j] *= number;
                };
            };
            return Data;
        }

        public static double[,] Multiply(double[,] matrix, double val)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] *= val;
                };
            };

            return matrix;
        }

        public static double[,] Multiply(double[,] matrixA, double[,] matrixB)
        {
            var aNumRows = matrixA.GetLength(0);
            var aNumCols = matrixA.GetLength(1);
            var bNumRows = matrixB.GetLength(0);
            var bNumCols = matrixB.GetLength(1);

            if (aNumCols != bNumRows)
                throw new FormatException(
                    "Amount of columns in Matrix A must " +
                    "be equal to amount of Rows in Matrix B");

            var result = new double[aNumRows, bNumCols];
            for (var r = 0; r < aNumRows; r++)
            {
                for (var c = 0; c < bNumCols; c++)
                {
                    double sum = 0;
                    for (var i = 0; i < aNumCols; i++)
                    {
                        sum += matrixA[r, i] * matrixB[i, c];
                    };
                    result[r, c] = sum;
                };
            };
            return result;
        }


        public static double[,] Map(double[,] matrix, Func<double, double> transformer)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    var val = matrix[i, j];
                    matrix[i, j] = transformer(val);
                };
            };
            return matrix;
        }


        public double[,] Transpose()
        {
            var result = new double[Cols, Rows];
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    result[c, r] = Data[r, c];
                };
            };
            return result;
        }

        public static double[,] Transpose(double[,] matrix)
        {
            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            var result = new double[cols, rows];
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    result[c, r] = matrix[r, c];
                };
            };
            return result;
        }

        public double[,] Random()
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Cols; j++)
                {
                    Data[i, j] = RandomGenerator.Double(-1, 1);
                };
            };
            return Data;
        }
    }
}
