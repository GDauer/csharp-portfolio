using chess.board;

namespace chess.game.pieces
{
    internal sealed class King : Piece
    {
        private const string identifier = "K";
        private const int pieceStep = 1;

        public King(ColorPieces colorPieces, Board board) : base(colorPieces, board)
        {
        }

        public override string ToString()
        {
            return identifier;
        }

        public override bool[,] GetPossibleMovements()
        {
            bool[,] movements = new bool[board.lines, board.rows];

            // Define the increments for all 8 possible directions (Horizontals, Verticals, and Diagonals)
            int[,] directions = new int[,] {
                { -pieceStep, 0 },  // Up
                { pieceStep, 0 },   // Down
                { 0, pieceStep },   // Right
                { 0, -pieceStep },  // Left
                { -pieceStep, -pieceStep }, // Top-Left Diagonal
                { -pieceStep, pieceStep },  // Top-Right Diagonal
                { pieceStep, -pieceStep },  // Bottom-Left Diagonal
                { pieceStep, pieceStep }    // Bottom-Right Diagonal
            };

            // Iterate through each of the 8 directions
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dLine = directions[i, 0];
                int dColumn = directions[i, 1];

                // The King only checks the immediate neighboring square
                Position pos = new Position(position.line + dLine, position.column + dColumn);

                // If the position is valid and the King can move there, mark it as true
                if (board.IsValidPosition(pos) && CanMove(pos))
                {
                    movements[pos.line, pos.column] = true;
                }
            }

            return movements;
        }

        private bool CanMove(Position pos)
        {
            Piece piece = board.GetPiece(pos);

            return piece == null || piece.colorPieces != colorPieces;
        }
    }
}
