using chess.board;
using chess.board.exceptions;
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
                board.AddPiece(new Tower(ColorPieces.Black, board), new Position(0, 0));
                board.AddPiece(new King(ColorPieces.Black, board), new Position(0, 2));
                board.AddPiece(new King(ColorPieces.White, board), new Position(4, 5));

                Screen.PrintBoard(board);
            }
            catch (AddPieceToBoardPositionException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}