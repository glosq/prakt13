namespace libmas
{
    public class RadomMatrix
    {
        public static void InitMatrix(out int[,] matrix, int column, int row, int randMax)
        {
            Random rnd; rnd = new Random();
            matrix = new int[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = rnd.Next(randMax);
                }
            }
        }
    }
}
