namespace TicTacToe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Board board = new();
            while (Evaluate.Winner(board) == Mark.Empty && board.GetMoveCount() != 9)
            {
                Console.WriteLine('\n' + (board.GetTurn() ? "X" : "Y") + " turn");
                Console.WriteLine(board);
                if (int.TryParse(Console.ReadLine(), out int input))  // Get move
                {
                    board.Move(input);
                }
            }
            Console.WriteLine('\n' + board.ToString() + '\n');

            switch (Evaluate.Winner(board))
            {
                case Mark.Empty:
                    Console.WriteLine("The game ends in a draw");
                    break;
                case Mark.X:
                    Console.WriteLine("X wins");
                    break;
                case Mark.O:
                    Console.WriteLine("O wins");
                    break;
            };
        }
    }

    public enum Mark
    {
        Empty,
        X,
        O
    }

    public class Board
    {
        private readonly Mark[] Squares;
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
            if (index < 0 || index > 8 || Squares[index] != Mark.Empty || Evaluate.Winner(this) != Mark.Empty)
            {
                return false; // Invalid move
            }

            Squares[index] = IsXTurn ? Mark.X : Mark.O;
            IsXTurn = !IsXTurn;
            MoveCount++;
            return true;
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

    public static class Evaluate
    {
        public static Mark Winner(Board board)
        {
            // Check columns
            for (int col = 0; col < 3; col++)
            {
                if (board.GetTile(col) != Mark.Empty && board.GetTile(col) == board.GetTile(col + 3) && board.GetTile(col + 3) == board.GetTile(col + 6))
                {
                    return board.GetTile(col);
                }
            }

            // Check rows
            for (int row = 0; row < 3; row++)
            {
                if (board.GetTile(3 * row) != Mark.Empty && board.GetTile(3 * row) == board.GetTile(3 * row + 1) && board.GetTile(3 * row + 1) == board.GetTile(3 * row + 2))
                {
                    return board.GetTile(3 * row);
                }
            }

            Mark center = board.GetTile(4);
            if (center == Mark.Empty)
            {
                return Mark.Empty;
            }

            // Check TL -> BR diagonal
            if (center == board.GetTile(0) && board.GetTile(0) == board.GetTile(8))
            {
                return board.GetTile(0);
            }

            // Check BL -> TR diagonal
            if (center == board.GetTile(2) && board.GetTile(2) == board.GetTile(6))
            {
                return board.GetTile(2);
            }

            return Mark.Empty;
        }
    }
}