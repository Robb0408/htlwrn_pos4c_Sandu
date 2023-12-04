using AdventOfCode2_2;

var games = await File.ReadAllLinesAsync("games.txt");
List<int> minCubes = new();

foreach (var game in games)
{
    var gameContent = game.Split(": ")[1];
    var rounds = gameContent.Replace("; ", ", ");
    var powSum = 1;

    var cubes = rounds.Split(", ");
    List<Cube> cubeList = new();
    foreach (var cube in cubes)
    {
        var details = cube.Split(" ");
        var amount = int.Parse(details[0]);
        var color = details[1];
        cubeList.Add(new Cube { Amount = amount, Color = color });
    }

    var result = cubeList
        .GroupBy(cube => cube.Color)
        .ToDictionary(
            group => group.Key,
            group => group.Max(c => c.Amount)
        );

    foreach (var item in result.Values)
    {
        powSum *= item;
    }

    minCubes.Add(powSum);
}

Console.WriteLine(minCubes.Sum());