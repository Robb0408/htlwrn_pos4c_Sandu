using System.Text.RegularExpressions;
using AdventOfCode7_2;

var content = await File.ReadAllLinesAsync("poker.txt");

var hands = content.Select(line =>
{
    Hand hand = new();
    var parts = line.Split(" ");
    var cards = parts[0].ToCharArray();
    hand.Cards = parts[0];
    hand.Bid = int.Parse(parts[1]);
    if (cards[0] == 'J')
    {
        // If first card is joker, replace with highest card in hand
        var max = cards.Max(c => c switch
        {
            'A' => 13,
            'K' => 12,
            'Q' => 11,
            'T' => 10,
            'J' => 1,
            _ => int.Parse(c.ToString())
        });
        switch (max)
        {
            case 10:
                parts[0] = parts[0].Replace('J', 'T');
                break;
            case 11:
                parts[0] = parts[0].Replace('J', 'Q');
                break;
            case 12:
                parts[0] = parts[0].Replace('J', 'K');
                break;
            case 13:
                parts[0] = parts[0].Replace('J', 'A');
                break;
        }
        Console.WriteLine($"Replacing J with {max}");
    }
    
    if (Regex.IsMatch(parts[0], @"^(\w)(\1|J){4}$"))
    {
        hand.Rank = 7;
    }
    else if (Regex.IsMatch(parts[0], @"^.*(\w)(.*(\1|J).*){3}$"))
    {
        hand.Rank = 6;
    }
    else if (Regex.IsMatch(parts[0], @"^.*(\w)(.*(\1|J).*){2}$"))
    {
        var foundTriplet = ' ';
        foreach (var card in cards)
        {
            var result = cards.Count(c => c == card || c == 'J');
            if (result == 3)
            {
                foundTriplet = card;
            }
        }


        var replace = parts[0].Replace(foundTriplet, '-');
        replace = replace.Replace('J', '-');
        hand.Rank = Regex.IsMatch(replace, @"^.*(\w)(.*(\1|J).*)$") ? 5 : 4;
    }
    else if (Regex.IsMatch(parts[0], @"^.*(\w)(.*(\1|J).*)$"))
    {
        var foundPair = ' ';
        foreach (var card in cards)
        {
            var result = cards.Count(c => c == card || c == 'J');
            if (result == 2)
            {
                foundPair = card;
            }
        }


        var replace = parts[0].Replace(foundPair, '-');
        replace = replace.Replace('J', '-');
        hand.Rank = Regex.IsMatch(replace, @"^.*(\w)(.*(\1|J).*)$") ? 3 : 2;
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
var ranker = 1L;
var result = orderedHands.Select(hand =>
{
    var res = hand.Bid * ranker;
    ranker++;
    return res;
}).ToList();
var count = 1;
foreach (var hand in orderedHands)
{
    Console.WriteLine(count + ": " + hand.Cards.PadLeft(20) + hand.Bid.ToString().PadLeft(20) + $" x {count} -> {result[count - 1].ToString(),20}" + hand.Rank.ToString().PadLeft(20));
    count++;
}

Console.WriteLine(result.Sum());
//251824095