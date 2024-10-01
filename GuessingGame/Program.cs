namespace GuessingGame;

class Program
{
    private static string playerName;
    private static int secretNumber = 13;
    private static int difficulty = 0;
    private static HighScore[] highScores = new HighScore[5];
    
    private static void Main()
    {
        for (int i = 0; i < 5; i++)
        {
            highScores[i] = new HighScore("placeholder", 999999, 0);
        }
        
        for (int i = 0; i < 5; i++)
        {
            highScores[i] = Game();

            if (i >= 4)
            {
                break;
            }
            
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
        playerName = AskForPlayerName();
        difficulty = AskForDifficulty();

        Console.WriteLine("You have 30 seconds to guess");
        
        int numberOfGuesses = 0;

        secretNumber = difficulty switch
        {
            0 => Random.Shared.Next(30),
            1 => Random.Shared.Next(60),
            2 => Random.Shared.Next(120),
            _ => secretNumber
        };
        
        DateTime start = DateTime.UtcNow;
        DateTime now = DateTime.UtcNow;

        while (true)
        {
            int guess = AskForNumber();
            
            now = DateTime.UtcNow;

            if ((now - start).TotalSeconds > 30)
            {
                Console.WriteLine("Time's up!");
                return new HighScore("placeholder", 999999, 0);
            }
            
            numberOfGuesses++;

            if (guess == secretNumber)
            {
                DateTime end = DateTime.UtcNow;
                TimeSpan deltaTime = end - start;
                
                Console.WriteLine($"Correct! Took {deltaTime.TotalSeconds} seconds");
                return new HighScore(playerName, numberOfGuesses, difficulty);
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
    
    private static int AskForDifficulty()
    {
        Console.Write("Choose difficulty 1. easy (30), 2. medium (60), 3. hard (120): ");
        int diff;
        
        while (true)
        {
            string? input = Console.ReadLine();

            if (input == null)
            {
                Console.Write("Try again: ");
                continue;
            }

            if (!int.TryParse(input, out diff))
            {
                Console.Write("Must be a number. Try again: ");
                continue;
            }

            if (diff < 1 || diff > 3)
            {
                Console.Write("Must be 1, 2 or 3. Try again: ");
                continue;
            }

            return diff - 1;
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