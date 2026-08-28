using chess.board;
using chess.board.exceptions;
using chess.game;
using chess.game.pieces;

namespace chess
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);

                PositionChess chessPos = new PositionChess('a', 1);
                Console.WriteLine(chessPos.ToPosition());
                //Screen.printBoard(board);
            }
            catch (AddPieceToBoardPositionException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}