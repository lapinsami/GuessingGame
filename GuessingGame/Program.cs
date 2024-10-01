namespace GuessingGame;

class Program
{
    private string playerName = "player";
    
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
    
    private static string AskForPlayerName()
    {
        Console.Write("Name: ");

        while (true)
        {
            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.Write("Try again: ");
                continue;
            }

            if (input.Length < 2)
            {
                Console.Write("Name must be 2 characters or longer. Try again: ");
                continue;
            }

            return input;
        }
    }
}