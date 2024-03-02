using System.Text.RegularExpressions;
using CollatzConjecture.Logic;

var number = args.Length > 0 ? args[0] : "";

var service = new CollatzConjectureServiceImpl();
if (int.TryParse(number, out var numberInt))
{
    var sequence = service.GetSequence(numberInt);
    if (args.Contains("--sequence"))
    {
        Console.WriteLine($"The sequence for {numberInt} is: {string.Join(", ", sequence)}");    
    }
    Console.WriteLine($"The sequence is {(service.IsSequenceValid(numberInt) ? "valid" : "invalid")}");
}
else
{
    var sequence = service.GetSequence(number);
    if (args.Contains("--sequence"))
    {
        Console.WriteLine($"The sequence for {number} is: {string.Join(", ", sequence)}");    
    }
    Console.WriteLine($"The sequence is {(service.IsSequenceValid(number) ? "valid" : "invalid")}");
}