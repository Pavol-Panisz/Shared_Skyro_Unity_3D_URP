namespace Hamurabi
{
    internal class MainProgram
    {
        private static Random rng = new Random();

        private static void Main()
        {
            MainGame();
            //SimulatePrices.SimulateLandPrices(100, 10);
        }

        private static void MainGame()
        {
            GameState.boughtLand = false;

            if (!FirstYear())
            {
                GrowBushels();
                AddPopulation();

                if (PlagueOccured())
                {
                    GameState.amountOfPeopleThatDiedFromPlagueDuringYear = GameState.currentPopulation / 2;
                }
                else
                {
                    GameState.amountOfPeopleThatDiedFromPlagueDuringYear = 0;
                }

                KillPeople();
                Rats();
            }

            if (GameState.gameEnded) return;

            Report();

            TryToBuyLand();
            if (!GameState.boughtLand)
            {
                TryToSellLand();
            }
            TryToFeedPeople();
            TryToPlantLand();

            EndYear();
        }

        private static void Rats()
        {
            for (int i = 0; i < DefaultVariables.ratInfoList.Count; i++)
            {
                if (ReturnRandomInt(0, 100) <= DefaultVariables.ratInfoList[i].chance)
                {
                    GameState.bushelshEatenByRats = (int)(GameState.bushels / 100 * DefaultVariables.ratInfoList[i].percentageOfStolen);
                    GameState.bushels -= GameState.bushelshEatenByRats;

                    return;
                }
            }

            GameState.bushelshEatenByRats = 0;
        }

        private static void GrowBushels()
        {
            GameState.bushels += GameState.bushelsPerAcre * GameState.landPlanted;
        }

        private static void TryToPlantLand()
        {
            BonusPrintMethods.Print(DefaultTexts.plantSeedsQuestion + " (" + DefaultVariables.bushelsPerSeed + DefaultTexts.bushelsPerSeedText + ")");

            int.TryParse(Console.ReadLine(), out int parseResult);
            int amountOfSeedsToPlant = parseResult;

            if (amountOfSeedsToPlant * 2 > GameState.bushels)
            {
                BonusPrintMethods.Print(DefaultTexts.notEnoughMoneyText);
                TryToPlantLand();
            }
            else
            {
                GameState.bushels -= amountOfSeedsToPlant * 2;
                GameState.landPlanted = amountOfSeedsToPlant;
            }
        }

        private static void KillPeople()
        {
            if (GameState.amountOfPeopleThatStarved > GameState.currentPopulation / 100 * DefaultVariables.percentageOfPeopleNeededToBeKilledInOneTurnToLose)
            {
                Lose();
            }

            GameState.currentPopulation -= GameState.amountOfPeopleThatStarved;
            GameState.currentPopulation -= GameState.amountOfPeopleThatDiedFromPlagueDuringYear;
        }

        private static void Lose()
        {
            //LOSE
            GameState.gameEnded = true;
        }

        private static void TryToFeedPeople()
        {
            BonusPrintMethods.Print(DefaultTexts.feedPopulationQuestion + " (" + DefaultVariables.minimumBushelsPerYearPerPerson + DefaultTexts.perPersonText + ")");

            int.TryParse(Console.ReadLine(), out int parseResult);
            int amountOfBushelsToFeedPeople = parseResult;

            if (amountOfBushelsToFeedPeople > GameState.bushels)
            {
                BonusPrintMethods.Print(DefaultTexts.notEnoughMoneyText);
                TryToFeedPeople();
            }
            else
            {
                GameState.bushels -= amountOfBushelsToFeedPeople;
                int bushelsNeeded = GameState.currentPopulation * DefaultVariables.minimumBushelsPerYearPerPerson;
                GameState.amountOfPeopleThatStarved= (int)((bushelsNeeded - amountOfBushelsToFeedPeople) / DefaultVariables.minimumBushelsPerYearPerPerson);
            }
        }

        private static void AddPopulation()
        {
            GameState.imigration = (int)(GameState.bushelsPerAcre * (20 * GameState.landOwned + GameState.bushels) / GameState.currentPopulation / 100 + 1);
            GameState.currentPopulation += GameState.imigration;
        }

        private static void RandomizeNumbers()
        {
            GameState.landPrice = ReturnRandomLandPrice();

            GameState.bushelsPerAcre = ReturnRandomBushelsPerAcre();
        }

        private static void EndYear()
        {
            if (GameState.currentYear == DefaultVariables.lastPlayableYear)
            {
                EndGame();
            }
            else
            {
                GameState.currentYear++;
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
            GameState.WriteGameState(lastReport);
        }

        private static void TryToBuyLand()
        {
            int amountThatPlayerCanBuy = GameState.bushels / GameState.landPrice;
            BonusPrintMethods.Print(DefaultTexts.buyLandQuestion + " (" + DefaultTexts.canBuyText + amountThatPlayerCanBuy.ToString() + ")");

            int.TryParse(Console.ReadLine(), out int parseResult);
            int amountOfLandToBuy = parseResult;

            if (amountOfLandToBuy * GameState.landPrice > GameState.bushels)
            {
                BonusPrintMethods.Print(DefaultTexts.notEnoughMoneyText);
                TryToBuyLand();
            }
            else
            {
                if (amountOfLandToBuy > 0)
                {
                    GameState.bushels -= amountOfLandToBuy * GameState.landPrice;
                    GameState.landOwned+= amountOfLandToBuy;

                    BonusPrintMethods.Printbushels(GameState.bushels);

                    GameState.boughtLand = true;
                }
            }
        }

        private static void TryToSellLand()
        {
            int amountThatPlayerCanSell = GameState.landOwned;
            BonusPrintMethods.Print(DefaultTexts.sellLandQuestion + " (" + DefaultTexts.canSellText + amountThatPlayerCanSell.ToString() + ")");

            int.TryParse(Console.ReadLine(), out int parseResult);
            int amountOfLandToSell = parseResult;

            if (amountOfLandToSell > GameState.landOwned)
            {
                BonusPrintMethods.Print(DefaultTexts.notEnoughLandText);
                TryToSellLand();
            }
            else
            {
                GameState.bushels += amountOfLandToSell * GameState.landPrice;
                GameState.landOwned -= amountOfLandToSell;

                BonusPrintMethods.Printbushels(GameState.bushels);
                BonusPrintMethods.PrintOwnedLand(GameState.landOwned);
            }
        }

        public static int ReturnRandomLandPrice()
        {
            return rng.Next(DefaultVariables.minMaxLandPrice.min, DefaultVariables.minMaxLandPrice.max + 1);
        }

        public static int ReturnRandomBushelsPerAcre()
        {
            return rng.Next(DefaultVariables.minMaxBushelshPerAcre.min, DefaultVariables.minMaxBushelshPerAcre.max + 1);
        }

        private static bool PlagueOccured()
        {
            return (ReturnRandomInt(0, 100) > DefaultVariables.plaguePropability) ? false : true;
        }

        private static int ReturnRandomInt(int min, int max)
        {
            return rng.Next(min, max);
        }

        public static bool FirstYear()
        {
            return GameState.currentYear == DefaultVariables.startingYear;
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
            landPrice = DefaultVariables.startingLandPrice;
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
            BonusPrintMethods.Print(averageLandPrice.ToString());
        }

        private static void RandomizeNumbers()
        {
            landPrice = MainProgram.ReturnRandomLandPrice();

            landPrices.Add(landPrice);
            BonusPrintMethods.Print(landPrice.ToString());
        }
    }
}