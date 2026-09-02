namespace chess.board
{
    class Position
    {
        public int Line {  get; set; }
        public int Column { get; set; }

        public Position(int line, int column)
        {
            this.Line = line;
            this.Column = column;
        }

        public void SetValues(int line, int column)
        {
            this.Line = line;
            this.Column = column;
        }

        public override string ToString()
        {
            return this.Line + ", " + this.Column;
        }
    }
}
