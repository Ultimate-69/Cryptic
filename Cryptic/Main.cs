Console.WriteLine("Welcome to CRYPTIC!");
GiveOptions();
Console.ReadKey();

void GiveOptions()
{
    Console.WriteLine("1. Encrypt");
    Console.WriteLine("2. Decrypt");
    Console.WriteLine("3. Quit");
    HandleOptionInput();
}

void HandleOptionInput()
{
    int choice = Console.ReadKey().KeyChar - '0';
    Console.WriteLine();
    
    switch (choice)
    {
        case 3:
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Invalid option. Please choose from the above only.");
            GiveOptions();
            break;
    }
}