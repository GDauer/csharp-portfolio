using chess.board;
using chess.board.exceptions;
using chess.game.pieces;

namespace chess.game
{
    class GameChess
    {
        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public ColorPieces ActualPlayer {  get; private set; }
        public bool IsCheckMate { get; private set; }
        private const int ChessBoardSize = 8;
        private HashSet<Piece> Pieces;
        private HashSet<Piece> CapturedPieces;

        public GameChess()
        {
            Board = new Board(ChessBoardSize, ChessBoardSize);
            Turn = 1;
            ActualPlayer = ColorPieces.White;
            IsCheckMate = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            SetupPiecesToChessBoard();
        }

        public void Play(Position start, Position end)
        {
            DoMov(start, end);
            Turn++;

            ChangePlayer();
        }

        public void DoMov(Position from, Position to)
        {
            Piece? piece = Board.RemovePiece(from) ?? throw new AddPieceToBoardPositionException("There's no piece here to be moved");
            piece.IncrementMovQty();

            Piece? capturedPiece = Board.RemovePiece(to);
            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }
            Board.AddPiece(piece, to);
        }

        private void ChangePlayer()
        {
            if (ActualPlayer == ColorPieces.White)
            {
                ActualPlayer = ColorPieces.Black;
                return;
            }

            ActualPlayer = ColorPieces.White;
        }

        public void ValidateOriginPosition(Position origin)
        {
            if (Board.GetPiece(origin)  == null)
            {
                throw new AddPieceToBoardPositionException("Origin Position can not be void. Choose an origin with a piece on it.");
            }

            if (ActualPlayer != Board.GetPiece(origin).ColorPieces)
            {
                throw new AddPieceToBoardPositionException("Can not move a piece from a different color.");
            }

            if (!Board.GetPiece(origin).IsAnyMovementsPossible())
            {
                throw new AddPieceToBoardPositionException("There's no possible movement for this piece.");
            }
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.GetPiece(origin).CanMoveToDestination(destination))
            {
                throw new AddPieceToBoardPositionException("Invalid destination, this piece can not go to that position.");
            }
        }

        private void SetupPiecesToChessBoard()
        {
            AddPieceToTheBoard('c', 4, new Tower(ColorPieces.White, Board));
            AddPieceToTheBoard('c', 7, new King(ColorPieces.Black, Board));
            AddPieceToTheBoard('c', 2, new Tower(ColorPieces.Black, Board));
            AddPieceToTheBoard('a', 4, new King(ColorPieces.White, Board));
        }

        public void AddPieceToTheBoard(char column, int line, Piece piece)
        {
            Board.AddPiece(piece, new PositionChess(column, line).ToPosition());
            Pieces.Add(piece);
        }

        public HashSet<Piece> GetCapturedPieces(ColorPieces colorPieces)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in CapturedPieces)
            {
                if (piece.ColorPieces == colorPieces)
                {
                    aux.Add(piece);
                }
            }
            return aux;
        }

        public HashSet<Piece> GetPiecesInGame(ColorPieces colorPieces)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in Pieces)
            {
                if (piece.ColorPieces == colorPieces)
                {
                    aux.Add(piece);
                }
            }

            aux.ExceptWith(GetCapturedPieces(colorPieces));
            return aux;
        }
    }
}
