var command = args.Length > 0 ? args[0] : "";
OrderImport.Logic.OrderImport orderImport = new();
switch (command)
{
    case "import" when args.Length == 3:
        try
        {
            await orderImport.ImportAsync(args[1], args[2]);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("The given file could not be found");
        }
        catch (IOException)
        {
            Console.WriteLine("An error occured while trying to read the files");
        }
        catch (Exception)
        {
            Console.WriteLine("An unexpected error occured");
        }
        break;
    case "clean":
        await orderImport.CleanAsync();
        break;
    case "check":
        await orderImport.CheckAsync();
        break;
    case "full" when args.Length == 3:
        await orderImport.CleanAsync();
        try
        {
            await orderImport.ImportAsync(args[1], args[2]);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("The given file could not be found");
            return;
        }
        catch (IOException)
        {
            Console.WriteLine("An error occured while trying to read the files");
            return;
        }
        catch (Exception)
        {
            Console.WriteLine("An unexpected error occured");
            return;
        }
        await orderImport.CheckAsync();
        break;
    default:
        orderImport.ShowHelp();
        break;
}