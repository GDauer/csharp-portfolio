namespace chess.board
{
    abstract class Piece
    {
        public Position? Position { get; set; }
        public ColorPieces ColorPieces { get; protected set; }
        public int QtyMovDone { get; protected set; }
        public Board Board { get; protected set; }

        public Piece (ColorPieces colorPieces, Board board)
        {
            this.Position = null;
            this.ColorPieces = colorPieces;
            this.Board = board;
            this.QtyMovDone = 0;
        }

        public void IncrementMovQty()
        {
            QtyMovDone++;
        }

        public bool IsAnyMovementsPossible()
        {
            bool[,] possibleMoviments = GetPossibleMovements();

            for (int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Rows; j++)
                {
                    if (possibleMoviments[i, j] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public abstract bool[,] GetPossibleMovements();
        public bool CanMove(Position pos)
        {
            Piece piece = Board.GetPiece(pos);

            return piece == null || piece.ColorPieces != ColorPieces;
        }

        public bool CanMoveToDestination(Position pos)
        {
            bool[,] movements = GetPossibleMovements();
            if (
                !CanMove(pos) &&
                pos.Line < 0 || pos.Line >= movements.GetLength(0) ||
                pos.Column < 0 || pos.Column >= movements.GetLength(1)
            )
            {
                return false;
            }

            // Returns the value inside the matrix (will be true if the move is allowed)
            return movements[pos.Line, pos.Column];
        }
    }
}
