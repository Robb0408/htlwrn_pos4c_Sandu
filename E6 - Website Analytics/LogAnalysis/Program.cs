﻿using System.Globalization;
using System.Text;
using System.Text.Json;
using LogAnalysis;
using LogAnalysis.Logic;


if (args.Length == 0)
{
    Console.WriteLine("No arguments given");
    Environment.Exit(4);
}
else if (!args.Contains("monthly") && !args.Contains("hourly") && !args.Contains("photographers"))
{
    Console.WriteLine("Invalid argument");
    Environment.Exit(5);
}

int timeIndex = args[0] == "monthly" ? 1 : 2;
List<Data> dataList = new();

try
{
    dataList = File.ReadLines("access-log.txt")
        .Skip(1)
        .Select(n =>
        {
            string[] splitData = n.Split("\t");
            return new Data
            {
                Url = splitData[0],
                Date = DateTime.Parse(splitData[timeIndex], new CultureInfo("en-US"))
            };
        }).ToList();
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine($"Not enough arguments provided:\n{ex.Message}");
    Environment.Exit(1);
}
catch (IOException ex)
{
    Console.WriteLine($"Error while trying to read:\n{ex.Message}");
    Environment.Exit(2);
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected Error:\n{ex.Message}");
    Environment.Exit(3);
}

// Logic from library
if (args[0] == "monthly")
{
    var stats = LogAnalyzer.AnalyzeMonthly(dataList).Where(n => args.Length != 2 || n.Key == args[1]);

    foreach (var entry in stats)
    {
        Console.WriteLine(entry.Key + ":");
        int totalDownloads = entry.Value.Sum(n => n.Value);
        foreach (var date in entry.Value)
        {
            Console.WriteLine($"\t{date.Key}: {date.Value}");
        }
        Console.WriteLine($"\tTOTAL: {totalDownloads}");
    }
}
else if (args[0] == "hourly")
{
    var stats = LogAnalyzer.AnalyzeHourly(dataList).Where(n => args.Length != 2 || n.Key == args[1]);

    foreach (var item in stats)
    {
        Console.WriteLine(item.Key + ":");
        int totalDownloads = item.Value.Sum(n => n.Value);
        foreach (var time in item.Value)
        {
            Console.WriteLine($"\t{time.Key}: {decimal.Round(decimal.Divide(time.Value * 100, totalDownloads), 2)} %");
        }
    }
} 
else
{
    List<Photo> photos = new();
    try
    {
        photos = JsonSerializer.Deserialize<List<Photo>>(File.ReadAllText("photographers.json"))!;
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Environment.Exit(6);
    }

    foreach (var item in photos)
    {
        dataList.Where(n => n.Url == item.Pic)
            .ToDictionary(n => item.TakenBy, n => /*continue*/);
    }

    /*foreach (var photo in photoSummary)
    {
        Console.WriteLine(photo.Key + ": " + photo.Value);
    }*/
}

 ///////////////////////////////////
// TODO: -Advanced Requirements   //
//                                //
// DONE: -Minimum Requirements    //
//       -Additional Requirements //
///////////////////////////////////