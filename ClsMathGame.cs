
using System.Globalization;

namespace QustionsExample.Levels;

public class MathGame
{
    private int TotalQuestions { get; set; }
    private int TotalCourrctAnswer { get; set; }
    private int Scor { get; set; }
    private int TotalUnCourrctAnswer { get; set; }
    int Percentage { get; set; }
    Levels eDifficult = new Levels();
    char[] Opreators = new char[4] { '-', '+', '/', '*' };


    public MathGame()
    {
        eDifficult = Levels.Easy;
    }

    #region PrintWelcomLevel & PrintQustiond
    string PrintStartWelcomLevel(string typeLevel)
    {
        return $"Welcom To The {typeLevel} Level\nLet's begin!".ToString();
    }
    void PrintQustions(int firstNumber, char opreator, int secondNumber)
    {
        Console.WriteLine($"What is {firstNumber} {opreator} {secondNumber}\n");

    }
    private void Start()
    {
        Console.WriteLine(PrintStartWelcomLevel($"{eDifficult}"));
    }
    #endregion

    #region StartLevels & AskQustions
    public void NextLevel()
    {
        // ask user count of qustions
        Console.WriteLine("How many qustions you want");
        int nUserChoic = 0;
        ReadNumber(out nUserChoic);
        TotalQuestions = nUserChoic;
        bool bIsPlayer = true;
        string cUserCoice = string.Empty;
        while (bIsPlayer)
        {
            Console.Clear();
            Console.WriteLine(PrintStartWelcomLevel($"{eDifficult}"));
            // Creat Method qustions random levels
            Qustions(TotalQuestions);
            // ask user do you want next level◊
            bIsPlayer = TryAgeinLevel();
            // Check the Percentage
            if (Percentage >= 50)
            {
                Scor++;
                TotalCourrctAnswer = 0;
                TotalUnCourrctAnswer = 0;
            }
            if (Scor == 0)
                eDifficult = Levels.Easy;
            else if (Scor == 1)
                eDifficult = Levels.Mediam;
            else
                eDifficult = Levels.Hard;
            Console.WriteLine("How many qustions you want");
            ReadNumber(out nUserChoic);

        }
    }
    public void Qustions(int countOfQustions)
    {

        int nMainNumber = eDifficult == Levels.Easy ? 1 : eDifficult == Levels.Mediam ? 20 : 50;
        int nMaxmNumber = eDifficult == Levels.Easy ? (10 * 2) : eDifficult == Levels.Mediam ? (80 * 2) : (100 * 2);
        int nInputUsre = 0;
        double Output = -1;
        for (int i = 0; i < countOfQustions; i++)
        {
            nMainNumber = Random.Shared.Next(nMainNumber, nMaxmNumber + nMainNumber);
            nMaxmNumber = Random.Shared.Next(nMaxmNumber);

            char opreator = Opreators[Random.Shared.Next(Opreators.Length)];

            PrintQustions(nMaxmNumber, opreator, nMainNumber);
            // take result from user
            ReadNumber(out nInputUsre);
            // implement opraetor two numbers
            Output = SwitchCses(nMaxmNumber, opreator, nMainNumber);
            // ComparTo Input User result
            CheckAnswer(Output, nInputUsre);
        }
    }

    #endregion
    private int SwitchCses(int firstNumber, char opreator, int secondNumber)
    {

        switch (opreator)
        {
            case '-':
                return firstNumber - secondNumber;
            case '+':
                return firstNumber + secondNumber;
            case '/':
                return firstNumber == 0 || secondNumber == 0 ? 0 : firstNumber / secondNumber;
            case '*':
                return firstNumber * secondNumber;
        }
        return 0;
    }

    #region Read & Checks

    public void ReadNumber(out int number)
    {
        bool bIsValidate = int.TryParse(Console.ReadLine(), out number);
        if (!bIsValidate)
        {
            Console.WriteLine("Invalid Input...Pleas enter valid number");
        }
    }

    private void CheckAnswer(double result, double inputUsre)
    {
        if (result == inputUsre)
        {
            Console.WriteLine("Correct");
            TotalCourrctAnswer++;
        }
        else
        {
            Console.WriteLine($"Un Correct! The correct answer is {result}");
            TotalUnCourrctAnswer++;
        }
    }
    public bool TryAgeinLevel()
    {
        Console.WriteLine($"Congratultion! You,ve passed this level with {GetPercentage():P00} Correct answer\n");
        Console.WriteLine("Do you want to move to the next level ?(y/n)");
        string cUserCoice = Console.ReadLine();
        return cUserCoice == "y" || cUserCoice == "Yes";
    }
    public double GetPercentage()
    {
        double dResult = ((double)TotalCourrctAnswer / (double)TotalQuestions);
        Percentage = (int)(dResult * 100);
        return dResult;
    }

    #endregion

}