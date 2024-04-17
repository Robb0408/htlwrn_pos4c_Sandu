﻿using Cocona;
using FileContentAnalyzer.Logic;

var app = CoconaApp.Create();
var service = new FileContentAnalyzerService();
app.AddCommand("wordfrequency", async (string fileName) => 
{
    var content = await File.ReadAllTextAsync(fileName);
    var result = service.GetFrequency(content);
    foreach (var item in result)
    {
        Console.WriteLine($"{item.Key}: {item.Value}");
    }
});

app.AddCommand("uniquewords", async (string fileName) =>
{
    var content = await File.ReadAllTextAsync(fileName);
    var result = service.GetUniqueWordsCount(content);
    Console.WriteLine(result);
});

app.AddCommand("longestwords", async (string fileName) =>
{
    var content = await File.ReadAllTextAsync(fileName);
    var result = service.GetLongestWords(content);
    foreach (var item in result)
    {
        Console.WriteLine(item);
    }
});
app.AddCommand("similarity", async (string fileName1, string fileName2) =>
{
    var content1 = await File.ReadAllTextAsync(fileName1);
    var content2 = await File.ReadAllTextAsync(fileName2);
    var result = service.GetSimilarity(content1, content2);
    Console.WriteLine(result);
});

app.Run();