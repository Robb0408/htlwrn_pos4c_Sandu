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

    public static IEnumerable<KeyValuePair<string, int>> GetPhotographerCount(List<Data> dataList)
    {
        return JsonSerializer.Deserialize<List<Photo>>(File.ReadAllText("photographers.json"))!
            .GroupBy(photo => photo.TakenBy)
            .ToDictionary(
                group => group.Key,
                group => dataList.Count(data => group.Any(p => p.Pic == data.Url)))
            .OrderByDescending(p => p.Value);
    }
}
