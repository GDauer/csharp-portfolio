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
            Position pos = new Position(0, 0);

            //Up
            pos.SetValues(pos.line - pieceStep, pos.column);
            if (board.IsValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }

            //upright diagonal
            pos.SetValues(pos.line - pieceStep, pos.column + pieceStep);
            if (board.IsValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }

            //Right
            pos.SetValues(pos.line, pos.column + pieceStep);
            if (board.IsValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }

            //downright diagonal
            pos.SetValues(pos.line + pieceStep, pos.column + pieceStep);
            if (board.IsValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }

            //down
            pos.SetValues(pos.line + pieceStep, pos.column);
            if (board.IsValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }

            //downleft diagonal
            pos.SetValues(pos.line + pieceStep, pos.column - pieceStep);
            if (board.IsValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }

            //left
            pos.SetValues(pos.line, pos.column - pieceStep);
            if (board.IsValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
            }

            //leftup diagonal
            pos.SetValues(pos.line - pieceStep, pos.column - pieceStep);
            if (board.IsValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;
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
