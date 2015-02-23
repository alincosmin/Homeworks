namespace Tema2
{
    class Program
    {
        static void Main(string[] args)
        {
            var LU = new LUDecomposition(15);
            var matrix = new int[]
            {
                4, 2, 6,
                2, 4, 6,
                2, 2, 5
            };

            LU.SetMatrix(matrix);
        }
    }
}
