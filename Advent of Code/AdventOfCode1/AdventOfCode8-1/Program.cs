using System.Diagnostics;
using AdventOfCode8_1;

Stopwatch watch = new();
watch.Start();
var content = await File.ReadAllLinesAsync("directions.txt");
var instructions = content[0].ToCharArray().ToList();

var nodes = content.Skip(2).Select(line =>
{
   var nodeName = line[..3];
   var nodeLeft = line[7..10];
   var nodeRight = line[12..15];
   return new Node { Name = nodeName, Left = nodeLeft, Right = nodeRight };
}).ToList();

nodes.ForEach(Console.WriteLine);

var currentNode = nodes.First(node => node.Name == "AAA");
var count = 0;
while (currentNode.Name != "ZZZ")
{
   foreach (var i in instructions)
   {
      if (currentNode.Name == "ZZZ")
      {
         break;
      }

      var destination = i == 'L' ? currentNode.Left : currentNode.Right;
      currentNode = nodes.First(node => node.Name == destination);
      count++;
   }
}

watch.Stop();
Console.WriteLine($"Count: {count}");
Console.WriteLine($"Elapsed: {watch.ElapsedMilliseconds}ms");

