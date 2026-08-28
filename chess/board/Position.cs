namespace chess.board
{
    class Position
    {
        public int line {  get; set; }
        public int column { get; set; }

        public Position(int line, int column)
        {
            this.line = line;
            this.column = column;
        }

        public void SetValues(int line, int column)
        {
            this.line = line;
            this.column = column;
        }

        public override string ToString()
        {
            return this.line + ", " + this.column;
        }
    }
}
