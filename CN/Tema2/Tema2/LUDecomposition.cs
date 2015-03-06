using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Tema2
{
    public class LUDecomposition
    {
        private double _precision;

        public double[] InitialMatrix { get; private set; }
        public double[] Matrix { get; private set; }

        public double[] LMatrix
        {
            get
            {
                var matrix = new double[MatrixSize*MatrixSize];
                for (int i = 0; i < MatrixSize; i++)
                {
                    matrix[i*MatrixSize + i] = 1;
                }

                for (int i = 1; i < MatrixSize; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        matrix[i*MatrixSize + j] = Matrix[i*MatrixSize + j];
                    }
                }

                return matrix;
            }
        }

        public double[] UMatrix
        {
            get
            {
                var matrix = new double[MatrixSize * MatrixSize];
                for (int i = 0; i < MatrixSize; i++)
                {
                    for (int j = i; j < MatrixSize; j++)
                    {
                        matrix[i * MatrixSize + j] = Matrix[i * MatrixSize + j];
                    }
                }

                return matrix;
            }
        }

        private bool _decomposed = false;

        public int MatrixSize { get; private set; }

        public void SetMatrix(double[] matrix)
        {
            var size = Math.Sqrt(matrix.Length);
            if (size == (int) size)
            {
                InitialMatrix = matrix;
                Matrix = matrix;
                MatrixSize = (int) size;
            }
            else
                throw new InvalidDataException("Invalid matrix size");
        }

        public LUDecomposition(int precisionPower)
        {
            _precision = Math.Pow(10, precisionPower);
        }

        public double[] Decompose()
        {
            double u, l;

            if (!_decomposed)
                for (int p = 0; p < MatrixSize; p++)
                {
                    for (int i = 0; i < MatrixSize; i++)
                    {
                        u = AGet(p, i);
                        u -= GetSumForU(p, i);
                        SetU(p, i, u);

                        l = AGet(i, p);
                        l -= GetSumForL(p, i);
                        if (Math.Abs(UGet(p, p)) > _precision)
                            l /= UGet(p, p);
                        else throw new DivideByZeroException();
                        SetL(i, p, l);
                    }
                    _decomposed = true;
                }

            return Matrix;
        }

        private double GetSumForU(int step, int index)
        {
            double sum = 0;

            for (int k = 0; k < step; k++)
            {
                sum += LGet(step, k)*UGet(k, index);
            }

            return sum;
        }

        private double GetSumForL(int step, int index)
        {
            double sum = 0;

            for (int k = 0; k < step; k++)
            {
                sum += LGet(index, k)*UGet(k, step);
            }

            return sum;
        }

        private double AGet(int i, int j)
        {
            return Matrix[i*MatrixSize + j];
        }

        private double LGet(int i, int j)
        {
            double value = 0;

            if (i >= j)
            {
                if (i == j)
                    value = 1;
                else value = AGet(i, j);
            }

            return value;
        }

        private double UGet(int i, int j)
        {
            double value = 0;

            if (i <= j)
                value = AGet(i, j);

            return value;
        }

        private void SetL(int i, int j, double value)
        {
            if (i > j)
                Matrix[i*MatrixSize + j] = value;
        }

        private void SetU(int i, int j, double value)
        {
            if (i <= j)
                Matrix[i*MatrixSize + j] = value;
        }
    }

    public static class MatrixExtensions
    {
        public static string NiceMatrixDisplay(this double[] matrix)
        {
            var sb = new StringBuilder();
            var size = (int) Math.Sqrt(matrix.Length);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    sb.Append(" ");
                    sb.Append(matrix[i*size + j].ToString("F02"));
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}