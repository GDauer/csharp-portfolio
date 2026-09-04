using chess.board;
using chess.game;

namespace chess
{
    internal class Screen
    {
        private const string Separator = " ";
        private const string EmptySpace = "-";
        private const int MaxAlphabetQty = 26;
        private const int MinAlphabetQty = 1;

        public static void PrintBoard(Board board)
        {
            for (int i = 0; i < board.Lines; i++)
            {
                //Printing board separator.
                Console.Write(board.Lines - i + Separator);
                for (int j = 0; j < board.Rows; j++)
                {
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            PrintAlphabet(board.Rows);
        }

        public static void PrintBoard(Board board, bool[,] possibleMovements)
        {
            ConsoleColor defaultBackground = Console.BackgroundColor;
            ConsoleColor possibleBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                //Printing board separator.
                Console.BackgroundColor = defaultBackground;
                Console.Write(board.Lines - i + Separator);
                for (int j = 0; j < board.Rows; j++)
                {
                    Console.BackgroundColor = defaultBackground;
                    if (possibleMovements[i, j] == true)
                    {
                        Console.BackgroundColor = possibleBackground;
                    }
                    PrintPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = defaultBackground;
            PrintAlphabet(board.Rows);
        }

        public static void PrintPiece(Piece? piece)
        {
            if (piece == null)
            {
                Console.Write(EmptySpace + Separator);
                return;
            }

            //Using aux to keep old values
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = (ConsoleColor) piece.ColorPieces;

            Console.Write(piece + Separator);

            Console.ForegroundColor = aux;
        }

        public static void PrintAlphabet(int qty)
        {
            if (qty < MinAlphabetQty || qty > MaxAlphabetQty)
            {
                return;
            }

            Console.Write(Separator + Separator);
            for (int i = 0; i < qty; i++)
            {
                // 'a' has the ASCII code 97. Summ with index 'i', we go futher in the alphabet.
                char letter = (char) ('a' + i);
                Console.Write(letter + Separator);
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
