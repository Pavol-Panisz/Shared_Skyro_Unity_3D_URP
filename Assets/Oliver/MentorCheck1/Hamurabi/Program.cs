public class DefaultValues
{
    //DEFAULT VALUES
    public const int startingLand = 1000;
    public const int landPrice = 27;
    public const int startingBushels = 3000;
    public const int startingYear = 0;
    public const int maxYear = 10;
    public const int startingPopulation = 100;
    public const int startingBushelsPerAcre = 3;
    public const int bushelsPerSeed = 2;
    public const int minimumBushelsPerYearPerPerson = 20;
    public const int plaguePropability = 15; 

    public static readonly MinMaxValueInt32 minMaxLandPrice = new MinMaxValueInt32(17, 27);
    public static readonly MinMaxValueInt32 minMaxBushelshPerAcre = new MinMaxValueInt32(1, 5);

    public static readonly List<RatInfoStruct> ratInfoList = new List<RatInfoStruct> {new RatInfoStruct(10, 0), new RatInfoStruct(10, 25), new RatInfoStruct(10, 50), new RatInfoStruct(10, 100)};

    //LOSE VALUES
    public const int percentageOfPeopleNeededToBeKilledInOneTurnToLose = 33;

    //GAME CURRENCY NAMES
    public const string moneyName = " bushels";
    public const string landName = " land";

    //LAND TEXT
    public const string buyLandQuestion = "How much land do you want to buy?";
    public const string sellLandQuestion = "How much land do you want to sell?";
    public const string landCostText = "Land costs ";

    //PLANT
    public const string plantSeedsQuestion = "How many seeds do you want to plant? ";
    public const string seedsPerAcreText = " beshels per seed";

    //MOUSE
    public const string howMuchBushelshRatsAteText = "Rats ate ";

    //GROW
    public const string bushelPerAcre = "Bushels per acre ";
    
    //POPULATION
    public const string currentPopulationText = "Current population is ";
    public const string populationAddedText = " new people arrived";
    public const string feedPopulationQuestion = "How many bushels do you wish to feed your people? ";
    public const string peopleDiedFromPlagueText = "Plague has occured in your kingdom and ";

    //GENERAL TEXT
    public const string ownText = "You own ";
    public const string haveText = "You have ";
    public const string currentYearPrefix = "It is year ";
    public const string perPersonText = " per person";
    public const string peopleDiedText = " people died.";

    //BUY
    public const string canBuyText = "You can buy ";
    public const string notEnoughMoneyText = "You don't have enough money!!!";
    public const string notEnoughLandText = "You don't have enough land!!!";
    
    //SELL
    public const string canSellText = "You can sell ";

    //SUMMARY TEXT
    public const string summaryText = "========SUMMARY========";
    public const string lastSummaryText = "========GAME ENDED========";

    #region Randomize Methods
    public static int ReturnRandomFloat()
    {
        Random rnd = new Random();
        return rnd.Next(minMaxLandPrice.min, minMaxLandPrice.max + 1);
    }
    #endregion
}

public class MainProgram
{
    #region Game Variables
    static int landPrice = DefaultValues.landPrice;

    static int currentYear = DefaultValues.startingYear;
    static int bushelsPerAcre = DefaultValues.startingBushelsPerAcre;
    static int imigration;

    static int peopleToDie = 0;
    static int peopleToDieFromPlague = 0;

    static int bushelshEatenByRats = 0;

    static bool gameEnded;
    #endregion

    #region Player Variables
    static int ownedLand = DefaultValues.startingLand;
    static int bushels = DefaultValues.startingBushels;

    static int currentPopulation = DefaultValues.startingPopulation;
    static int landPlanted = 0;

    static bool boughtLand;
    #endregion

    public static void Main()
    {
        MainGame();
        //SimulatePrices.SimulateLandPrices(100, 10);
    }

    private static void MainGame()
    {
        boughtLand = false;

        if (currentYear != 0)
        {
            GrowBushels();
            AddPopulation();
            KillPeople();
            Rats();
        }

        if (gameEnded) return;

        Report();

        TryToBuyLand();
        if (!boughtLand)
        {
            TryToSellLand();
        }
        TryToFeedPeople();
        TryToPlantLand();

        if (PlagueOccured() && currentYear != 0)
        {
            peopleToDieFromPlague = currentPopulation / 2;
        }
        else
        {
            peopleToDieFromPlague = 0;
        }

        EndYear();
    }

    private static void Rats()
    {
        for (int i = 0; i < DefaultValues.ratInfoList.Count; i++)
        {
            if (ReturnRandomInt(0, 100) <= DefaultValues.ratInfoList[i].chance)
            {
                bushelshEatenByRats = (int)(bushels / 100 * DefaultValues.ratInfoList[i].percentageOfStolen);
                bushels -= bushelshEatenByRats;
                bushels = Math.Clamp(bushels, 0, int.MaxValue);

                return;
            }
        }

        bushelshEatenByRats = 0;
    }

    private static void GrowBushels()
    {
        bushels += bushelsPerAcre * landPlanted;
    }

    private static void TryToPlantLand()
    {
        Console.WriteLine(DefaultValues.plantSeedsQuestion + " (" + DefaultValues.bushelsPerSeed + DefaultValues.seedsPerAcreText + ")");

        int.TryParse(Console.ReadLine(), out int parseResult);
        int amountOfSeedsToPlant = parseResult;

        if (amountOfSeedsToPlant * 2 > bushels)
        {
            Console.WriteLine(DefaultValues.notEnoughMoneyText);
            TryToPlantLand();
        }
        else
        {
            bushels -= amountOfSeedsToPlant * 2;
            landPlanted = amountOfSeedsToPlant;
        }
    }

    private static void KillPeople()
    {
        if (peopleToDie > currentPopulation / 100 * DefaultValues.percentageOfPeopleNeededToBeKilledInOneTurnToLose)
        {
            Lose();
        }

        currentPopulation -= peopleToDie;
        currentPopulation -= peopleToDieFromPlague;
    }

    private static void Lose()
    {
        //LOSE
        gameEnded = true;
    }

    private static void TryToFeedPeople()
    {
        Console.WriteLine(DefaultValues.feedPopulationQuestion + " (" + DefaultValues.minimumBushelsPerYearPerPerson + DefaultValues.perPersonText + ")");

        int.TryParse(Console.ReadLine(), out int parseResult);
        int amountOfBushelsToFeedPeople = parseResult;

        if (amountOfBushelsToFeedPeople > bushels)
        {
            Console.WriteLine(DefaultValues.notEnoughMoneyText);
            TryToFeedPeople();
        }
        else
        {
            bushels -= amountOfBushelsToFeedPeople;
            int bushelsNeeded = currentPopulation * DefaultValues.minimumBushelsPerYearPerPerson;
            peopleToDie = (int)((bushelsNeeded - amountOfBushelsToFeedPeople) / DefaultValues.minimumBushelsPerYearPerPerson);
        }
    }

    private static void AddPopulation()
    {
        imigration = (int)(bushelsPerAcre * (20 * ownedLand + bushels) / currentPopulation / 100 + 1);
        currentPopulation += imigration;
    }

    private static void RandomizeNumbers()
    {
        landPrice = ReturnRandomLandPrice();

        bushelsPerAcre =ReturnRandomBushelsPerAcre();
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
        Report(true);
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
        Console.WriteLine(imigration + DefaultValues.populationAddedText + " and " + peopleToDie + DefaultValues.peopleDiedText);
        Console.WriteLine(DefaultValues.currentPopulationText + currentPopulation);

        if (peopleToDieFromPlague > 0)
        {
            Console.WriteLine(DefaultValues.peopleDiedFromPlagueText + peopleToDieFromPlague + DefaultValues.peopleDiedText);
        }

        BonusMethods.PrintOwnedLand(ownedLand);
        Console.WriteLine(DefaultValues.bushelPerAcre + bushelsPerAcre);
        Console.WriteLine(DefaultValues.howMuchBushelshRatsAteText + bushelshEatenByRats + DefaultValues.moneyName);
        BonusMethods.Printbushels(bushels);

        BonusMethods.Space();
        Console.WriteLine(DefaultValues.landCostText + landPrice + DefaultValues.moneyName);
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
            if (amountOfLandToBuy > 0)
            {
                bushels -= amountOfLandToBuy * landPrice;
                ownedLand += amountOfLandToBuy;

                BonusMethods.Printbushels(bushels);

                boughtLand = true;
            }
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

    public static int ReturnRandomLandPrice()
    {
        Random rnd = new Random();
        return rnd.Next(DefaultValues.minMaxLandPrice.min, DefaultValues.minMaxLandPrice.max + 1);
    }

    public static int ReturnRandomBushelsPerAcre()
    {
        Random rnd = new Random();
        return rnd.Next(DefaultValues.minMaxBushelshPerAcre.min, DefaultValues.minMaxBushelshPerAcre.max + 1);
    }

    public static int ReturnRandomInt(int min, int max)
    {
        Random rnd = new Random();
        return rnd.Next(min, max);
    }

    private static bool PlagueOccured()
    {
        return (ReturnRandomInt(0, 100) > DefaultValues.plaguePropability)? false : true;
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

public struct RatInfoStruct
{
    public int chance;
    public int percentageOfStolen;

    public RatInfoStruct(int chance, int percentageOfStolen)
    {
        this.chance = chance;
        this.percentageOfStolen = percentageOfStolen;
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
        landPrice = MainProgram.ReturnRandomLandPrice();

        landPrices.Add(landPrice);
        Console.WriteLine(landPrice);
    }
}