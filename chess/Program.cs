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
                GameChess chessGame = new GameChess();

                while (!chessGame.isCheckMate)
                {
                    Console.Clear();
                    Screen.PrintBoard(chessGame.board);
                    //Give some space
                    Console.WriteLine();
                    Console.WriteLine();

                    Console.Write("Type the origin position: ");
                    PositionChess from = new PositionChess(Screen.ScreenCordinators());
                    Position coordinatorsFrom = from.ToPosition();

                    Console.Write("Type the destination position: ");
                    PositionChess to = new PositionChess(Screen.ScreenCordinators());
                    Position coordinatorsTo = to.ToPosition();

                    chessGame.doMov(coordinatorsFrom, coordinatorsTo);
                }
            }
            catch (AddPieceToBoardPositionException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}