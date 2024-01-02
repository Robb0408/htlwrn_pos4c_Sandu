using System.Text;
using System.Text.RegularExpressions;

var lines = await File.ReadAllLinesAsync("engine.txt");
var lineList = lines.ToList();
StringBuilder sb = new();

for (var i = 0; i < lineList.Count; i++)
{
    sb.Append('.');
}

// add dots around the text to skip var handling bc of array indexes (god bless)
lineList.Insert(0, sb.ToString());
lineList.Add(sb.ToString());
lineList = lineList.Select(line => "..." + line + "...").ToList();

sb.Clear();
List<int> gearRatios = new();
Regex regex = new(@"\d{1,3}");
// line-iteration
for (var i = 1; i < lineList.Count - 1; i++)
{
    // character-iteration
    for (var j = 1; j < lineList[i].Length; j++)
    {
        if (lineList[i][j] == '*')
        {
            for (var k = -1; k <= 1; k++)
            {
                for (var l = -3; l <= 3; l++)
                {
                    sb.Append(lineList[i + k][j + l]);
                }

                sb.Append('\n');
            }

            var field = sb.ToString().Split("\n");
            Console.WriteLine(string.Join("\n", field));
            var collection = regex.Matches(sb.ToString());
            Console.WriteLine(string.Join(", ", collection.Select(o => o.Value)));
            sb.Clear();
            var gearRatio = 1;
            if (collection.Count >= 2)
            {
                foreach (Match o in collection)
                {
                    var index = field
                        .Where(line =>
                        {
                            if (line.Contains(o.Value))
                            {
                                var match = regex.Matches(line);
                                var isSame = false;
                                foreach (Match m in match)
                                {
                                    if (m.Value == o.Value)
                                    {
                                        isSame = true;
                                    }
                                }
                                return isSame;
                            }
                            return false;
                        })
                        .Select(line => line.IndexOf(o.Value, StringComparison.Ordinal))
                        .First();
                    if (index is >= 2 and <= 4 || index + o.Length - 1 is >= 2 and <= 4)
                    {
                        gearRatio *= int.Parse(o.Value);
                        Console.WriteLine($"Value: {o.Value} Index: {index} Length: {o.Length}");
                    }
                    else
                    {
                        Console.WriteLine($"Value: {o.Value} Index: {index} - Invalid");
                    }
                }

                if (gearRatio > 1)
                {
                    gearRatios.Add(gearRatio);
                }
            }
            else
            {
                Console.WriteLine("Invalid");
            
            }
        }
    }
}

var count = 0;
gearRatios.ForEach(num =>
{
    Console.WriteLine($"{count,4}: {num}");
    count++;
});

Console.WriteLine(gearRatios.Sum());
//79842967