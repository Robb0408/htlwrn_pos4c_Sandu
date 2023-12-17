namespace AdventOfCode8_2;

public class Node
{
    public string Name { get; init; } = null!;
    public string Left { get; init; } = null!;
    public string Right { get; init; } = null!;

    public override string ToString()
    {
        return $"{Name} -> L:{Left}  R:{Right}";
    }
}