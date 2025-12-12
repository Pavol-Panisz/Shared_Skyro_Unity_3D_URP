namespace Hamurabi
{
    internal class DefaultVariables
    {
        //STARTING VARIABLES
        public const int startingLand = 1000;
        public const int startingLandPrice = 27;
        public const int startingBushels = 3000;
        public const int startingYear = 1;
        public const int startingPopulation = 100;
        public const int startingBushelsPerAcre = 3;

        //DEFAULT VARIABLES
        public const int lastPlayableYear = 10;
        public const int bushelsPerSeed = 2;
        public const int minimumBushelsPerYearPerPerson = 20;
        public const int plaguePropability = 15;

        //MIN MAX VARIABLES
        public static readonly MinMaxValueInt32 minMaxLandPrice = new MinMaxValueInt32(17, 27);
        public static readonly MinMaxValueInt32 minMaxBushelshPerAcre = new MinMaxValueInt32(1, 5);

        //RAT INFO
        public static readonly List<RatInfoStruct> ratInfoList = new List<RatInfoStruct> { new RatInfoStruct(10, 0), new RatInfoStruct(10, 25), new RatInfoStruct(10, 50), new RatInfoStruct(10, 100) };

        //LOSE VARIABLES
        public const int percentageOfPeopleNeededToBeKilledInOneTurnToLose = 33;
    }

    internal class DefaultTexts
    {
        //GAME CURRENCY NAMES
        public const string moneyName = " bushels";
        public const string landName = " land";

        //LAND TEXT
        public const string buyLandQuestion = "How much land do you want to buy?";
        public const string sellLandQuestion = "How much land do you want to sell?";
        public const string landCostText = "Land costs {0}";

        //PLANT
        public const string plantSeedsQuestion = "How many seeds do you want to plant? ";
        public const string bushelsPerSeedText = " bushels per seed";

        //RATS
        public const string howMuchBushelshRatsAteText = "Rats ate ";

        //GROW
        public const string bushelPerAcreText = "Bushels per acre ";

        //PLAGUE
        public const string peopleDiedFromPlagueText = "Plague has occured in your kingdom and ";

        //POPULATION
        public const string currentPopulationText = "Current population is ";
        public const string populationArrivedText = " new people arrived";
        public const string feedPopulationQuestion = "How many bushels do you wish to feed your people? ";

        //GENERAL TEXT
        public const string ownText = "You now own ";
        public const string haveText = "You now have ";
        public const string currentYearText = "It is year ";
        public const string perPersonText = " per person";
        public const string peopleStarvedText = " people starved.";

        //BUY
        public const string canBuyText = "You can buy ";
        public const string notEnoughMoneyText = "You don't have enough money!!!";
        public const string notEnoughLandText = "You don't have enough land!!!";

        //SELL
        public const string canSellText = "You can sell ";

        //SUMMARY TEXT
        public const string summaryText = "========SUMMARY========";
        public const string lastSummaryText = "========GAME ENDED========";
    }
}