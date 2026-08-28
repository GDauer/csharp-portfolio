using chess.board;

namespace chess.game
{
    class PositionChess : HumanBoardPosition
    {
        private const int chessMaxLine = 8;
        private const char chessFirstRow = 'a';

        public PositionChess(char row, int line) : base(row, line)
        {
        }

        // Construtor que aceita a classe mãe e repassa os dados para o construtor base
        public PositionChess(HumanBoardPosition mother) : base(mother.row, mother.line)
        {
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
