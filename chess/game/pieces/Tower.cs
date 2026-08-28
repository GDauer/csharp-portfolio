using chess.board;

namespace chess.game.pieces
{
    internal sealed class Tower : Piece
    {
        private const string identifier = "T";
        private const int pieceStep = 1;

        public Tower(ColorPieces colorPieces, Board board) : base(colorPieces, board)
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
            while (board.IsValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;

                if (board.GetPiece(pos) != null && board.GetPiece(pos).colorPieces != colorPieces ) {
                    break;
                }
                pos.line = pos.line - pieceStep;
            }

            //Down
            pos.SetValues(pos.line + pieceStep, pos.column);
            while (board.IsValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;

                if (board.GetPiece(pos) != null && board.GetPiece(pos).colorPieces != colorPieces)
                {
                    break;
                }
                pos.line = pos.line + pieceStep;
            }

            //Right
            pos.SetValues(pos.line, pos.column + pieceStep);
            while (board.IsValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;

                if (board.GetPiece(pos) != null && board.GetPiece(pos).colorPieces != colorPieces)
                {
                    break;
                }
                pos.column = pos.column + pieceStep;
            }

            //Left
            pos.SetValues(pos.line, pos.column - pieceStep);
            while (board.IsValidPosition(pos) && CanMove(pos))
            {
                movements[pos.line, pos.column] = true;

                if (board.GetPiece(pos) != null && board.GetPiece(pos).colorPieces != colorPieces)
                {
                    break;
                }
                pos.column = pos.column - pieceStep;
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
