namespace TicTacToe
{
    public enum Mark
    {
        Empty,
        X,
        O
    }
    public class Board
    {
        private readonly Mark[] Squares;  // Represents the board
        private bool IsXTurn;
        private int MoveCount;

        public Board()
        {
            Squares = new Mark[9] { Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty };
            IsXTurn = true;
            MoveCount = 0;
        }

        public bool Move(int index)
        {
            if (index < 0 || index > 8 || Squares[index] != Mark.Empty)
            {
                return false; // Invalid move
            }

            Squares[index] = IsXTurn ? Mark.X : Mark.O;
            IsXTurn = !IsXTurn;
            MoveCount++;
            return true;
        }

        public bool UndoMove(int index)
        {
            if (index < 0 || index > 8 || Squares[index] == Mark.Empty)
            {
                return false; // Invalid move
            }
            Squares[index] = Mark.Empty;
            IsXTurn = !IsXTurn;
            MoveCount--;
            return true;
        }

        public int Winner()
        {
            // Check columns
            for (int col = 0; col < 3; col++)
            {
                if (Squares[col] != Mark.Empty && Squares[col] == Squares[col + 3] && Squares[col + 3] == Squares[col + 6])
                {
                    return Squares[col] == Mark.X ? 10 - MoveCount : MoveCount - 10;
                }
            }

            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (Squares[3 * row] != Mark.Empty && Squares[3 * row] == Squares[3 * row + 1] && Squares[3 * row + 1] == Squares[3 * row + 2])
                {
                    return Squares[3 * row] == Mark.X ? 10 - MoveCount : MoveCount - 10;
                }
            }

            Mark center = Squares[4];
            if (center == Mark.Empty)
            {
                return 0;
            }

            // Check TL -> BR diagonal
            if (center == Squares[0] && Squares[0] == Squares[8])
            {
                return Squares[0] == Mark.X ? 10 - MoveCount : MoveCount - 10;
            }

            // Check BL -> TR diagonal
            if (center == Squares[2] && Squares[2] == Squares[6])
            {
                return Squares[2] == Mark.X ? 10 - MoveCount : MoveCount - 10;
            }

            return 0;
        }

        public bool GetTurn()
        {
            return IsXTurn;
        }

        public Mark GetTile(int index)
        {
            if (index < 0 || index > 8) throw new IndexOutOfRangeException();

            return Squares[index];
        }

        public int GetMoveCount()
        {
            return MoveCount;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < 9; i++)
            {
                if (i == 3 || i == 6)
                {
                    str += "\n\n";
                }
                switch (Squares[i])
                {
                    case Mark.Empty:
                        str += " ~ ";
                        break;
                    case Mark.X:
                        str += " x ";
                        break;
                    case Mark.O:
                        str += " o ";
                        break;
                }
            }
            return str;
        }

    }
}