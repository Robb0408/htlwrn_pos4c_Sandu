var lines = await File.ReadAllLinesAsync("calibration.txt");
var charConvert = lines
    .Select(line => line.Select(c => c).Where(char.IsNumber));

var coordinates = charConvert.Select(line =>
{
    var enumerable = line.ToList();
    char[] digits;
    if (enumerable.Count >= 2)
    {
        
        digits = new[]
        {
            enumerable[0],
            enumerable[^1]
        };
        return Convert.ToInt32(new string(digits));
    }
    digits = new[]
    {
        enumerable[0],
        enumerable[0]
    };
    return Convert.ToInt32(new string(digits));
});

Console.WriteLine(coordinates.Sum());