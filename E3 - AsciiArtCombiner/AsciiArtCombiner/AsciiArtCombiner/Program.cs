// See https://aka.ms/new-console-template for more information

using FileCombiner;

List<string> allFileContent; // A list to store the content of every file as string
if (args.Length == 0 || ((args.Contains("--v") ^ args.Contains("--variable")) && args.Length == 1))
{
    DisplayError("Program needs 2 files or more. No files provided.");
    return;
}
else if (args.Length < 2 || ((args.Contains("--v") ^ args.Contains("--variable")) && args.Length < 3))
{
    DisplayError($"Program needs 2 files or more. Only {args.Length} provided.");
    return;
}

try
{
    // Read input
    allFileContent = new List<string>();
    ReadFiles(allFileContent, args);
}
catch (FileNotFoundException ex)
{
    DisplayError($"File \"{Path.GetFileName(ex.FileName)}\" was not found.");
    return;
}
catch (IOException)
{   
    DisplayError($"An error occured while trying to read.");
    return;
}

if (!IsSameSize(allFileContent) && !(args.Contains("--v") ^ args.Contains("--variable")))
{
    DisplayError("One or more files do not have the same dimensions. All files have to be the same size " +
        "(except when using \"-v\" or \"--variable\".");
    return;
}

Console.WriteLine(AsciiCombiner.Combine(allFileContent));


/// <summary>
/// Reads the content of every provided file and stores the content in a list as strings.
/// </summary>
static void ReadFiles(List<string> fileContent, string[] fileNames)
{
    foreach (var file in fileNames)
    {
        if (file == "--v" || file == "--variable")
        {
            continue;
        }
        fileContent.Add(File.ReadAllText(file));
    }
}

/// <summary>
/// Checks if the size of every file is the same.
/// </summary>
/// <param name="fileContent">List containing the content of every file.</param>
static bool IsSameSize(List<string> fileContent)
{
    // Takes height from first file to compare
    string[] lines = fileContent[0].Split(Environment.NewLine);
    int height = lines.Length;
    int length = lines[0].Length;
    // Comparing dimensions with every file
    for (int i = 0; i < fileContent.Count; i++) 
    {
        string[] line = fileContent[i].Split(Environment.NewLine);
        if (line.Length != height)
        {
            return false;
        }
        for (int j = 0; j < line.Length; j++)
        {
            if (line[j].Length != length)
            {
                return false;
            }
        }
    }
    return true;
}

/// <summary>
/// Displays a error message in the console.
/// </summary>
/// <param name="message">Error message that should be displayed.</param>
static void DisplayError(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ResetColor();
}