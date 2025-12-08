public class DefaultValues
{
    //DEFAULT VALUES
    public const int startingLand = 100;
    public const int landPrice = 20;
    public const int startingBushels = 1000;
    public const int startingYear = 0;

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
        Report();

        TryToBuyLand();
    }

    public static void Report()
    {
        //WRITE BASIC INFO
        Console.WriteLine(DefaultValues.currentYearPrefix + currentYear);
        BonusMethods.PrintOwnedLand(ownedLand);
        Console.WriteLine(DefaultValues.landCostText + landPrice + DefaultValues.landName);
        BonusMethods.Printbushels(bushels);

        BonusMethods.Space();
    }

    public static void TryToBuyLand()
    {
        Console.WriteLine(DefaultValues.buyLandQuestion);

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
}