namespace AdventOfCode7_2;

public class Hand : IComparable<Hand>
{
    public string Cards { get; set; } = null!;
    public int Bid { get; set; }
    public int Rank { get; set; }
    
    public int CompareTo(Hand? other)
    {
        if (other is null) return 1;
        if (Rank != other.Rank) return Rank.CompareTo(other.Rank);
        var cardsValues = GetNumeric(Cards).ToList();
        var otherCardsValues = GetNumeric(other.Cards).ToList();

        var result = 0;
        for (var i = 0; i < cardsValues.Count; i++)
        {
            var card = cardsValues[i];
            var otherCard = otherCardsValues[i];
            if (card > otherCard)
            {
                result = 1;
                break;
            }

            if (card == otherCard) continue;
            result = -1;
            break;
        }

        return result;
    }

    private IEnumerable<int> GetNumeric(string cards) =>
        cards.Select(c => c switch
        {
            'A' => 13,
            'K' => 12,
            'Q' => 11,
            'T' => 10,
            'J' => 1,
            _ => int.Parse(c.ToString())
        });
}