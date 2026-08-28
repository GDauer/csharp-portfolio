namespace chess.board
{
    class Piece
    {
        public Position? position { get; set; }
        public ColorPieces colorPieces { get; protected set; }
        public int qtyMovDone { get; protected set; }
        public Board board { get; protected set; }

        public Piece (ColorPieces colorPieces, Board board)
        {
            this.position = null;
            this.colorPieces = colorPieces;
            this.board = board;
            this.qtyMovDone = 0;
        }

        public void incrementMovQty()
        {
            qtyMovDone++;
        }
    }
}
