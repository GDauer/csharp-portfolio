using chess.board;

namespace chess.game.pieces
{
    internal sealed class Pawn : Piece
    {
        private const string Identifier = "\u2659";
        private const int PieceStep = 1;

        public Pawn(ColorPieces colorPieces, Board board) : base(colorPieces, board)
        {
        }

        public override string ToString()
        {
            return Identifier;
        }

        public override bool[,] GetPossibleMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Rows];

            // Determine the vertical direction based on color (White moves up -1, Black moves down +1)
            int direction = (ColorPieces == ColorPieces.White) ? -PieceStep : PieceStep;

            // 1. Standard Move: 1 square forward (only if the square is empty)
            Position pos = new Position(Position.Line + direction, Position.Column);
            if (Board.IsValidPosition(pos) && Board.GetPiece(pos) == null)
            {
                movements[pos.Line, pos.Column] = true;

                // 2. First Move: 2 squares forward (only if both the 1st and 2nd squares are empty)
                Position pos2 = new Position(Position.Line + (2 * direction), Position.Column);
                if (QtyMovDone == 0 && Board.IsValidPosition(pos2) && Board.GetPiece(pos2) == null)
                {
                    movements[pos2.Line, pos2.Column] = true;
                }
            }

            // 3. Diagonal Captures (Left and Right)
            int[] diagonalColumns = new int[] { Position.Column - PieceStep, Position.Column + PieceStep };
            foreach (int col in diagonalColumns)
            {
                Position diagPos = new Position(Position.Line + direction, col);
                if (Board.IsValidPosition(diagPos))
                {
                    Piece enemy = Board.GetPiece(diagPos);
                    // Pawns can only move diagonally if there's an enemy piece to capture
                    if (enemy != null && enemy.ColorPieces != ColorPieces)
                    {
                        movements[diagPos.Line, diagPos.Column] = true;
                    }
                }
            }

            return movements;
        }
    }
}
