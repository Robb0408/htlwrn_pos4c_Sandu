// See https://aka.ms/new-console-template for more information

using FileCombiner;

List<string> allFileContent; // Array of a list to store the content of every file

if (args.Length < 2)
{
    throw new ArgumentException($"Program takes 2 files or more. Only {args.Length} provided.");
}
try
{
    // Input file content and store it into a list array
    allFileContent = new List<string>();
    ReadFiles(allFileContent, args);
}
catch (FileNotFoundException ex)
{
    throw ex;
}
catch (IOException ex)
{
    throw ex;
}
if (!IsSameSize(allFileContent))
{
    throw new ArgumentException("One or more files are not the same size. All files have to be the same size.");
}
string combinedContent = AsciiCombiner.Combine(allFileContent);
Console.WriteLine(combinedContent);


static void ReadFiles(List<string> fileContent, string[] fileNames)
{
    foreach (var file in fileNames)
    {
        fileContent.Add(File.ReadAllText(file));
    }
}

/// <summary>
/// Checks if the size of every file is the same
/// </summary>
static bool IsSameSize(List<string> fileContent)
{
    string[] line1 = fileContent[0].Split(Environment.NewLine);
    int length1 = line1[0].Length;
    int rows1 = line1.Length;
    for (int i = 1; i < fileContent.Count; i++) 
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
