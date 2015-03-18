using System;
using System.Web.Script.Serialization;

namespace Tema3
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = new double[][]
            {
                new double[]{4,2,6},
                new double[]{2,4,6},
                new double[]{2,2,5} 
            };

            var initialVector = new double[] {14, 16, 12};

            var v1 = ComputeVector(matrix, initialVector);

            var json = new JavaScriptSerializer();
            Console.WriteLine(json.Serialize(v1));
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
