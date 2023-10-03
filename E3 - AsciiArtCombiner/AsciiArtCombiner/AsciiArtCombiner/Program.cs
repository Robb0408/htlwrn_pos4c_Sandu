// See https://aka.ms/new-console-template for more information

string[] lines = File.ReadAllLines(args[0]);

foreach (var line in lines)
{
    Console.WriteLine(line);
}