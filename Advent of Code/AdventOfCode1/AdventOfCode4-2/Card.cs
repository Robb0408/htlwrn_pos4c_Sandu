namespace AdventOfCode4_2;

public class Card
{
    public int Number { get; set; }
    public List<int> WinningNumbers { get; set; } = [];
    public List<int> Numbers { get; set; } = [];

    public override string ToString()
    {
        return $"Card {Number}: {string.Join(",", WinningNumbers)} - {string.Join(",", Numbers)}";
    }
}