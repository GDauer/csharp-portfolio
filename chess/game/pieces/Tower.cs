using chess.board;

namespace chess.game.pieces
{
    internal sealed class Tower : Piece
    {
        private const string Identifier = "♖";
        private const int PieceStep = 1;

        public Tower(ColorPieces colorPieces, Board board) : base(colorPieces, board)
        {
        }

        public override string ToString()
        {
            return Identifier;
        }

        public override bool[,] GetPossibleMovements()
        {
            bool[,] movements = new bool[Board.Lines, Board.Rows];

            // Define the increments to the 4 directions: [line, column]
            // Up, Down, Right, Left
            int[,] directions = new int[,] {
                { -PieceStep, 0 },
                { PieceStep, 0 },
                { 0, PieceStep },
                { 0, -PieceStep }
            };

            // Percorre cada uma das direções
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dLine = directions[i, 0];
                int dColumn = directions[i, 1];

                Position pos = new Position(Position.Line + dLine, Position.Column + dColumn);

                while (Board.IsValidPosition(pos) && CanMove(pos))
                {
                    movements[pos.Line, pos.Column] = true;

                    // If there's a piece of a different color, stop
                    if (Board.GetPiece(pos) != null && Board.GetPiece(pos).ColorPieces != ColorPieces)
                    {
                        break;
                    }

                    // Avança mais um passo na mesma direção
                    pos.SetValues(pos.Line + dLine, pos.Column + dColumn);
                }
            }

            return movements;
        }
    }
}
