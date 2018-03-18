using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math
{
    public class Matrix
    {
        public Matrix(int rows, int cols)
        {
            Cols = cols;
            Rows = rows;
            _Matrix = new int[Rows, Cols];
        }

        public int Cols { get; }
        public int Rows { get; }

        private int[,] _Matrix;
    }
0}
