namespace GuessingGame;

class Program
{
    private static string playerName;
    private static int secretNumber = 13;
    private static HighScore[] highScores = new HighScore[5];
    
    private static void Main()
    {
        for (int i = 0; i < 5; i++)
        {
            highScores[i] = new HighScore("placeholder", 999999, 0);
        }
        
        playerName = AskForPlayerName();
        
        for (int i = 0; i < 5; i++)
        {
            highScores[i] = Game();
            
            Console.Write("Keep playing? (y/N)?: ");
            string? input = Console.ReadLine();

            if (input == null)
            {
                break;
            }
            
            if (input.ToLower() != "y")
            {
                break;
            }
        }
        
        PrintHighScores();
    }

    private static HighScore Game()
    {
        int numberOfGuesses = 0;

        while (true)
        {
            int guess = AskForNumber();
            numberOfGuesses++;

            if (guess == secretNumber)
            {
                Console.WriteLine("Correct!");
                return new HighScore(playerName, numberOfGuesses, 0);
            }

            if (guess < secretNumber)
            {
                Console.WriteLine("Too small!");
                continue;
            }
            
            if (guess > secretNumber)
            {
                Console.WriteLine("Too big!");
                continue;
            }
        }
    }

    private static int AskForNumber()
    {
        Console.Write("Guess: ");
        int guess;
        
        while (true)
        {
            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.Write("Try again: ");
                continue;
            }

            if (!int.TryParse(input, out guess))
            {
                Console.Write("Must be a number. Try again: ");
                continue;
            }

            if (guess < 0)
            {
                Console.Write("Must be a positive number. Try again: ");
                continue;
            }

            return guess;
        }
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
    
    private static void PrintHighScores()
    {
        var sortedArray = highScores.OrderBy(i => i.guesses).ToArray();

        Console.WriteLine("Highscores:");

        int i = 1;
        foreach (var score in sortedArray)
        {
            if (score.name == "placeholder")
            {
                continue;
            }
            
            Console.WriteLine(i + ". " + score);
            i++;
        }
    }
}