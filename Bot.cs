namespace TicTacToe
{
    class Bot : IPlayer
    {
        readonly bool IsXPlayer;
        readonly Board board;
        public Bot(bool IsXPlayer, Board board)
        {
            this.IsXPlayer = IsXPlayer;
            this.board = board;
        }
        public int Move()
        {
            int bestMove = -1;
            int bestValue = IsXPlayer ? int.MinValue : int.MaxValue;  // X will try to maximize score, O will try to minimize
            for (int i = 0; i < 9; i++)
            {
                int currentValue;
                if (!board.Move(i))
                {
                    continue;
                }
                currentValue = Minimax(!IsXPlayer);  // Search the opponent's moves from each position
                if ((IsXPlayer && currentValue > bestValue) || (!IsXPlayer && currentValue < bestValue))
                {
                    bestMove = i;
                    bestValue = currentValue;
                }
                board.UndoMove(i);  // Backtrack
            }
            return bestMove;  // Return index of best move
        }
        public int Minimax(bool IsXTurn)
        {
            int winner = board.Winner();
            if (board.GetMoveCount() == 9 || winner != 0)
            {
                return winner;  // Positive score if X wins, negative if O wins, and 0 if the game ends in a draw
            }

            int bestValue = IsXTurn ? int.MinValue : int.MaxValue;
            for (int i = 0; i < 9; i++)
            {
                int currentValue;
                if (!board.Move(i))
                {
                    continue;
                }
                currentValue = Minimax(!IsXTurn);
                if ((IsXTurn && currentValue > bestValue) || (!IsXTurn && currentValue < bestValue))
                {
                    bestValue = currentValue;
                }
                board.UndoMove(i);
            }
            return bestValue;
        }
    }
}