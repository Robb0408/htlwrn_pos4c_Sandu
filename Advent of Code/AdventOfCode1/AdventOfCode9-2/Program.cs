/*
 * Sample lines
 * 0 3 6 9 12 15
 * 1 3 6 10 15 21
 * 10 13 16 21 30 45
 */

var content = await File.ReadAllLinesAsync("input.txt");
var metrics = content.ToList();

var sum = 0;
//main loop
foreach (var metric in metrics)
{
    var numbers = metric.Split(" ").Select(int.Parse).ToList(); // 0, 3, 6, 9, 12, 15
    var sequence = new List<List<int>>();
    sequence.Add(numbers);
    // loop while a list does not contain only zeros in sequence
    while (!sequence[^1].TrueForAll(n => n == 0))
    {
        var last = sequence[^1];
        var differences = new List<int>();
        for (var i = 0; i < last.Count - 1; i++)
        {
            var difference = last[i + 1] - last[i];
            differences.Add(difference);
        }
        sequence.Add(differences);
    }
    sequence[^1].Insert(0, 0);
    
    for (var i = sequence.Count - 2; i >= 0; i--)
    {
        var add = sequence[i][0] - sequence[i + 1][0];
        sequence[i].Insert(0, add);
    }
    sum += sequence[0][0];
    // print sequence (only for debug)
    sequence.ForEach(l => Console.WriteLine(string.Join(" ", l)));
}

Console.WriteLine($"Sum: {sum}");