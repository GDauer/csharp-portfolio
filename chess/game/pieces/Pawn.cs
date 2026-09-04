using chess.board;

namespace chess.game.pieces
{
    internal sealed class Pawn : Piece
    {
        private const string Identifier = "\u2659";
        private const int PieceStep = 1;

        private GameChess GameChess;

        public Pawn(ColorPieces colorPieces, Board board, GameChess game) : base(colorPieces, board)
        {
            GameChess = game;
        }

        public override string ToString()
        {
            return Identifier;
        }

        public override bool[,] GetPossibleMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Rows];

            // Determine the vertical direction based on color (White moves up -1, Black moves down +1)
            int direction = (ColorPieces == ColorPieces.White) ? - PieceStep : PieceStep;

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
            //Special Mov En Passant
            if (Position.Line == 3 && ColorPieces == ColorPieces.White)
            {
                
                Position left = new Position(Position.Line, Position.Column - PieceStep);
                if  (Board.IsValidPosition(left) && CanMove(left) && Board.GetPiece(left) == GameChess.EnPassantVuln)
                {
                    movements[left.Line - 1, left.Column] = true;
                }

                Position right = new Position(Position.Line, Position.Column + PieceStep);
                if (Board.IsValidPosition(right) && CanMove(right) && Board.GetPiece(right) == GameChess.EnPassantVuln)
                {
                    movements[right.Line - 1, right.Column] = true;
                }
            }

            //Special Mov En Passant
            if (Position.Line == 4 && ColorPieces == ColorPieces.Black)
            {

                Position left = new Position(Position.Line, Position.Column + PieceStep);
                if (Board.IsValidPosition(left) && CanMove(left) && Board.GetPiece(left) == GameChess.EnPassantVuln)
                {
                    movements[left.Line + 1, left.Column] = true;
                }

                Position right = new Position(Position.Line, Position.Column - PieceStep);
                if (Board.IsValidPosition(right) && CanMove(right) && Board.GetPiece(right) == GameChess.EnPassantVuln)
                {
                    movements[right.Line + 1, right.Column] = true;
                }
            }

            return movements;
        }
    }
}
