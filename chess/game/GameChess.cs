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
        public bool IsCheck {  get; private set; }
        private const int ChessBoardSize = 8;
        private HashSet<Piece> Pieces;
        private HashSet<Piece> CapturedPieces;

        public GameChess()
        {
            Board = new Board(ChessBoardSize, ChessBoardSize);
            Turn = 1;
            ActualPlayer = ColorPieces.White;
            IsCheckMate = false;
            IsCheck = false;
            Pieces = new HashSet<Piece>();
            CapturedPieces = new HashSet<Piece>();
            SetupPiecesToChessBoard();
        }

        public void Play(Position start, Position end)
        {
            Piece? capturedPiece = DoMov(start, end);

            // It's not allowed to put yourself in check
            if (IsKingInCheck(ActualPlayer))
            {
                UndoMovement(start, end, capturedPiece);
                throw new AddPieceToBoardPositionException("You can not put or keep yourself in Check.");
            }

            IsCheck = IsKingInCheck(GetEnemyPiece(ActualPlayer));
            Turn++;

            ChangePlayer();
        }

        public void UndoMovement(Position origin, Position destination, Piece? capturedPiece)
        {
            Piece piece = Board.RemovePiece(destination)
                ?? throw new Exception("Something went wrong while undoing movement. Try restarting the game!");
            piece.DecrementMovQty();

            if (capturedPiece != null)
            {
                Board.AddPiece(capturedPiece, destination);
                CapturedPieces.Remove(capturedPiece);
            }

            Board.AddPiece(piece, origin);
        }

        public Piece? DoMov(Position from, Position to)
        {
            Piece? piece = Board.RemovePiece(from) ?? throw new AddPieceToBoardPositionException("There's no piece here to be moved");
            piece.IncrementMovQty();

            Piece? capturedPiece = Board.RemovePiece(to);
            if (capturedPiece != null)
            {
                CapturedPieces.Add(capturedPiece);
            }
            Board.AddPiece(piece, to);

            return capturedPiece;
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

        public bool IsKingInCheck(ColorPieces colorPieces)
        {
            Piece king = GetKing(colorPieces) 
                ?? throw new ApplicationException($"There's no King from this color in the board: {colorPieces}");

            Position kingPosition = king.Position
                ?? throw new ApplicationException("Invalid King Position on Check Verify");

            foreach (Piece piece in GetPiecesInGame(GetEnemyPiece(colorPieces)))
            {
                bool[,] possibleMovements = piece.GetPossibleMovements();

                if (possibleMovements.Length > 0 && possibleMovements[kingPosition.Line, kingPosition.Column])
                {
                    return true;
                }
            }

            return false;
        }

        private Piece? GetKing(ColorPieces colorPieces)
        {
            foreach (Piece piece in GetPiecesInGame(colorPieces))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        private ColorPieces GetEnemyPiece(ColorPieces colorPieces)
        {
            if (colorPieces == ColorPieces.White)
            {
                return ColorPieces.Black;
            }

            return ColorPieces.White;
        }
    }
}
