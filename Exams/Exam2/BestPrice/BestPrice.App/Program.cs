
using BestPrice.Logic;

var command = args.Length >= 1 ? args[0] : "";
var logic = new BestPriceLogic();
switch (command)
{
    // HAU: ✅ awesome usage of pattern matching (when)
    case "import" when args.Length >= 2:
        await logic.ImportDataFromFileAsync(args[1]);
        break;
    case "clear":
        await logic.DeleteAllAsync();
        break;
    case "calculate" when args.Length == 2:
        var result = await logic.CalculatePriceWithoutDiscountAsync(args[1]);
        Console.WriteLine(result);
        break;
    default:
        Console.WriteLine("Unvalid command. Usage: dotnet run import {filename.json} [by Emanuel Roberto Sandu]");
        break;
}