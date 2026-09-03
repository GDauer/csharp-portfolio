using chess.board;

namespace chess.game.pieces
{
    internal sealed class Queen : Piece
    {
        private const string Identifier = "\u2655";
        private const int PieceStep = 1;

        public Queen(ColorPieces colorPieces, Board board) : base(colorPieces, board)
        {
        }

        public override string ToString()
        {
            return Identifier;
        }

        public override bool[,] GetPossibleMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Rows];

            // Combine all 8 directions (Horizontals, Verticals, and Diagonals)
            int[,] directions = new int[,] {
                { -PieceStep, 0 }, { PieceStep, 0 }, { 0, PieceStep }, { 0, -PieceStep },
                { -PieceStep, -PieceStep }, { -PieceStep, PieceStep },
                { PieceStep, -PieceStep }, { PieceStep, PieceStep }
            };

            // Iterate through each of the 8 directions
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dLine = directions[i, 0];
                int dColumn = directions[i, 1];

                Position pos = new Position(Position.Line + dLine, Position.Column + dColumn);

                // Keep moving along the line until hitting an obstacle or boundary
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
