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

                while (!chessGame.IsCheckMate)
                {
                    try
                    {
                        Console.Clear();
                        Screen.PrintGamePlay(chessGame);
                        Console.Write("Type the origin position: ");

                        PositionChess from = new PositionChess(Screen.ScreenCordinators());
                        Position coordinatorsFrom = from.ToPosition();

                        //Show piece's possible moves in the screen
                        chessGame.ValidateOriginPosition(coordinatorsFrom);
                        Console.Clear();
                        bool[,] possiblePositions = chessGame.Board.GetPiece(coordinatorsFrom).GetPossibleMovements();

                        Screen.PrintBoard(chessGame.Board, possiblePositions);
                        //Give some space
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.Write("Type the destination position: ");

                        PositionChess to = new PositionChess(Screen.ScreenCordinators());
                        Position coordinatorsTo = to.ToPosition();
                        chessGame.ValidateDestinationPosition(coordinatorsFrom, coordinatorsTo);

                        chessGame.Play(coordinatorsFrom, coordinatorsTo);
                    } catch (AddPieceToBoardPositionException e)
                    {
                        Console.Clear();
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Press Any Keys to Play again!");
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}
