namespace Tema5
{
    public class RareMatrixNode
    {
        public double Value { get; set; }
        public int ColumnIndex { get; set; }

        public static RareMatrixNode Add(RareMatrixNode firstNode, RareMatrixNode secondNode)
        {
            return new RareMatrixNode()
            {
                Value = firstNode.Value + secondNode.Value,
                ColumnIndex = firstNode.ColumnIndex
            };
        }
    }
}