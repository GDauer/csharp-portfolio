using chess.board;

namespace chess.game.pieces
{
    internal sealed class Tower : Piece
    {
        private const string identifier = "T";

        public Tower(ColorPieces colorPieces, Board board) : base(colorPieces, board)
        {
        }

        public override string ToString()
        {
            return identifier;
        }
    }
}
