using System.Text.Json;

namespace LogAnalysis.Logic;

public static class LogAnalyzer
{
    public static IOrderedEnumerable<KeyValuePair<string, IOrderedEnumerable<KeyValuePair<string, int>>>> AnalyzeMonthly(List<Data> linesOfData)
    {
        return linesOfData
            .GroupBy(data => data.Url, data => data.Date)
            .ToDictionary(
                group => group.Key,
                group => group
                    .GroupBy(data => data.ToString("yyyy-MM"))
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
            .GroupBy(data => data.Url, data => data.Date)
            .ToDictionary(
                group => group.Key,
                group => group
                    .GroupBy(data => data.ToString("HH:00"))
                    .ToDictionary(
                        dateGroup => dateGroup.Key,
                        dateGroup => dateGroup.Count()
                    )
                    .OrderBy(n => n.Key)
            )
            .OrderBy(n => n.Key);
    }

    public static List<KeyValuePair<List<string>, int>> GetPhotographerCount(List<Data> dataList)
    {
        var photos = JsonSerializer.Deserialize<List<Photo>>(File.ReadAllText("photographers.json"))!;

        return dataList
            .GroupBy(n => n.Url)
            .ToDictionary(
                group => photos.Where(photo => photo.Pic == group.Key).Select(photo => photo.TakenBy).ToList(),
                group => group.Count()
            )
            .OrderByDescending(n => n.Value)
            .ToList();
    }
}
