using System;
using System.Text;

namespace Tema2
{
    public class EcuationSolver
    {
        private double _precision;
        private int _matrixSize;
        private double[] _matrix;
        private double[] _initialVector;
        private double[] _LSystemSolution;
        private double[] _Solution;

        public EcuationSolver(int precisionPower)
        {
            _precision = Math.Pow(10, precisionPower);
        }

        public void Prepare(double[] matrix, double[] vector)
        {
            var size = Math.Sqrt(matrix.Length);
            if (size == (int) size && vector.Length == (int) size)
            {
                _matrixSize = (int)size;
                _matrix = new double[_matrixSize*_matrixSize];
                _initialVector = new double[_matrixSize];

                matrix.CopyTo(_matrix, 0);
                vector.CopyTo(_initialVector, 0);
            }
        }

        public double[] ComputeSolution()
        {
            SolveLSystem();
            SolveUSystem();
            return _Solution;
        }

        private double LGet(int i, int j)
        {
            double value = 0;

            if (i >= j)
            {
                if (i == j)
                    value = 1;
                else value = _matrix[i * _matrixSize + j];
            }

            return value;
        }

        private double UGet(int i, int j)
        {
            double value = 0;

            if (i <= j)
                value = _matrix[i * _matrixSize + j];

            return value;
        }

        private double ComputeSumForL(int index)
        {
            double result = 0;

            for (int i = 0; i < index; i++)
            {
                result += LGet(index, i)*_LSystemSolution[i];
            }

            return result;
        }

        private void SolveLSystem()
        {
            _LSystemSolution = new double[_matrixSize];

            for (int i = 0; i < _matrixSize; i++)
            {
                _LSystemSolution[i] = (_initialVector[i] - ComputeSumForL(i))/LGet(i,i);
            }
        }

        private double ComputeSumForU(int index)
        {
            double result = 0;

            for (int i = index+1; i < _matrixSize; i++)
            {
                result += UGet(index, i)*_Solution[i];
            }

            return result;
        }

        private void SolveUSystem()
        {
            _Solution = new double[_matrixSize];

            for (int i = _matrixSize - 1; i >= 0; i--)
            {
                _Solution[i] = (_LSystemSolution[i] - ComputeSumForU(i))/UGet(i,i);
            }
            
        }
    }

    public static class ArrayExtensions
    {
        public static string NiceSolutionDisplay(this double[] array)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < array.Length; i++)
            {
                sb.AppendLine(array[i].ToString("F02"));
            }

            return sb.ToString();
        }
    }
}

