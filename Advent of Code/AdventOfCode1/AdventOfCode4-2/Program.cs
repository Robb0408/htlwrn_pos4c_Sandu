//sample line:
//Card   1: 69 72 87 33 61 15  8 78 43 50 | 96 33 86 53 15 82 50 85 61  8 98 72 43 63 45 78 87 69 10 34 73 88 65 27 19

using AdventOfCode4_2;

var lines = await File.ReadAllLinesAsync("cards.txt");
var originalCards = lines.Select(line =>
{
    var split = line.Split(": ");
    var cardNum = int.Parse(split[0][5..]);
    var winning = split[1]
        .Split(" | ")[0]
        .Split(" ")
        .Where(num => num.Length > 0)
        .Select(int.Parse)
        .ToList();
    var numbers = split[1]
        .Split(" | ")[1]
        .Split(" ")
        .Where(num => num.Length > 0)
        .Select(int.Parse)
        .ToList();
    return new Card { Number = cardNum, WinningNumbers = winning, Numbers = numbers };
}).ToList();

var cards = originalCards.Select(card => card).ToList();
for (var i = 0; i < cards.Count; i++)
{
    var originalIndex = originalCards.IndexOf(cards[i]);
    cards[i].WinningNumbers.ForEach(num =>
    {
        if (!cards[i].Numbers.Contains(num)) return;
        //Console.WriteLine($"Card {card.Number} has a winning number {num} in its numbers");
        cards.Add(originalCards[originalIndex + 1]);
        //Console.WriteLine($"Inserted card {cards[i + 1].Number} after card {card.Number}");
        originalIndex++;
    });
}

//cards.ForEach(Console.WriteLine);

var groupedCards = cards.GroupBy(card => card.Number).ToList();
// foreach (var groupedCard in groupedCards)
// {
//     Console.WriteLine($"Card {groupedCard.Key} exists {groupedCard.Count()} time(s)");
//     foreach (var card in groupedCard)
//     {
//         Console.WriteLine("\t" + card);
//     }
// }

Console.WriteLine($"Total cards: {groupedCards.Sum(group => group.Count())}");