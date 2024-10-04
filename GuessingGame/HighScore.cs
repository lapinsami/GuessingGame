namespace GuessingGame;

public class HighScore(string playerName, int numberOfGuesses, int difficultyLevel)
{
    public string name = playerName;
    public int guesses = numberOfGuesses;
    private int difficulty = difficultyLevel;

    public override string ToString()
    {
        string difficultyLevelString = difficulty switch
        {
            0 => "easy",
            1 => "medium",
            _ => "hard"
        };
        
        string plural = guesses != 1 ? "es" : "";
        
        return $"{name} - {guesses} guess{plural} ({difficultyLevelString})";
    }
}