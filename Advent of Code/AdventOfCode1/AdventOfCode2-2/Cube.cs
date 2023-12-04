namespace AdventOfCode2_2;

public class Cube
{
    public int Amount { get; set; }
    public string Color { get; set; } = null!;

    public void IncreaseAmount(int addAmount)
    {
        Amount += addAmount;
    }
}