using System;

Random random = new Random();

int[] secretCode = new int[4];
for (int i = 0; i < 4; i++)
{
    secretCode[i] = random.Next(1, 7);
}

int attempts = 10;
bool isGuessed = false;

Console.WriteLine("Welcome to Mastermind!");
Console.WriteLine("Guess the 4-digit code. Each digit is between 1 and 6. You have 10 attempts.");

while (attempts > 0 && !isGuessed)
{
    Console.Write("Enter your guess: ");
    string userInput = Console.ReadLine();

    if (userInput.Length != 4 || !int.TryParse(userInput, out _))
    {
        Console.WriteLine("Invalid input. Please enter exactly 4 digits between 1 and 6.");
        continue;
    }

    int[] guess = new int[4];
    for (int i = 0; i < 4; i++)
    {
        guess[i] = int.Parse(userInput[i].ToString());
    }

    string hint = GenerateHint(secretCode, guess);
    Console.WriteLine("Hint: " + hint);

    if (hint == "++++")
    {
        isGuessed = true;
        Console.WriteLine("Wow! You've successfully guessed the correct code.");
    }
    else
    {
        attempts--;
        Console.WriteLine($"Attempts remaining: {attempts}");
    }
}

if (!isGuessed)
{
    Console.WriteLine("You've used all your attempts. Better luck next time!");
    Console.WriteLine($"The correct code was: {string.Join("", secretCode)}");
}

string GenerateHint(int[] secretCode, int[] guess)
{
    int correctPosition = 0;
    int correctDigit = 0;
    bool[] secretCodeUsed = new bool[4];
    bool[] guessUsed = new bool[4];

    for (int i = 0; i < 4; i++)
    {
        if (guess[i] == secretCode[i])
        {
            correctPosition++;
            secretCodeUsed[i] = true;
            guessUsed[i] = true;
        }
    }

    for (int i = 0; i < 4; i++)
    {
        if (!guessUsed[i])
        {
            for (int j = 0; j < 4; j++)
            {
                if (!secretCodeUsed[j] && guess[i] == secretCode[j])
                {
                    correctDigit++;
                    secretCodeUsed[j] = true;
                    break;
                }
            }
        }
    }

    return new string('+', correctPosition) + new string('-', correctDigit);
}
