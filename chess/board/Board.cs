using chess.board.exceptions;

namespace chess.board
{
    class Board
    {
        public int lines {  get; set; }
        public int rows { get; set; }
        private Piece[,] pieces;

        public Board (int lines, int rows)
        {
            this.lines = lines;
            this.rows = rows;
            this.pieces = new Piece[lines, rows];
        }

        public Piece GetPiece(int line, int rows)
        {
            return pieces[line, rows];
        }

        public Piece GetPiece(Position pos)
        {
            return pieces[pos.line, pos.column];
        }

        public Piece? RemovePiece(Position pos)
        {
            Piece? aux = GetPiece(pos);
            if (aux == null)
            {
                return null;
            }

            aux.position = null;
            pieces[pos.line, pos.column] = null!;

            return aux;
        }

        public void AddPiece(Piece piece, Position pos)
        {
            if (!IsPositionAvaliable(pos))
            {
                throw new AddPieceToBoardPositionException(
                    "This position is not avaliable (" + pos.line + ", " + pos.column + ")" +
                    " for the Piece: " + piece + ", 'cause there's another piece is this position"
                );
            }

            pieces[pos.line, pos.column] = piece;
            piece.position = pos;
        }

        public bool IsPositionAvaliable(Position pos)
        {
            ValidatePosition(pos);

            return GetPiece(pos) == null;
        }

        public void ValidatePosition(Position pos)
        {
            if (!IsValidPosition(pos))
            {
                throw new AddPieceToBoardPositionException(
                    "Invalid Position Range (" + pos.line + ", " + pos.column + ") for the board: " + lines + ", " + rows
                );
            }
        }

        public bool IsValidPosition(Position pos)
        {
            if (pos.line < 0 || pos.line >= lines || pos.column < 0 || pos.column >= rows)
            {
                return false;
            }
            
            return true;
        }
    }
}
