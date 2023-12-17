using System.Diagnostics;
using AdventOfCode8_2;

Stopwatch watch = new();
watch.Start();
var content = await File.ReadAllLinesAsync("directions.txt");
var instructions = content[0].ToCharArray().ToList();
Console.WriteLine($"Instructions: {string.Join(", ", instructions)}");
var nodes = content.Skip(2).Select(line =>
{
    var nodeName = line[..3];
    var nodeLeft = line[7..10];
    var nodeRight = line[12..15];
    return new Node { Name = nodeName, Left = nodeLeft, Right = nodeRight };
}).ToList();

var currentNodes = nodes.Where(node => node.Name.EndsWith('A')).ToList();
Console.WriteLine($"Current nodes: {string.Join(", ", currentNodes)}");
var instructionsIndex = 0;
/*while (!currentNodes.TrueForAll(node => node.Name.EndsWith('Z')))
{
    currentNodes = currentNodes.Select(currentNode =>
    {
        var destination = instructions[instructionsIndex] == 'L'
            ? currentNode.Left
            : currentNode.Right;
        return nodes.First(node => node.Name == destination);
    }).ToList();
    count++;
    if (instructionsIndex < instructions.Count - 1)
    {
        instructionsIndex++;
    }
    else
    {
        instructionsIndex = 0;
    }

    if (currentNodes.Exists(currentNode => currentNode.Name.EndsWith('Z')))
    {
        Console.WriteLine($"Match ({currentNodes.Count(currentNode => currentNode.Name.EndsWith('Z'))}" +
                          $"/{currentNodes.Count}) nodes: {string.Join(", ", currentNodes)}");
    }
}*/

var records = new List<long>();
currentNodes.ForEach(node =>
{
    var count = 0L;
    while (!node.Name.EndsWith('Z'))
    {
        var destination = instructions[instructionsIndex] == 'L'
            ? node.Left
            : node.Right;
        node = nodes.First(n => n.Name == destination);
        count++;
        if (instructionsIndex < instructions.Count - 1)
        {
            instructionsIndex++;
        }
        else
        {
            instructionsIndex = 0;
        }
    }
    records.Add(count);
});

watch.Stop();
Console.WriteLine($"Counts: {string.Join(", ", records)}");

//least common multiple of all records
var lcm = records.Aggregate((a, b) => a * b / GreatestCommonDivisor(a, b));


Console.WriteLine($"Least common multiple: {lcm}");
Console.WriteLine($"Elapsed: {watch.ElapsedMilliseconds}ms");
return;

static long GreatestCommonDivisor(long a, long b)
{
    while (b != 0)
    {
        var t = b;
        b = a % b;
        a = t;
    }

    return a;
}
