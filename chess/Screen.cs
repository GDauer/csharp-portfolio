using chess.board;

namespace chess
{
    internal class Screen
    {
        private const string separator = " ";
        private const string emptySpace = "-";

        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.lines; i++)
            {
                for (int j = 0; j < board.rows; j++)
                {
                    if (board.GetPiece(i, j) == null)
                    {
                        Console.Write(emptySpace + separator);
                    }
                    else
                    {
                        Console.Write(board.GetPiece(i, j) + separator);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
