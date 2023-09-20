namespace FourWins;

public class Program
{
    public static int Main(string[] args)
    {
        int[,] board;
        bool player = false;
        int winnerPlayer;
        if (args.Length > 0)
        {
            int.TryParse(args[0], out int x);
            int.TryParse(args[1], out int y);
            board = new int[x, y];
        }
        board = new int[6,7];
        do
        {
            PrintGameField(board);
            Console.Write("Player Nr. 1\nPlease choose a column: ");
            int.TryParse(Console.ReadLine(), out int column);
            AddPlayerDisc(board, (player) ? 1 : 0, column);
            player = !player;
            IsGameEnd(board, out int winner);
            winnerPlayer = winner;
        }
        while (winnerPlayer == 0);
        Console.WriteLine($"Player {winnerPlayer} has won!");
        return 0;
    }

    /// <summary>
    /// Prints the game field on the console.
    /// </summary>
    /// <remarks>
    /// Walls are blue or other chars, player one is red and/or 'x' , player two is yellow and/or 'o'.
    /// </remarks>
    /// <param name="field">The field.</param>
    private static void PrintGameField(int[,] field)
    {
        Console.Clear();
        // Print first row of only blue background
        Console.BackgroundColor = ConsoleColor.Blue;
        for (int i = 0; i < field.GetLength(1) * 4 + 2; i++)
        {
            Console.Write(" ");
        }
        Console.WriteLine();
        // Print board elements shown as square fields surrounded by blue borders
        // Rows
        for (int i = 0; i < field.GetLength(0); i++)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Write("  ");
            // Columns
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if (field[i, j] != 0)
                    Console.BackgroundColor = ConsoleColor.Yellow;
                else
                    Console.ResetColor();
                Console.Write("  ");
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write("  ");
            }
            Console.WriteLine();
            // Print a blue border row
            for (int k = 0; k < field.GetLength(1) * 4 + 2; k++)
            {
                Console.Write(" ");
            }
            Console.WriteLine();
            Console.ResetColor();
        }
        for (int i = 1; i <= field.GetLength(1) * 4; i++)
        {
            if (i % 4 == 0)
                Console.Write(i / 4);
            else 
                Console.Write(" ");
            
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Adds the player disc to the game board on the given column.
    /// </summary>
    /// <param name="field">The playing field.</param>
    /// <param name="playerNr">The player nr.</param>
    /// <param name="addOnColumn">The column number to add the disc.</param>
    /// <remarks>
    /// Searches for the new place regarding the rules of four in a row.
    /// Note: the disc slides down. 
    /// </remarks>
    /// <returns>
    ///    <c>true</c> if the add of disc is possible; otherwise, <c>false</c>.
    /// </returns>
    private static bool AddPlayerDisc(int[,] field, int playerNr, int addOnColumn)
    {
        for (int i = 0; i < field.GetLength(0); i++)
        {
            if (field[i, addOnColumn - 1] != 0 || i == field.GetLength(0) - 1)
            {
                field[i - 1, addOnColumn - 1] = playerNr;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Determines if the game end is reached.
    /// Possible ends:
    /// Player 1 has four in a row
    /// Player 2 has four in a row
    /// Game field is full and no player has four in a row
    /// </summary>
    /// <param name="field">The field.</param>
    /// <param name="winnerPlayer">
    /// The winning player.
    /// 0: Nothing changed 
    /// 1: Player 1 wins
    /// 2: Player 2 wins
    /// -1: Draw - nobody won
    /// </param>
    /// <returns>
    ///   <c>true</c> if the game has ended; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsGameEnd(int[,] field, out int winnerPlayer)
    {
        winnerPlayer = 0;
        return true;
    }
}
