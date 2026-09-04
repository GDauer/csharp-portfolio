using chess.board;

namespace chess.game.pieces
{
    internal sealed class Bishop : Piece
    {
        private const string Identifier = "\u265D";
        private const int PieceStep = 1;

        public Bishop(ColorPieces colorPieces, Board board) : base(colorPieces, board)
        {
        }

        public override string ToString()
        {
            return Identifier;
        }

        public override bool[,] GetPossibleMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Rows];

            // Define the increments for the 4 diagonal directions
            int[,] directions = new int[,] {
                { -PieceStep, -PieceStep }, // Top-Left
                { -PieceStep, PieceStep },  // Top-Right
                { PieceStep, -PieceStep },  // Bottom-Left
                { PieceStep, PieceStep }    // Bottom-Right
            };

            // Iterate through each of the 4 directions
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dLine = directions[i, 0];
                int dColumn = directions[i, 1];

                Position pos = new Position(Position.Line + dLine, Position.Column + dColumn);

                // Keep moving along the diagonal until hitting an obstacle or boundary
                while (Board.IsValidPosition(pos) && CanMove(pos))
                {
                    movements[pos.Line, pos.Column] = true;

                    // If there's an enemy piece, it can be captured, but the path is blocked
                    if (Board.GetPiece(pos) != null && Board.GetPiece(pos).ColorPieces != ColorPieces)
                    {
                        break;
                    }

                    // Advance one more step in the same direction
                    pos.SetValues(pos.Line + dLine, pos.Column + dColumn);
                }
            }

            return movements;
        }
    }
}
