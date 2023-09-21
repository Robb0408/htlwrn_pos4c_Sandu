namespace FourWins;

public class Program
{
    public static int Main(string[] args)
    {
        int[,] board;
        int width, height, column, winnerPlayer;
        bool player = false; // false: Player 1 (red), true: Player 2 (yellow)
        if (args.Length > 0)
        {
            string[] sizeArr = args[0].Split("x");
            if (int.TryParse(sizeArr[0], out int x))
                width = x;
            else
                width = 7;
            if (int.TryParse(sizeArr[1], out int y))
                height = y;
            else
                height = 6;
            if (height < 4 && width < 4) // Minimum size
            {
                width = 7;
                height = 6;
            }  
        }
        else
        {
            width = 7;
            height = 6;
        }
        board = new int[height, width];
        // Game loop
        do
        {
            PrintGameField(board);
            do
            {
                if (player)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                else 
                    Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Player Nr. {((player) ? 2 : 1)}");
                Console.ResetColor();
                Console.Write("Please choose a column: ");
                string? input = Console.ReadLine();
                while (input == null){
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input");
                    Console.ResetColor();
                    Console.Write("Please choose a column: ");
                    input = Console.ReadLine();
                }
                column = Convert.ToInt32(input);
            }
            while (!AddPlayerDisc(board, (player) ? 1 : 0, column));
            player = !player;
            IsGameEnd(board, out int winner);
            winnerPlayer = winner;
        }
        while (winnerPlayer == 0);
        PrintGameField(board);
        if (winnerPlayer == -1)
            Console.WriteLine("Draw - nobody has won!");
        else
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
        // Print first row of blue background
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
                if (field[i, j] == 1)
                    Console.BackgroundColor = ConsoleColor.Red;
                else if (field[i, j] == 2) 
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
        // Validate input
        if (addOnColumn < 1 || addOnColumn > 7)
            return false;
        // Find last free square in column and place the disk
        for (int i = 0; i < field.GetLength(0); i++)
        {
            if (field[i, addOnColumn - 1] != 0)
            {
                //If column is full return false
                if (i == 0)
                    return false;
                field[i - 1, addOnColumn - 1] = playerNr + 1;
                return true;
            }
            else if (i == field.GetLength(0) - 1)
            {
                field[i, addOnColumn - 1] = playerNr + 1;
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
        winnerPlayer = -1;
        for (int i = 0; i < field.GetLength(0); i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if (field[i, j] == 0)
                {
                    winnerPlayer = 0;
                }
            }
        }
        if (winnerPlayer == -1)
            return true;
        for (int i = 0; i < field.GetLength(0) - 3; i++)
        {
            for (int j = 0; j < field.GetLength(1); j++)
            {
                if (field[i, j] == field[i + 1, j] && field[i, j] == field[i + 2, j] && field[i, j] == field[i + 3, j])
                {
                    winnerPlayer = field[i, j];
                    return true;
                }
            }
        }
        return false;
    }
}
