using System;
using System.IO;

namespace Tema2
{
    public class LUDecomposition
    {
        private double _precision;

        public int[] InitialMatrix { get; private set; }
        public int[] Matrix { get; private set; }

        public int MatrixSize { get; private set; }

        public void SetMatrix(int[] matrix)
        {
            var size = Math.Sqrt(matrix.Length);
            if (size == (int) size)
            {
                InitialMatrix = matrix;
                Matrix = matrix;
            }
            else
                throw new InvalidDataException("Invalid matrix size");
        }

        public LUDecomposition(int negativePower)
        {
            _precision = Math.Pow(10, (-1)*negativePower);
        }

        public object[] Decompose()
        {
            return new object[]
            {
                new int[]{0},
                new int[]{1}
            };
        }
    }
}