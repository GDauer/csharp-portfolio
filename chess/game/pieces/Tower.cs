using chess.board;

namespace chess.game.pieces
{
    internal sealed class Tower : Piece
    {
        private const string identifier = "T";
        private const int pieceStep = 1;

        public Tower(ColorPieces colorPieces, Board board) : base(colorPieces, board)
        {
        }

        public override string ToString()
        {
            return identifier;
        }

        public override bool[,] GetPossibleMovements()
        {
            bool[,] movements = new bool[board.lines, board.rows];

            // Define the increments to the 4 directions: [line, column]
            // Up, Down, Right, Left
            int[,] directions = new int[,] {
                { -pieceStep, 0 },
                { pieceStep, 0 },
                { 0, pieceStep },
                { 0, -pieceStep }
            };

            // Percorre cada uma das direções
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dLine = directions[i, 0];
                int dColumn = directions[i, 1];
                Position pos = new Position(position.line + dLine, position.column + dColumn);

                while (board.IsValidPosition(pos) && CanMove(pos))
                {
                    movements[pos.line, pos.column] = true;

                    // If there's a piece of a different color, stop
                    if (board.GetPiece(pos) != null && board.GetPiece(pos).colorPieces != colorPieces)
                    {
                        break;
                    }

                    // Avança mais um passo na mesma direção
                    pos.SetValues(pos.line + dLine, pos.column + dColumn);
                }
            }

            return movements;
        }

        private bool CanMove(Position pos)
        {
            Piece piece = board.GetPiece(pos);

            return piece == null || piece.colorPieces != colorPieces;
        }
    }
}
