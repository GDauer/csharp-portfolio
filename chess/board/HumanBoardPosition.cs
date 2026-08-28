namespace chess.board
{
    class HumanBoardPosition
    {
        public int line { get; set; }
        public char row { get; set; }

        public HumanBoardPosition(char row, int line)
        {
            this.line = line;
            this.row = row;
        }
    }
}
