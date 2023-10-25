using E6___Website_Analytics;

List<Data> dataList = new();
try
{
    dataList = File.ReadLines(args[0])
        .Select(n =>
        {
            string[] splitData = n.Split("\t");
            return new Data
            {
                Url = splitData[0],
                Date = splitData[1],
            };
        }).ToList();
}
catch (IndexOutOfRangeException ex)
{
    Console.WriteLine($"Not enough arguments provided:\n{ex.Message}");
}
catch (IOException ex)
{
    Console.WriteLine($"Error while trying to read:\n{ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected Error:\n{ex.Message}");
}

var list = dataList.Skip(1)
    .GroupBy(data => data.Url);