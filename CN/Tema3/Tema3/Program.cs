using System;
using System.Web.Script.Serialization;
using MathNet.Numerics.LinearAlgebra.Single;

namespace Tema3
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = new double[][]
            {
                new double[]{1,-1,4},
                new double[]{1,4,-2},
                new double[]{1,4,2},
                new double[]{1,-1, }, 
            };

            var initialVector = new double[] {12, 6, -4};

            var vector = ComputeVector(matrix, initialVector);

            var json = new JavaScriptSerializer();
            Console.WriteLine(json.Serialize(vector));


        }

        private static double[] ComputeVector(double[][] matrix, double[] initialVector)
        {
            int size = matrix.Length;
            var result = new double[size];

            for (int i = 0; i < size; i++)
            {
                double sum = 0;
                
                for (int j = 0; j < size; j++)
                {
                    sum += initialVector[j]*matrix[i][j];
                }

                result[i] = sum;
            }

            return result;
        }
    }
}
