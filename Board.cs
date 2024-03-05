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
        private readonly Mark[] _squares;  // Represents the board
        private bool _isXTurn;
        private int _moveCount;

        public Board()
        {
            _squares = new Mark[9] { Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty, Mark.Empty };
            _isXTurn = true;
            _moveCount = 0;
        }

        public bool Move(int index)
        {
            if (index < 0 || index > 8 || _squares[index] != Mark.Empty)
            {
                return false; // Invalid move
            }

            _squares[index] = _isXTurn ? Mark.X : Mark.O;
            _isXTurn = !_isXTurn;
            _moveCount++;
            return true;
        }

        public bool UndoMove(int index)
        {
            if (index < 0 || index > 8 || _squares[index] == Mark.Empty)
            {
                return false; // Invalid move
            }
            _squares[index] = Mark.Empty;
            _isXTurn = !_isXTurn;
            _moveCount--;
            return true;
        }

        public int Winner()
        {
            // Check columns
            for (int col = 0; col < 3; col++)
            {
                if (_squares[col] != Mark.Empty && _squares[col] == _squares[col + 3] && _squares[col + 3] == _squares[col + 6])
                {
                    return _squares[col] == Mark.X ? 10 - _moveCount : _moveCount - 10;
                }
            }

            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (_squares[3 * row] != Mark.Empty && _squares[3 * row] == _squares[3 * row + 1] && _squares[3 * row + 1] == _squares[3 * row + 2])
                {
                    return _squares[3 * row] == Mark.X ? 10 - _moveCount : _moveCount - 10;
                }
            }

            Mark center = _squares[4];
            if (center == Mark.Empty)
            {
                return 0;
            }

            // Check TL -> BR diagonal
            if (center == _squares[0] && _squares[0] == _squares[8])
            {
                return _squares[0] == Mark.X ? 10 - _moveCount : _moveCount - 10;
            }

            // Check BL -> TR diagonal
            if (center == _squares[2] && _squares[2] == _squares[6])
            {
                return _squares[2] == Mark.X ? 10 - _moveCount : _moveCount - 10;
            }

            return 0;
        }

        public bool IsXTurn => _isXTurn;
        public int MoveCount => _moveCount;

        public Mark this[int index]
        {
            get
            {
                if (index < 0 || index > 8) throw new IndexOutOfRangeException("Index must be within the range 0 to 8");
                return _squares[index];
            }
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
                switch (_squares[i])
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