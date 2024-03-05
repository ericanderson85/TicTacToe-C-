namespace TicTacToe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Board board = new();
            IPlayer XPlayer = new Human();
            IPlayer OPlayer = new Bot(false, board);
            while (board.GetMoveCount() != 9 && board.Winner() == 0)
            {
                Console.WriteLine('\n' + (board.GetTurn() ? "X" : "Y") + " turn");
                Console.WriteLine(board);
                if (board.GetTurn())
                {
                    board.Move(XPlayer.Move());
                }
                else
                {
                    board.Move(OPlayer.Move());
                }
            }
            Console.WriteLine("\n\n" + board.ToString() + "\n");
            if (board.Winner() == 0)
            {
                Console.WriteLine("The game ends in a draw\n");
            }
            else if (board.Winner() > 0)
            {
                Console.WriteLine("X wins");
            }
            else
            {
                Console.WriteLine("O wins");
            }
        }
    }
}