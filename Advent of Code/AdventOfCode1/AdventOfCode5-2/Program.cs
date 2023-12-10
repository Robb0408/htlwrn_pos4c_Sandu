using System.Diagnostics;

Stopwatch watch = new();
watch.Start();

var content = await File.ReadAllLinesAsync("seeds.txt");

var temp = content[0].Split(": ")[1].Split(" ");
var tempList = new List<(long, long)>();
var seeds = new List<Seed>();
for (var i = 0; i < temp.Length; i += 2)
{
    var seed = long.Parse(temp[i]);
    var range = long.Parse(temp[i + 1]);
    tempList.Add((seed, range));
}

tempList.ForEach(pair =>
{
    var seedNumber = pair.Item1;
    var range = pair.Item2;
    for (var i = 0; i < range; i++)
    {
        seeds.Add(new Seed { Number = seedNumber + i });
    }
});

var completeSeeds = new List<Seed>();

seeds.ForEach(seed =>
{
    var isSoil = false;
    var isFertilizer = false;
    var isWater = false;
    var isLight = false;
    var isTemperature = false;
    var isHumidity = false;
    var mapping = new List<long[]>();

    foreach (var line in content)
    {
        var chars = line.ToCharArray();
        if (chars.Length > 0 && !char.IsNumber(chars[0])) continue;
        if (!isSoil && line == string.Empty)
        {
            // If line empty, then go to next mapping
            seed.Soil = Map(seed.Number, mapping);
            isSoil = true;
            mapping.Clear();
        }
        else if (!isFertilizer && line == string.Empty)
        {
            // If line empty, then go to next mapping
            seed.Fertilizer = Map(seed.Soil, mapping);
            isFertilizer = true;
            mapping.Clear();
        }
        else if (!isWater && line == string.Empty)
        {
            // If line empty, then go to next mapping
            seed.Water = Map(seed.Fertilizer, mapping);
            isWater = true;
            mapping.Clear();
        }
        else if (!isLight && line == string.Empty)
        {
            // If line empty, then go to next mapping
            seed.Light = Map(seed.Water, mapping);
            isLight = true;
            mapping.Clear();
        }
        else if (!isTemperature && line == string.Empty)
        {
            // If line empty, then go to next mapping
            seed.Temperature = Map(seed.Light, mapping);
            isTemperature = true;
            mapping.Clear();
        }
        else if (!isHumidity && line == string.Empty)
        {
            // If line empty, then go to next mapping
            seed.Humidity = Map(seed.Temperature, mapping);
            isHumidity = true;
            mapping.Clear();
        }
        else
        {
            var split = line.Split(" ");
            mapping.Add(new[]
            {
                long.Parse(split[0]),
                long.Parse(split[1]), long.Parse(split[2])
            });
        }
    }

    seed.Location = Map(seed.Humidity, mapping);
    mapping.Clear();
    completeSeeds.Add(seed);
});

foreach (var seed in completeSeeds)
{
    Console.WriteLine($"Seed {seed.Number} is at location {seed.Location}" +
                      $"\n\tSoil: {seed.Soil}" +
                      $"\n\tFertilizer: {seed.Fertilizer}" +
                      $"\n\tWater: {seed.Water}" +
                      $"\n\tLight: {seed.Light}" +
                      $"\n\tTemperature: {seed.Temperature}" +
                      $"\n\tHumidity: {seed.Humidity}" +
                      $"\n\tLocation: {seed.Location}");
}

static long Map(long numberToMap, IEnumerable<long[]> mapping)
{
    var result = numberToMap;
    var isDone = false;
    foreach (var numbers in mapping)
    {
        var destination = numbers[0];
        var source = numbers[1];
        var range = numbers[2];

        for (var i = 0; i < range; i++)
        {
            if (result == source + i)
            {
                result = destination + i;
                isDone = true;
                break;
            }
        }
        if (isDone) { break; }
    }

    Console.WriteLine($"Mapped {numberToMap} to {result}");
    return result;
}

Console.WriteLine("Lowest location: " + completeSeeds.Min(seed => seed.Location));
Console.WriteLine("Total seeds: " + completeSeeds.Count);
watch.Stop();
Console.WriteLine($"Time: {watch.ElapsedMilliseconds}ms - {watch.ElapsedMilliseconds / 1000}s");

public class Seed
{
    public long Number { get; init; }
    public long Soil { get; set; }
    public long Fertilizer { get; set; }
    public long Water { get; set; }
    public long Light { get; set; }
    public long Temperature { get; set; }
    public long Humidity { get; set; }
    public long Location { get; set; }
}