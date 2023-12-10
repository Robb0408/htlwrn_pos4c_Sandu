using System.Text;

var content = await File.ReadAllLinesAsync("races.txt");
var split = content.Select(line => line.Split(":")[1].Trim());

StringBuilder sb = new();
var lines = new List<string>();
foreach (var s in split)
{
    var split2 = s.Split(" ");
    foreach (var s1 in split2)
    {
        sb.Append(s1);
    }
    lines.Add(sb.ToString());
    sb.Clear();
}

lines.ForEach(Console.WriteLine);

var maxTime = long.Parse(lines[0]);
var maxDistance = long.Parse(lines[1]);
long possible = 0;

for (var i = 0; i <= maxTime; i++)
{
    // i is button hold time
    if (i * (maxTime - i) > maxDistance)
    {
        possible++;
    }
}

Console.WriteLine(possible);