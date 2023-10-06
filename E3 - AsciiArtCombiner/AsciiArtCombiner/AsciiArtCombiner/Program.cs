// See https://aka.ms/new-console-template for more information

List<string> allFileContent; // Array of a list to store the content of every file

if (args.Length < 2)
{
    throw new ArgumentException($"Program takes 2 files or more. Only {args.Length} provided.");
}
try
{
    // Input file content and store it into a list array
    allFileContent = new List<string>();
    foreach (var file in args)
    {
        allFileContent.Add(File.ReadAllText(file));
    }
}
catch (FileNotFoundException ex)
{
    throw ex;
}
catch (IOException ex)
{
    throw ex;
}
if (!IsSameSize())
{
    throw new ArgumentException("One or more files are not the same size. All files have to be the same size.");
}




bool IsSameSize()
{
    string[] line1 = allFileContent[0].Split("\n");
    int length1 = line1[0].Length;
    int rows1 = line1.Length;
    for (int i = 1; i < allFileContent.Count; i++) 
    {
        string[] line = allFileContent[i].Split("\n");
        int length = line[2].Length;
        int rows = line.Length;
        if (length1 != length || rows1 != rows)
        {
            return false;
        }
    }
    return true;
}
