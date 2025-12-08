public class DefaultValues
{
    //DEFAULT VALUES
    public const int startingLand = 100;
    public const int landPrice = 20;
    public const int startingBushels = 1000;
    public const int startingYear = 0;
    public const int maxYear = 10;

    public static readonly MinMaxValueInt32 minMaxLandPrice = new MinMaxValueInt32(17, 27);

    //GAME CURRENCY NAMES
    public const string moneyName = " bushels";
    public const string landName = " land";

    //LAND TEXT
    public const string buyLandQuestion = "How much land do you want to buy?";
    public const string sellLandQuestion = "How much land do you want to sell?";
    public const string landCostText = "Land costs ";

    //GENERAL TEXT
    public const string ownText = "You own ";
    public const string haveText = "You have ";
    public const string currentYearPrefix = "It is year ";

    //BUY
    public const string canBuyText = "You can buy ";
    public const string notEnoughMoneyText = "You don't have enough money!!!";
    public const string notEnoughLandText = "You don't have enough land!!!";
    
    //SELL
    public const string canSellText = "You can sell ";

    //SUMMARY TEXT
    public const string summaryText = "====Summary====";
    public const string lastSummaryText = "====GAME ENDED====";

    #region Randomize Methods
    public static int ReturnRandomLandPrice()
    {
        Random rnd = new Random();
        return rnd.Next(minMaxLandPrice.min, minMaxLandPrice.max);
    }
    #endregion
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
        //SimulatePrices.SimulateLandPrices(10, 10);
    }

    private static void MainGame()
    {
        Report();

        TryToBuyLand();
        TryToSellLand();
        
        EndYear();
    }

    private static void RandomizeNumbers()
    {
        Random rnd = new Random();
        landPrice = DefaultValues.ReturnRandomLandPrice();
    }

    private static void EndYear()
    {
        if (currentYear == DefaultValues.maxYear)
        {
            EndGame();
        }
        else
        {
            currentYear++;
            RandomizeNumbers();

            MainGame();
        }
    }

    private static void EndGame()
    {
        Report();
    }

    private static void Report(bool lastReport = false)
    {
        if (lastReport)
        {
            BonusMethods.PrintHeader(DefaultValues.lastSummaryText);
        }
        else
        {
            BonusMethods.PrintHeader(DefaultValues.summaryText);
        }

        //WRITE BASIC INFO
        Console.WriteLine(DefaultValues.currentYearPrefix + currentYear);
        BonusMethods.PrintOwnedLand(ownedLand);
        Console.WriteLine(DefaultValues.landCostText + landPrice + DefaultValues.moneyName);
        BonusMethods.Printbushels(bushels);

        BonusMethods.Space();
    }

    private static void TryToBuyLand()
    {
        int amountThatPlayerCanBuy = bushels / landPrice;
        Console.WriteLine(DefaultValues.buyLandQuestion + " (" + DefaultValues.canBuyText + amountThatPlayerCanBuy.ToString() + ")");

        int.TryParse(Console.ReadLine(), out int parseResult);
        int amountOfLandToBuy = parseResult;

        if (amountOfLandToBuy * landPrice > bushels)
        {
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

    private static void TryToSellLand()
    {
        int amountThatPlayerCanSell = ownedLand;
        Console.WriteLine(DefaultValues.sellLandQuestion + " (" + DefaultValues.canSellText + amountThatPlayerCanSell.ToString() + ")");

        int.TryParse(Console.ReadLine(), out int parseResult);
        int amountOfLandToSell = parseResult;

        if (amountOfLandToSell > ownedLand)
        {
            Console.WriteLine(DefaultValues.notEnoughLandText);
            TryToSellLand();
        }
        else
        {
            bushels += amountOfLandToSell * landPrice;
            ownedLand -= amountOfLandToSell;

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
}

public struct MinMaxValueInt32
{
    public int min;
    public int max;

    public MinMaxValueInt32(int min, int max)
    {
        this.min = min;
        this.max = max;
    }
}

public class SimulatePrices()
{
    static List<int> landPrices = new List<int>();
    static int landPrice;

    public static void SimulateLandPrices(int amountOfSimulations, int amountOfNumbersInSimulations)
    {
        landPrice = DefaultValues.landPrice;
        landPrices.Clear();

        for (int i = 0; i < amountOfSimulations; i++)
        {
            for (int y = 0; y < amountOfSimulations; y++)
            {
                RandomizeNumbers();
            }
        }

        float averageLandPrice = 0;

        for (int i = 0; i < landPrices.Count; i++)
        {
            averageLandPrice += landPrices[i];
        }

        averageLandPrice /= landPrices.Count;
        Console.WriteLine(averageLandPrice);
    }

    private static void RandomizeNumbers()
    {
        Random rnd = new Random();
        landPrice = DefaultValues.ReturnRandomLandPrice();

        landPrices.Add(landPrice);
        Console.WriteLine(landPrice);
    }
}