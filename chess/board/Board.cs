using chess.board.exceptions;

namespace chess.board
{
    class Board
    {
        public int Lines {  get; set; }
        public int Rows { get; set; }
        private Piece[,] Pieces;

        public Board (int lines, int rows)
        {
            this.Lines = lines;
            this.Rows = rows;
            this.Pieces = new Piece[lines, rows];
        }

        public Piece GetPiece(int line, int rows)
        {
            return Pieces[line, rows];
        }

        public Piece GetPiece(Position pos)
        {
            return Pieces[pos.Line, pos.Column];
        }

        public Piece? RemovePiece(Position pos)
        {
            Piece? aux = GetPiece(pos);
            if (aux == null)
            {
                return null;
            }

            aux.Position = null;
            Pieces[pos.Line, pos.Column] = null!;

            return aux;
        }

        public void AddPiece(Piece piece, Position pos)
        {
            if (!IsPositionAvaliable(pos))
            {
                throw new AddPieceToBoardPositionException(
                    "This position is not avaliable (" + pos.Line + ", " + pos.Column + ")" +
                    " for the Piece: " + piece + ", 'cause there's another piece is this position"
                );
            }

            Pieces[pos.Line, pos.Column] = piece;
            piece.Position = pos;
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
                    "Invalid Position Range (" + pos.Line + ", " + pos.Column + ") for the board: " + Lines + ", " + Rows
                );
            }
        }

        public bool IsValidPosition(Position pos)
        {
            if (pos.Line < 0 || pos.Line >= Lines || pos.Column < 0 || pos.Column >= Rows)
            {
                return false;
            }
            
            return true;
        }
    }
}
