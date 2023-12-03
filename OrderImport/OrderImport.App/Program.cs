var command = args[0];
switch (command)
{
    case "import":
        Console.WriteLine("a");
        break;
    case "clean":
        Console.WriteLine("b");
        break;
    case "check":
        Console.WriteLine("c");
        break;
    case "full":
        Console.WriteLine("d");
        break;
    default:
        Console.WriteLine("e");
        break;
}