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
            if (amountOfPeopleThatDiedFromPlagueDuringYear > 0)
            {
                Debug.Log("DefaultTexts.peopleDiedFromPlagueText + amountOfPeopleThatDiedFromPlagueDuringYear + DefaultTexts.peopleStarvedText");
            }
            Debug.Log($"Population is now {currentPopulation}");
            BonusPrintMethods.PrintOwnedLand(landOwned);
            Debug.Log($"You harvested {bushelsPerAcre} bushels per acre of land");
            Debug.Log($"Rats ate {bushelshEatenByRats} bushels");
            BonusPrintMethods.Printbushels(bushels);

            BonusPrintMethods.Space();
            Debug.Log($"One acre of land costs {landPrice}");
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
            Debug.Log($"The city now owns {ownedLand} acres of land");
        }

        public static void PrintHeader(string text)
        {
            BonusPrintMethods.Space();
            Debug.Log(text);
            BonusPrintMethods.Space();
        }
    }
}
