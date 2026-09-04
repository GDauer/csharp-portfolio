using chess.board;

namespace chess.game.pieces
{
    internal sealed class Knight : Piece
    {
        private const string Identifier = "\u265E";
        private const int PieceStep = 1;

        public Knight(ColorPieces colorPieces, Board board) : base(colorPieces, board)
        {
        }

        public override string ToString()
        {
            return Identifier;
        }

        public override bool[,] GetPossibleMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Rows];

            // Define the 8 possible "L-shaped" target offsets for the Knight
            int[,] directions = new int[,] {
                { -2, -1 }, { -2, 1 }, // 2 up, 1 sideways
                { 2, -1 }, { 2, 1 },   // 2 down, 1 sideways
                { -1, -2 }, { 1, -2 }, // 2 left, 1 up/down
                { -1, 2 }, { 1, 2 }    // 2 right, 1 up/down
            };

            // Iterate through each of the 8 offsets
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                // Multiply by PieceStep in case your board uses dynamic scaling
                int dLine = directions[i, 0] * PieceStep;
                int dColumn = directions[i, 1] * PieceStep;

                Position pos = new Position(Position.Line + dLine, Position.Column + dColumn);

                // The Knight leaps directly to the target square
                if (Board.IsValidPosition(pos) && CanMove(pos))
                {
                    movements[pos.Line, pos.Column] = true;
                }
            }

            return movements;
        }
    }
}
