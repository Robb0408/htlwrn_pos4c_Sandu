using System.Text.RegularExpressions;
using AdventOfCode7_1;

var content = await File.ReadAllLinesAsync("poker.txt");

var hands = content.Select(line =>
{
    Hand hand = new();
    var parts = line.Split(" ");
    var cards = parts[0].ToCharArray();
    hand.Cards = parts[0];
    hand.Bid = int.Parse(parts[1]);
    
    if (Regex.IsMatch(parts[0], @"^(\w)\1{4}$"))
    {
        hand.Rank = 7;
    }
    else if (Regex.IsMatch(parts[0], @"^.*(\w)(.*\1.*){3}$"))
    {
        hand.Rank = 6;
    }
    else if (Regex.IsMatch(parts[0], @"^.*(\w)(.*\1.*){2}$"))
    {
        var foundTriplet = ' ';
        foreach (var card in cards)
        {
            var result = cards.Count(c => c == card);
            if (result == 3)
            {
                foundTriplet = card;
            }
        }


        var replace = parts[0].Replace(foundTriplet, '-');
        hand.Rank = Regex.IsMatch(replace, @"^.*(\w)(.*\1.*)$") ? 5 : 4;
    }
    else if (Regex.IsMatch(parts[0], @"^.*(\w)(.*\1.*)$"))
    {
        var foundPair = ' ';
        foreach (var card in cards)
        {
            var result = cards.Count(c => c == card);
            if (result == 2)
            {
                foundPair = card;
            }
        }


        var replace = parts[0].Replace(foundPair, '-');
        hand.Rank = Regex.IsMatch(replace, @"^.*(\w)(.*\1.*)$") ? 3 : 2;
    }
    else
    {
        hand.Rank = 1;
    }

    return hand;
}).ToList();



var orderedHands = hands
    .OrderBy(hand => hand.Rank).ToList();

orderedHands.Sort((hand1, hand2) => hand1.CompareTo(hand2));
foreach (var hand in orderedHands)
{
    Console.WriteLine(hand.Cards.PadLeft(20) + hand.Bid.ToString().PadLeft(20) + hand.Rank.ToString().PadLeft(20));
}
var ranker = 1;
var result = orderedHands.Select(hand =>
{
    var res = hand.Bid * ranker;
    ranker++;
    return res;
});

Console.WriteLine(result.Sum());
