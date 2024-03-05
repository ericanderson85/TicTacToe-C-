namespace TicTacToe
{
    class Human : IPlayer
    {
        public int Move()
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    return input;
                }
            }
        }
    }
}