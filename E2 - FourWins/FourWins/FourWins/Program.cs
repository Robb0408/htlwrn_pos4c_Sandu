﻿using System.Numerics;

namespace FourWins;

public class Program
{
    public static int Main(string[] args)
    {
        int[,] board;
        int width, height, column, winnerPlayer;
        bool player = false; // false: Player 1 (red), true: Player 2 (yellow)
        if (args.Length > 0 && args[0].Contains("x"))
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
                Console.Write($"Please choose a column (1-{board.GetLength(1)}): ");
                string? input = Console.ReadLine();
                while (input == "" || !int.TryParse(input, out int number)) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input");
                    Console.ResetColor();
                    Console.Write($"Please choose a column (1-{board.GetLength(1)}): ");
                    input = Console.ReadLine();
                }
                column = Convert.ToInt32(input);
                if (column < 1 || column > board.GetLength(1))
                {
                    Console.WriteLine("Column number does not exist");
                }
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
        for (int i = 0; i < field.GetLength(1) * 2 + 1; i++)
        {
            Console.Write("  ");
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
            for (int k = 0; k < field.GetLength(1) * 2 + 1; k++)
            {
                Console.Write("  ");
            }
            Console.ResetColor();
            Console.WriteLine();
        }
        for (int i = 1; i <= field.GetLength(1); i++)
        {
            if (i < 10)
                Console.Write("   ");
            else
                Console.Write("  ");
            Console.Write(i);

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
        if (addOnColumn < 1 || addOnColumn > field.GetLength(1))
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
        // Check for a draw (no field is 0)
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
        for (int i = field.GetLength(0) - 1; i >= 0; i--)
        {
            for (int j = 0; j < field.GetLength(1) - 3; j++)
            {
                // Skip field if no player placed a disk in it
                if (field[i, j] == 0)
                    continue;
                // Check horizontal
                else if (field[i, j] == field[i, j + 1] && field[i, j] == field[i, j + 2] && field[i, j] == field[i, j + 3])
                {
                    winnerPlayer = field[i, j];

                }
                else if (i >= 3)
                {
                    if (
                    // Check diagonal (bottom left - top right) /
                    (field[i, j] == field[i - 1, j + 1] && field[i, j] == field[i - 2, j + 2] && field[i, j] == field[i - 3, j + 3]) ||
                    // Check columnwise
                    (field[i, j] == field[i - 1, j] && field[i, j] == field[i - 2, j] && field[i, j] == field[i - 3, j]))
                    {
                        winnerPlayer = field[i, j];
                    }
                    else if (j >= 3)
                    {
                        if (
                        // Check diagonal (top left - bottom right) \
                        (field[i, j] == field[i - 1, j - 1] && field[i, j] == field[i - 2, j - 2] && field[i, j] == field[i - 3, j - 3]))
                        {
                            winnerPlayer = field[i, j];
                        }
                    }
                }
                    /*else if (
                        (field[i, j] == field[i, j + 1] && field[i, j] == field[i, j + 2] && field[i, j] == field[i, j + 3]) ||
                        ((i >= 3) && (field[i, j] == field[i - 1, j] && field[i, j] == field[i - 2, j] && field[i, j] == field[i - 3, j]) ||
                        (field[i, j] == field[i - 1, j + 1] && field[i, j] == field[i - 2, j + 2] && field[i, j] == field[i - 3, j + 3])) ||
                        ((i < field.GetLength(0) - 3) && (field[i, j] == field[i + 1, j + 1] && field[i, j] == field[i + 2, j + 2] && field[i, j] == field[i + 3, j + 3]))
                        )
                    {
                        winnerPlayer = field[i, j];
                    }
                } catch (Exception e)
                {
                    Console.WriteLine($"{i} and {j}");
                    Environment.Exit(1);
                }*/
            }
        }
        switch (winnerPlayer)
        {
            case 0:
            case 1: 
            case 2: return true; 
            default: return false;
        }
    }
}
