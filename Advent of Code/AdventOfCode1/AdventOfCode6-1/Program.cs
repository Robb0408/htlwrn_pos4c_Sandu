var content = await File.ReadAllLinesAsync("races.txt");
var split = content.Select(line => line.Split(":")[1].Trim());

var maxTime = split.ElementAt(0).Split("   ")
    .Where(s => s.Length > 0)
    .Select(s => int.Parse(s.Trim())).ToList();
var maxDistance = split.ElementAt(1).Split("   ")
    .Where(s => s.Length > 0)
    .Select(s => int.Parse(s.Trim())).ToList();

var possible = new List<int> { 0, 0, 0, 0 };
for (var i = 0; i < maxTime.Count; i++)
{
    var time = maxTime[i];
    var buttonHold = 0;
    while (buttonHold <= time)
    {
        var traveled = (time - buttonHold) * buttonHold;
        if (traveled > maxDistance[i])
        {
            possible[i] += 1;
        }

        buttonHold++;
    }
}

int sum = 1;
possible.ForEach(n => sum *= n);

Console.WriteLine(sum);
