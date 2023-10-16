// Check argument count
if (args.Length < 3 || args.Length > 4)
{
    DisplayError("Not enough or too many arguments provided, expected 3.");
    Environment.Exit(1);
}

List<string> lines = new();
string fileName = args[0];

// Read the file and catch possible exceptions
try
{
    lines = File.ReadLines(fileName).ToList();
}
catch (FileNotFoundException ex)
{
    DisplayError($"File {Path.GetFileName(ex.FileName)} not found.");
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

// Split the lines
string[] headerCols = lines[0].Split("\t");

if (!(headerCols.Contains(args[1]) || headerCols.Contains(args[2])))
{
    DisplayError("Group by column or numeric column does not exist.");
    Environment.Exit(5);
}

int groupByColIndex = Array.IndexOf(headerCols, args[1]);
int numericColIndex = Array.IndexOf(headerCols, args[2]);

lines.Remove(lines[0]); // Remove header line for easier handling

var result = ReadData(lines, groupByColIndex, numericColIndex)
    .GroupBy(n => n.Item1)
    .Select(group =>
    {
        return new
        {
            GroupByKey = group.Key,
            NumericColSum = group.Sum(n => n.Item2)
        };
    })
    .OrderByDescending(n => n.NumericColSum);

int maxCount = (args.Length == 4 ) ? int.Parse(args[3]) : result.Count();
int maxLength = result.Take(maxCount).Max(n => n.GroupByKey.Length);
int baseAmount = result.First().NumericColSum;
int rate;

for (int i = 0; i < maxCount; i++)
{
    Console.Write(result.ElementAt(i).GroupByKey.PadLeft(maxLength) + " | ");
    Console.BackgroundColor = ConsoleColor.Red;
    rate = result.ElementAt(i).NumericColSum * 100 / baseAmount;
    for (int j = 1; j <= rate; j++)
    {
        Console.Write(" ");
    }
    Console.ResetColor();
    Console.WriteLine();
}

/// <summary>
/// Reads data from two columns.
/// </summary>
static IEnumerable<(string, int)> ReadData(List<string> source, int index1, int index2)
{
    foreach (string line in source)
    {
        string[] splitLine = line.Split("\t");
        yield return (
            splitLine[index1],
            int.Parse(splitLine[index2])
        );
    }
}

/// <summary>
/// Displays an error in the console.
/// </summary>
static void DisplayError(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ResetColor();
}