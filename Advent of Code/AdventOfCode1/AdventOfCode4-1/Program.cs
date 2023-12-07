var lines = await File.ReadAllLinesAsync("cards.txt");
var cards = lines.Select(line => line.Split(": ")[1]).ToList();
var totalPoints = new List<int>();

cards.ForEach(card =>
{
    var sum = 0;
    var isDouble = false;
    var winning = card
        .Split(" | ")[0].Split(" ")
        .Where(num => num.Length > 0)
        .Select(int.Parse);
    var numbers = card
        .Split(" | ")[1].Split(" ")
        .Where(num => num.Length > 0)
        .Select(int.Parse);
    foreach (var num in numbers)
    {
        foreach (var s in winning)
        {
            if (num != s) continue;
            if (isDouble)
            {
                sum *= 2;
            }
            else
            {
                sum++;
                isDouble = true;
            }
        }
    }

    totalPoints.Add(sum);
});

Console.WriteLine(totalPoints.Sum());