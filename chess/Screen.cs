using chess.board;
using System.Net.Quic;

namespace chess
{
    internal class Screen
    {
        private const string separator = " ";
        private const string emptySpace = "-";
        private const int maxAlphabetQty = 26;
        private const int minAlphabetQty = 1;

        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.lines; i++)
            {
                //Printing board separator.
                Console.Write(board.lines - i + separator);
                for (int j = 0; j < board.rows; j++)
                {
                    if (board.GetPiece(i, j) == null)
                    {
                        Console.Write(emptySpace + separator);
                    }
                    else
                    {
                        PrintPiece(board.GetPiece(i, j));
                    }
                }
                Console.WriteLine();
            }
            PrintAlphabet(board.rows);
        }

        public static void PrintPiece(Piece piece)
        {
            //Using aux to keep old values
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = (ConsoleColor) piece.colorPieces;

            Console.Write(piece + separator);

            Console.ForegroundColor = aux;
        }

        public static void PrintAlphabet(int qty)
        {
            if (qty < minAlphabetQty || qty > maxAlphabetQty)
            {
                return;
            }

            Console.Write(separator + separator);
            for (int i = 0; i < qty; i++)
            {
                // 'a' has the ASCII code 97. Summ with index 'i', we go futher in the alphabet.
                char letter = (char) ('a' + i);
                Console.Write(letter + separator);
            }
        }

        public static HumanBoardPosition ScreenCordinators()
        {
            string input = "" + Console.ReadLine();
            char column = input[0];
            int line = int.Parse("" + input[1]);
            HumanBoardPosition position = new HumanBoardPosition(column, line);

            return position;
        }
    }
}
