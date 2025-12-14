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
        public const int personCanPlantSeeds = 10;

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
        //NOT ENOUGH
        public const string notEnoughMoneyText = "You don't have enough money!!!";
        public const string notEnoughLandText = "You don't have enough land!!!";
        public const string notEnoughPeopleText = "You don't have enough people!!!";

        //LOSE TEXTS
        public const string starvedManyPeopleInAYearText = "DUE TO THIS EXTREME MISMANAGEMENT YYOU HAVE NOT ONLY \nBEEN IMPEACHED AND THROWN OUT OF OFFICE BUT YOU HAVE \nALSO BEEN DECLARED NATIONAL FINK!!!";

        //SUMMARY TEXT
        public const string summaryText = "========SUMMARY========";
        public const string lastSummaryText = "========GAME ENDED========";

        public static readonly List<StatusStruct> StatusList = new List<StatusStruct> {
            new StatusStruct("YOUR PERFORMACE FOR THE LAST 10 YEARS IS WORSE THAN BOBO'S \nPERFORMANCE WITH GAME DEVELOPMENT. PARENTS WILL USE YOU \nAS AN EXAMPLE ON WHAT SOMEONE WILL BECOME IF THEY WONT DO THEIR HOMEWORK.", 7, 33),
            new StatusStruct("YOU ARE AS GOOD AS VIVAT SLOVAKIA. YOU WILL BE REMEMBERED AS ONE \nOF THE WORST RULERS IN THIS COUNTRY.", 9, 10),
            new StatusStruct("YOU ARE BETTER THAN THE CURRENT GOVERMENT OF SLOVAKIA. \nCONGRATULATION, YOU WONT BE REMEMBERED.", 10, 3),
            new StatusStruct("YOUR PERFORMANCE DURING THE LAST 10 YEARS CHANGED THE WORLD \nEVEN MORE THAN RESCUE ZOO TYCOON. GOOD JOB. YOU WILL BE \nREMEMBERED FOR HUNDERDS OF YEARS.", int.MaxValue, int.MinValue)
        };
    }

    public struct StatusStruct
    {
        public string message;
        public int minHunger;
        public int maxRich;

        public StatusStruct(string message, int maxRich, int minHunger)
        {
            this.message = message;
            this.maxRich = maxRich;
            this.minHunger = minHunger;
        }
    }
}