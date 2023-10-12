string fileName;
string? groupByCol, numericCol;
List<string> lines = new();

if (args.Length != 3)
{
    DisplayError("Not enough or too many arguments provided, expected 3.");
    Environment.Exit(1);
}
fileName = args[0];
try
{
    lines = File.ReadLines(fileName).ToList();
} 
catch (FileNotFoundException) 
{
    DisplayError("File not found.");
    Environment.Exit(2);
}
catch (IOException)
{
    DisplayError("Error occured while trying to read.");
    Environment.Exit(3);
}
catch (Exception)
{
    DisplayError("Unknown error.");
    Environment.Exit(4);
}

string[] headerCols = lines[0].Split("\t");

foreach (string col in headerCols)
{
    if (args[1] == col)
    {
        groupByCol = col;
    }
    else if (args[2] == col)
    {
        numericCol = col;
    }
}

if (groupByCol == null || numericCol == null)
{

}
lines.Remove(lines[0]); // Remove header line for easier handling


static void DisplayError(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ResetColor();
}