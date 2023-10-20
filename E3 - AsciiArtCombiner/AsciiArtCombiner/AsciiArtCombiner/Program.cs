// See https://aka.ms/new-console-template for more information

using FileCombiner;

<<<<<<< Updated upstream
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
=======
List<string> allFileContent; // Array of a list to store the content of every file
>>>>>>> Stashed changes

if (args.Length < 2)
{
    throw new ArgumentException($"Program takes 2 files or more. Only {args.Length} provided.");
}
try
{
    // Read input
    allFileContent = new List<string>();
    ReadFiles(allFileContent, args);
}
catch (FileNotFoundException ex)
{
    throw ex;
}
<<<<<<< Updated upstream
catch (IOException)
{   
    DisplayError($"An error occured while trying to read.");
    return;
}

if (!IsSameSize(allFileContent) && !(args.Contains("--v") ^ args.Contains("--variable")))
{
    DisplayError("One or more files do not have the same size. All files have to be the same size " +
        "(except when using \"-v\" or \"--variable\").");
    return;
=======
catch (IOException ex)
{
    throw ex;
}
if (!IsSameSize(allFileContent))
{
    throw new ArgumentException("One or more files are not the same size. All files have to be the same size.");
>>>>>>> Stashed changes
}
string combinedContent = AsciiCombiner.Combine(allFileContent);
Console.WriteLine(combinedContent);


<<<<<<< Updated upstream
/// <summary>
/// Reads the content of every provided file and stores the content in a list as strings.
/// </summary>
/// <param name="fileContent">Where the content of every file is stored in.</param>
/// <param name="fileNames">Filenames to read from.</param>
=======
>>>>>>> Stashed changes
static void ReadFiles(List<string> fileContent, string[] fileNames)
{
    foreach (var file in fileNames)
    {
<<<<<<< Updated upstream
        if (file == "--v" || file == "--variable")
        {
            continue;
        }
=======
>>>>>>> Stashed changes
        fileContent.Add(File.ReadAllText(file));
    }
}

/// <summary>
/// Checks if the size of every file is the same
/// </summary>
<<<<<<< Updated upstream
/// <param name="fileContent">List containing the content of every file.</param>
/// <returns>
/// True: Every file has the same size.
/// False: Not every file has the same size.
/// </returns>
static bool IsSameSize(List<string> fileContent)
{
    // Takes size from first file as base to compare
    string[] lines = fileContent[0].Split(Environment.NewLine);
    int height = lines.Length;
    int length = lines[0].Length;

    // Comparing size with every file
    for (int i = 0; i < fileContent.Count; i++) 
=======
static bool IsSameSize(List<string> fileContent)
{
    string[] line1 = fileContent[0].Split(Environment.NewLine);
    int length1 = line1[0].Length;
    int rows1 = line1.Length;
    for (int i = 1; i < fileContent.Count; i++) 
>>>>>>> Stashed changes
    {
        string[] line = fileContent[i].Split(Environment.NewLine);
        int length = line[2].Length;
        int rows = line.Length;
        if (length1 != length || rows1 != rows)
        {
            return false;
        }
    }
    return true;
}
<<<<<<< Updated upstream

/// <summary>
/// Displays a error message in red color in the console.
/// </summary>
/// <param name="message">Error message that should be displayed.</param>
static void DisplayError(string message)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(message);
    Console.ResetColor();
}
=======
>>>>>>> Stashed changes
