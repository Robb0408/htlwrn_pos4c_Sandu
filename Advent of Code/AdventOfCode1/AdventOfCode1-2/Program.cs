using System.Text;

var lines = await File.ReadAllLinesAsync("calibration.txt");
var linesList = lines.ToList();
Dictionary<string, int> numbers = new()
{
    { "one", 1 },
    { "two", 2 },
    { "three", 3 },
    { "four", 4 },
    { "five", 5 },
    { "six", 6 },
    { "seven", 7 },
    { "eight", 8 },
    { "nine", 9 }
};

List<string> newLines = new();
StringBuilder sb = new();
StringBuilder temp = new();
// line iteration
linesList.ForEach(line =>
{
    // character iteration
    line.Select(c => c).ToList().ForEach(chr =>
    {
        if (char.IsNumber(chr))
        {
            sb.Append(chr);
        }
        else
        {
            temp.Append(chr);
            numbers
                .Where(number => temp.ToString().Contains(number.Key))
                .ToList()
                .ForEach(number =>
            {
                sb.Append(number.Value);
                temp.Clear();
                temp.Append(chr);
            });
        }
    });
    newLines.Add(sb.ToString());
    sb.Clear();
});
var result = newLines.Select(line =>
{
    var chars = line.ToCharArray();
    return line.Length >= 2 
        ? int.Parse(char.ToString(chars[0]) + char.ToString(chars[^1])) 
        : int.Parse(char.ToString(chars[0]) + char.ToString(chars[0]));
});
for (int i = 0; i < result.Count(); i++)
{
    Console.WriteLine($"{i+1,4}: {result.ElementAt(i)}");
}
Console.WriteLine("Sum:  " + result.Sum());
// 54807 too low