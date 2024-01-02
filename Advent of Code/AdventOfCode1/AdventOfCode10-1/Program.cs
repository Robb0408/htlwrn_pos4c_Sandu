var content = await File.ReadAllLinesAsync("pipes.txt");

var lines = content.Select(lines => lines.ToCharArray().ToList()).ToList();

var startRow = 0;
var startCol = 0;
var isFound = false;
for (var i = 0; i < lines.Count; i++)
{
    for (var j = 0; j < lines[i].Count; j++)
    {
        if (lines[i][j] == 'S')
        {
            startRow = i;
            startCol = j;
            isFound = true;
            break;
        }

        startCol++;
    }
    if (isFound)
    {
        break;
    }

    startRow++;
}

Console.WriteLine("Start: " + startRow + ", " + startCol);

var endRow = -1;
var endCol = -1;

// | vertical
// - horizontal
// L top <-> right
// J top <-> left
// 7 bottom <-> left
// F bottom <-> right
// . no pipe
// S start

while (endCol == -1 && endRow == -1)
{
    
}
