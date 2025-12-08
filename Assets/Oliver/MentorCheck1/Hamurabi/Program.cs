public class DefaultValues
{
    //DEFAULT VALUES
    public const int startingLand = 100;
    public const int landPrice = 20;
    public const int startingBushels = 1000;
    public const int startingYear = 0;
    public const int maxYear = 10;

    //GAME CURRENCY NAMES
    public const string moneyName = " bushels";
    public const string landName = " land";

    //LAND TEXT
    public const string buyLandQuestion = "How much land do you want to buy?";
    public const string landCostText = "Land costs ";

    //GENERAL TEXT
    public const string ownText = "You own ";
    public const string haveText = "You have ";
    public const string currentYearPrefix = "It is year ";
    public const string notEnoughMoneyText = "You don't have enough money!!!";
    public const string canBuyText = "You can buy ";

    //SUMMARY TEXT
    public const string summaryText = "Summary";
}

public class MainProgram
{
#region Game Variables
    static int landPrice = DefaultValues.landPrice;
    
    static int currentYear = DefaultValues.startingYear;
#endregion

#region Player Variables
    static int ownedLand = DefaultValues.startingLand;
    static int bushels = DefaultValues.startingBushels;
#endregion

    public static void Main()
    {
        MainGame();
    }

    public static void MainGame()
    {
        Report();

        TryToBuyLand();
    
        EndYear();
    }

    public static void EndYear()
    {
        if (currentYear > DefaultValues.maxYear)
        {
            EndGame();    
        }
        else
        {
            currentYear++;
            MainGame();
        }
    }

    public static void EndGame()
    {
        Report();
    }

    public static void Report()
    {
        //WRITE BASIC INFO
        BonusMethods.PrintHeader(DefaultValues.summaryText);

        Console.WriteLine(DefaultValues.currentYearPrefix + currentYear);
        BonusMethods.PrintOwnedLand(ownedLand);
        Console.WriteLine(DefaultValues.landCostText + landPrice + DefaultValues.moneyName);
        BonusMethods.Printbushels(bushels);

        BonusMethods.Space();
    }

    public static void TryToBuyLand()
    {
        int amountThatPlayerCanBuy = bushels / landPrice;
        Console.WriteLine(DefaultValues.buyLandQuestion);
        BonusMethods.PrintInParentheses(DefaultValues.canBuyText + " " + amountThatPlayerCanBuy.ToString());

        int amountOfLandToBuy = int.Parse(Console.ReadLine());
        
        if (amountOfLandToBuy * landPrice > bushels) {
            Console.WriteLine(DefaultValues.notEnoughMoneyText);    
            TryToBuyLand();
        }
        else
        {
            bushels -= amountOfLandToBuy * landPrice;
            ownedLand += amountOfLandToBuy;

            BonusMethods.Printbushels(bushels);
            BonusMethods.PrintOwnedLand(ownedLand);
        }
    }
}

public class BonusMethods
{
    public static void Space()
    {
        Console.WriteLine("");
    }

    public static void Printbushels(int bushels)
    {
        Console.WriteLine(DefaultValues.haveText + bushels + DefaultValues.moneyName);
    }

    public static void PrintOwnedLand(int ownedLand)
    {
        Console.WriteLine(DefaultValues.ownText + ownedLand + DefaultValues.landName);
    }

    public static void PrintHeader(string text)
    {
        Space();
        Console.WriteLine(text);
        Space();
    }

    public static void PrintInParentheses(string text, bool writeOnNewLine = false)
    {
        if (writeOnNewLine)
        {
            Console.WriteLine(" (" + text + ")");
        }
        else
        {
            Console.Write(" (" + text + ")");
        }
    }
}