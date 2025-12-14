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

        #region Game Info Variables
            public static int totalPeople = DefaultVariables.startingPopulation;
            public static int totalPeopleThatDied = 0;

            public static List<float> percentageOfPeopleThatDiedInEveryYear = new List<float>();
        #endregion

        public static void WriteGameState(bool lastReport)
        {
            if (!lastReport)
            {
                BonusPrintMethods.Space();
                Debug.Log(DefaultTexts.summaryText);
                BonusPrintMethods.Space();
                Debug.Log($"Its year {currentYear}");
                Debug.Log($"{amountOfPeopleThatStarved} people starved and {imigration} people arrived");
                if (amountOfPeopleThatDiedFromPlagueDuringYear > 0)
                {
                    Debug.Log($"{amountOfPeopleThatDiedFromPlagueDuringYear} people died from plague this year.");
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
            else if (amountOfPeopleThatStarved > currentPopulation / 100 * DefaultVariables.percentageOfPeopleNeededToBeKilledInOneTurnToLose)
            {
                Debug.Log($"You starved a {amountOfPeopleThatStarved} in one year!!!");
                BonusPrintMethods.Space();
                Debug.Log(DefaultTexts.starvedManyPeopleInAYearText);
            }
            else
            {
                Debug.Log($"In your {DefaultVariables.lastPlayableYear}-year term of office, {totalPeopleThatDied / totalPeople * 100}% of the population starved per year on the average, I. E. a total of {totalPeopleThatDied} peoople died!");
                Debug.Log($"You started with {DefaultVariables.startingLand / DefaultVariables.startingPopulation} acres per person and ended with {landOwned / currentPopulation} acres per person.");

                float rich = landOwned / currentPopulation;
                float hunger = 0;

                foreach(float hungerValue in percentageOfPeopleThatDiedInEveryYear)
                {
                    hunger += hungerValue;
                }

                hunger /= percentageOfPeopleThatDiedInEveryYear.Count;

                foreach (StatusStruct statusStruct in DefaultTexts.StatusList)
                {
                    if (rich <= statusStruct.maxRich || hunger >= statusStruct.minHunger)
                    {
                        Debug.Log(statusStruct.message);
                    }
                }
            }
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
            Debug.Log($"You now have {bushels} bushels");
        }

        public static void PrintOwnedLand(int ownedLand)
        {
            Debug.Log($"The city now owns {ownedLand} acres of land");
        }

        public static void PrintHeader(string text)
        {
            Space();
            Debug.Log(text);
            Space();
        }
    }
}
