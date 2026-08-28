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
    }
}
