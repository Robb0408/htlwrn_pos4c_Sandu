using System.Text;

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
lineList = lineList.Select(line => "." + line + ".").ToList();

sb.Clear();
List<int> validNumbers = new();

// line-iteration
for (var i = 1; i < lineList.Count - 1; i++)
{
    bool isValid = false;

    // character-iteration
    for (var j = 1; j < lineList[i].Length; j++)
    {

        if (char.IsDigit(lineList[i][j]))
        {
            sb.Append(lineList[i][j]);
            if ((lineList[i][j - 1] != '.' && !char.IsDigit(lineList[i][j - 1])) ||
                (lineList[i][j + 1] != '.' && !char.IsDigit(lineList[i][j + 1])) ||
                lineList[i - 1][j - 1] != '.' ||
                lineList[i - 1][j] != '.' ||
                lineList[i - 1][j + 1] != '.' ||
                lineList[i + 1][j - 1] != '.' ||
                lineList[i + 1][j] != '.' ||
                lineList[i + 1][j + 1] != '.')
            {
                isValid = true;
            }
        }
        else
        {
            if (sb.Length > 0 && isValid)
            {
                validNumbers.Add(int.Parse(sb.ToString()));
            }
            sb.Clear();
            isValid = false;
        }
    }
}

var count = 0;
validNumbers.ForEach(num =>
{
    Console.WriteLine($"{count,4}: {num}");
    count++;
});

Console.WriteLine(validNumbers.Sum());