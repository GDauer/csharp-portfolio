using chess.board;
using chess.board.exceptions;
using chess.game.pieces;

namespace chess.game
{
    class GameChess
    {
        public Board board { get; private set; }
        private int turn;
        private ColorPieces actualPlayer;
        public bool isCheckMate { get; private set; }
        private const int chessBoardSize = 8;

        public GameChess()
        {
            board = new Board(chessBoardSize, chessBoardSize);
            turn = 1;
            actualPlayer = ColorPieces.White;
            isCheckMate = false;
            setupPiecesToChessBoard();
        }

        public void doMov(Position from, Position to)
        {
            Piece? piece = board.RemovePiece(from) ?? throw new AddPieceToBoardPositionException("There's no piece here to be moved");
            piece.incrementMovQty();

            Piece? capturedPiece = board.RemovePiece(to);

            board.AddPiece(piece, to);
        }

        private void setupPiecesToChessBoard()
        {
            board.AddPiece(new Tower(ColorPieces.White, board), new PositionChess('c', 1).ToPosition());
        }
    }
}
