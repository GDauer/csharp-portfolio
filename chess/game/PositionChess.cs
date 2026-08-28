using chess.board;

namespace chess.game
{
    class PositionChess
    {
        public int line {  get; set; }
        public char row { get; set; }
        private const int chessMaxLine = 8;
        private const char chessFirstRow = 'a';

        public PositionChess (char row, int line)
        {
            this.line = line;
            this.row = row;
        }

        public Position ToPosition()
        {
            //doing row - 'a' forces the C# to return an int in the alphabet position.
            return new Position(chessMaxLine - line, row - chessFirstRow);
        }

        public override string ToString()
        {
            return "" + row + line;
        }
    }
}
