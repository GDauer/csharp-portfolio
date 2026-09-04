using chess.board;

namespace chess.game.pieces
{
    internal sealed class King : Piece
    {
        private GameChess GameChess;
        private const string Identifier = "\u2654";
        public const int PieceStep = 1;
        public const int SmallRoqueStepDistance = 3;
        public const int BiggerRoqueStepDistance = 4;

        public King(ColorPieces colorPieces, Board board, GameChess gameChess) : base(colorPieces, board)
        {
            GameChess = gameChess;
        }

        public override string ToString()
        {
            return Identifier;
        }

        public override bool[,] GetPossibleMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Rows];

            // Define the increments for all 8 possible directions (Horizontals, Verticals, and Diagonals)
            int[,] directions = new int[,] {
                { -PieceStep, 0 },  // Up
                { PieceStep, 0 },   // Down
                { 0, PieceStep },   // Right
                { 0, -PieceStep },  // Left
                { -PieceStep, -PieceStep }, // Top-Left Diagonal
                { -PieceStep, PieceStep },  // Top-Right Diagonal
                { PieceStep, -PieceStep },  // Bottom-Left Diagonal
                { PieceStep, PieceStep }    // Bottom-Right Diagonal
            };

            // Iterate through each of the 8 directions
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dLine = directions[i, 0];
                int dColumn = directions[i, 1];

                // The King only checks the immediate neighboring square
                Position pos = new Position(Position.Line + dLine, Position.Column + dColumn);

                // If the position is valid and the King can move there, mark it as true
                if (Board.IsValidPosition(pos) && CanMove(pos))
                {
                    movements[pos.Line, pos.Column] = true;
                }
            }

            //Roqué
            if (QtyMovDone == 0 && !GameChess.IsCheck)
            {
                //Small roqué
                Position TowerPos1 = new Position(Position.Line, Position.Column + SmallRoqueStepDistance);

                if (IsTowerEnabledToRoquePlay(TowerPos1))
                {
                    Position nextHouse = new Position(Position.Line, Position.Column + PieceStep);
                    Position secondHouse = new Position(Position.Line, Position.Column + PieceStep + PieceStep);

                    if (Board.GetPiece(nextHouse) == null && Board.GetPiece(secondHouse) == null)
                    {
                        Console.WriteLine(Position.Line);
                        Console.WriteLine(Position.Column + PieceStep + PieceStep);
                        movements[Position.Line, Position.Column + PieceStep + PieceStep] = true;
                    }
                }

                //Big roqué
                Position TowerPos2 = new Position(Position.Line, Position.Column - BiggerRoqueStepDistance);

                if (IsTowerEnabledToRoquePlay(TowerPos2))
                {
                    Position nextHouse = new Position(Position.Line, Position.Column - PieceStep);
                    Position secondHouse = new Position(Position.Line, Position.Column - PieceStep - PieceStep);
                    Position thirdHouse = new Position(Position.Line, Position.Column - PieceStep - PieceStep - PieceStep);

                    if (Board.GetPiece(nextHouse) == null && Board.GetPiece(secondHouse) == null && Board.GetPiece(thirdHouse) == null)
                    {
                        movements[Position.Line, Position.Column - PieceStep - PieceStep] = true;
                    }
                }
            }

            return movements;
        }

        private bool IsTowerEnabledToRoquePlay(Position position)
        {
            Piece piece = Board.GetPiece(position);

            return piece != null && piece is Tower && piece.ColorPieces == ColorPieces && piece.QtyMovDone == 0;
        }
    }
}
