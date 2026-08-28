using chess.board;

namespace chess.game.pieces
{
    internal sealed class King : Piece
    {
        private const string identifier = "K";

        public King(ColorPieces colorPieces, Board board) : base(colorPieces, board)
        {
        }

        public override string ToString()
        {
            return identifier;
        }
    }
}
