using System.Text.Json;

namespace LogAnalysis.Logic;

public static class LogAnalyzer
{
    public static IOrderedEnumerable<KeyValuePair<string, IOrderedEnumerable<KeyValuePair<string, int>>>> AnalyzeMonthly(List<Data> linesOfData)
    {
        return linesOfData
            .GroupBy(data => data.Url)
            .ToDictionary(
                group => group.Key,
                group => group
                    .GroupBy(data => data.Date.ToString("yyyy-MM"))
                    .ToDictionary(
                        dateGroup => dateGroup.Key,
                        dateGroup => dateGroup.Count()
                    )
                    .OrderBy(n => n.Key)
            )
            .OrderBy(n => n.Key);
    }

    public static IOrderedEnumerable<KeyValuePair<string, IOrderedEnumerable<KeyValuePair<string, int>>>> AnalyzeHourly(List<Data> linesOfData)
    {
        return linesOfData
            .GroupBy(data => data.Url)
            .ToDictionary(
                group => group.Key,
                group => group
                    .GroupBy(data => data.Date.ToString("HH" + ":00"))
                    .ToDictionary(
                        dateGroup => dateGroup.Key,
                        dateGroup => dateGroup.Count()
                    )
                    .OrderBy(n => n.Key)
            )
            .OrderBy(n => n.Key);
    }

    public static List<KeyValuePair<string, int>> GetPhotographerCount(List<Data> dataList)
    {
        var photos = JsonSerializer.Deserialize<List<Photo>>(File.ReadAllText("photographers.json"))!;
        List<KeyValuePair<string, int>> photographerPicCount = new();

        foreach (var item in photos)
        {
            var test = dataList
                .Where(n => n.Url == item.Pic)
                .GroupBy(n => n.Url)
                .ToDictionary(
                    group => item.TakenBy,
                    group => group.Count()
                )
                .First();

            photographerPicCount.Add(test);
        }
        return photographerPicCount.OrderByDescending(n => n.Value).ToList();
    }
}
