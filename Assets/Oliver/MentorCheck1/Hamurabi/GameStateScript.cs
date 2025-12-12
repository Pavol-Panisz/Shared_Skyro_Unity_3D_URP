namespace Hamurabi
{
    internal class GameState
    {
        #region Game Variables
            public static int landPrice = DefaultVariables.startingLandPrice;
            public static int currentYear = DefaultVariables.startingYear;
            public static int bushelsPerAcre = DefaultVariables.startingBushelsPerAcre;
            public static int imigration = 0;

            public static int amountOfPeopleThatStarved = 0;
            public static int amountOfPeopleThatDiedFromPlagueDuringYear = 0;

            public static int bushelshEatenByRats = 0;

            public static bool gameEnded;
        #endregion

        #region Player Variables
            public static int landOwned = DefaultVariables.startingLand;
            public static int bushels = DefaultVariables.startingBushels;
            public static int currentPopulation = DefaultVariables.startingPopulation;
            public static int landPlanted = 0;
            public static bool boughtLand;
        #endregion

        public static void WriteGameState(bool lastReport)
        {
            BonusPrintMethods.Space();
            if (lastReport)
            {
                Debug.Log(DefaultTexts.lastSummaryText);
            }
            else
            {
                Debug.Log(DefaultTexts.summaryText);
            }
            BonusPrintMethods.Space();
            Debug.Log($"Its year {currentYear}");
            Debug.Log($"{amountOfPeopleThatStarved} people starved and {imigration} people arrived");
            Debug.Log($"Ri");
            Debug.Log($"One acre of land costs {landPrice}");


            BonusPrintMethods.Space();
            if (lastReport)
            {
                Console.WriteLine(DefaultTexts.lastSummaryText);
            }
            else
            {
                Console.WriteLine(DefaultTexts.summaryText);
            }
            BonusPrintMethods.Space();

            //WRITE BASIC INFO
            Console.WriteLine(DefaultTexts.currentYearText + currentYear);
            Console.WriteLine(imigration + DefaultTexts.populationArrivedText + " and " + amountOfPeopleThatStarved + DefaultTexts.peopleStarvedText);
            Console.WriteLine(DefaultTexts.currentPopulationText + currentPopulation);

            if (amountOfPeopleThatDiedFromPlagueDuringYear > 0)
            {
                Console.WriteLine(DefaultTexts.peopleDiedFromPlagueText + amountOfPeopleThatDiedFromPlagueDuringYear + DefaultTexts.peopleStarvedText);
            }

            BonusPrintMethods.PrintOwnedLand(landOwned);
            Console.WriteLine(DefaultTexts.bushelPerAcreText + bushelsPerAcre);
            Console.WriteLine(DefaultTexts.howMuchBushelshRatsAteText + bushelshEatenByRats + DefaultTexts.moneyName);
            BonusPrintMethods.Printbushels(bushels);

            BonusPrintMethods.Space();
            Console.WriteLine(DefaultTexts.landCostText + landPrice + DefaultTexts.moneyName);
            BonusPrintMethods.Space();
        }
    }

    internal class BonusPrintMethods
    {
        public static void Space()
        {
            Debug.Log("");
        }

        public static void Printbushels(int bushels)
        {
            Debug.Log(DefaultTexts.haveText + bushels + DefaultTexts.moneyName);
        }

        public static void PrintOwnedLand(int ownedLand)
        {
            Debug.Log(DefaultTexts.ownText + ownedLand + DefaultTexts.landName);
        }

        public static void PrintHeader(string text)
        {
            BonusPrintMethods.Space();
            Debug.Log(text);
            BonusPrintMethods.Space();
        }
    }
}
