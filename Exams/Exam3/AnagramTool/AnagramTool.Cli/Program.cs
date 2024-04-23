using AnagramTool.Logic.Services.Implementations;
using Cocona;

// Cocona is the best thing to exist for Cli projects and I am in love with it
var app = CoconaApp.Create();

app.AddCommand("check", ([Argument] string word1, [Argument] string word2) =>
{
    var service = new AnagramService(new AnagramDictionaryService());
    if (service.CheckAnagram(word1, word2))
    {
        Console.WriteLine($"{word1} and {word2} are anagrams.");
    }
    else
    {
        Console.WriteLine($"{word1} and {word2} are no anagrams.");
    }
});

app.AddCommand("find", async ([Argument] string word) =>
{
    var service = new AnagramService(new AnagramDictionaryService());
    var result = await service.FindAnagramsAsync(word);
    // HAU: ℹ️ you could also use Any() 
    if (result.Count() > 0) 
    { 
        Console.WriteLine(string.Join("\n", result));
    }
    else
    {
        Console.WriteLine("No known anagrams found.");
    }
});

app.Run();