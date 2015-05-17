using MathNet.Numerics.LinearAlgebra.Double.Solvers;

namespace Tema3
{
    public class QRDecomposer
    {
        private double[] _vector;
        private double[][] _matrix;

        public QRDecomposer(double[][] matrix, double[] vector)
        {
            matrix.CopyTo(_matrix, 0);
            vector.CopyTo(_vector, 0);
        }

        public double[][] ComputeP(int size)
        {
            return null;
        }
    }
}