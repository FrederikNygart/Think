using System;
using System.Collections.Generic;
using System.Text;

namespace Think
{
    public class Matrix
    {
        private int v1;
        private int v2;

        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public int[,] Data { get; private set; }

        public Matrix(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            InitMatrix(rows, cols);
        }

        private int[,] InitMatrix(int rows, int cols)
        {

            Data = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Data[i, j] = 0;
                };
            };

            return Data;
        }

        public int[,] Add(int number)
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

        public int[,] Add(int[,] matrix)
        {
            if (Cols != matrix.GetLength(0))
            {
                Console.Write("GET OUT OF HERE!");
                return new int[1, 1];
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


        public int[,] Multiply(int number)
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

        public int[,] Multiply(int[,] matrix)
        {
            if (Cols != matrix.GetLength(0))
            {
                Console.Write("GET OUT OF HERE!");
                return new int[1, 1];
            }
            var newCols = matrix.GetLength(1);
            var result = new int[Rows, newCols];
            for (var r = 0; r < Rows; r++)
            {
                for (var c = 0; c < newCols; c++)
                {
                    var sum = 0;
                    for (var k = 0; k < Cols; k++)
                    {
                        sum += Data[r, k] * matrix[k, c];
                    };
                    result[r, c] = sum;
                };
            };
            return result;
        }

        public int[,] Transpose()
        {
            var result = new int[Cols, Rows];
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Cols; c++)
                {
                    result[c, r] = Data[r, c];
                };
            };
            return result;
        }

        public  int[,] Random()
        {
            for (var i = 0; i < Rows; i++)
            {
                for (var j = 0; j < Cols; j++)
                {
                    Data[i, j] += new Random().Next(1, 10);
                };
            };
            return Data;
        }
    }
}
