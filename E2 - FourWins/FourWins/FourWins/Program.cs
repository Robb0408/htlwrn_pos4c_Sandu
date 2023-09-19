namespace FourWins;

public class Program
{
    public static int Main(string[] args)
    {
        int[,] board = new int[6,7];
        PrintGameField(board);

        /*
        do
        {
            PrintGameField(board);
        }
        while (IsGameEnd(board));*/
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
    /*private static bool AddPlayerDisc(int[,] field, int playerNr, int addOnColumn)
    {
        // Your code here!  
    }*/

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
    private static bool IsGameEnd(int[,] field/*, out int winnerPlayer*/)
    {
        return false;
    }
}
