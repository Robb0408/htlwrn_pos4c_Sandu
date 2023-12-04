using AdventOfCode2_1;

var games = await File.ReadAllLinesAsync("games.txt");
var count = 0;
List<int> possibleGameIds = new();

foreach (var game in games)
{
    count++;
    bool isPossible = true;
    var gameContent = game.Split(": ")[1];
    var rounds = gameContent.Split("; ");
    foreach (var round in rounds)
    {
        var cubes = round.Split(", ");
        List<Cube> cubeList = (from cube in cubes select cube.Split(" ") into details let amount = int.Parse(details[0]) let color = details[1] select new Cube { Amount = amount, Color = color }).ToList();

        foreach (var _ in cubeList.Where(cube => cube is { Color: "red", Amount: > 12 } 
                     or { Color: "green", Amount: > 13 } 
                     or { Color: "blue", Amount: > 14 }))
        {
            isPossible = false;
        }
    }
    if (isPossible)
    {
        possibleGameIds.Add(count);
    }
}

foreach (var possibleGameId in possibleGameIds)
{
    Console.WriteLine(possibleGameId);
}

Console.WriteLine(possibleGameIds.Sum());
//12 red cubes, 13 green cubes, and 14 blue cubes