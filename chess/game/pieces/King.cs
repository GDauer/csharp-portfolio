using chess.board;

namespace chess.game.pieces
{
    internal sealed class King : Piece
    {
        private const string Identifier = "K";
        private const int PieceStep = 1;

        public King(ColorPieces colorPieces, Board board) : base(colorPieces, board)
        {
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

            return movements;
        }
    }
}
