namespace Hamurabi
{
    /// <summary>
    /// Main class that holds most of the core gameplay
    /// </summary>
    internal class MainProgram
    {
        private static Random rng = new Random();

        /// <summary>
        /// Void that gets called automaticly on game start
        /// </summary>
        private static void Main()
        {
            MainGame();
        }

        /// <summary>
        /// Main method that calls other methods that are required for the game to work
        /// </summary>
        private static void MainGame()
        {
            //Resets values
            GameState.boughtLand = false;

            //If the game is in its first year values will stay the same and won't change so when game designers set some starting values they know that the game will start with those values
            if (GameState.currentYear != DefaultVariables.startingYear)
            {
                GrowBushels();
                AddPopulation();

                DoPlague();
                Rats();

                KillPeople();
            }

            //Stop the game when meny people die
            if (GameState.gameEnded) return;

            Report();

            TryToBuyLand();
            if (!GameState.boughtLand)
            {
                TryToSellLand();
            }
            TryToFeedPeople();
            TryToPlantBushels();

            EndYear();
        }

        #region Planting and growing
            /// <summary>
            /// Calculates how may bushels did grow
            /// </summary>
            private static void GrowBushels()
            {
                GameState.bushels += GameState.bushelsPerAcre * GameState.landPlanted;
            }
            
            /// <summary>
            /// Ask player how many seeds do they want to plant and plant them
            /// </summary>
            private static void TryToPlantBushels()
            {
                Debug.Log($"How many acres do you wish to plant with seed? (One seed costs {DefaultVariables.bushelsPerSeed} bushels)");

                int.TryParse(Console.ReadLine(), out int parseResult);
                int amountOfAcresToPlant = parseResult;

                if (amountOfAcresToPlant * DefaultVariables.bushelsPerSeed > GameState.bushels)
                {
                    Debug.Log(DefaultTexts.notEnoughMoneyText);
                    TryToPlantBushels();
                }
                else if (amountOfAcresToPlant > GameState.currentPopulation * DefaultVariables.personCanPlantSeeds)
                {
                    Debug.Log(DefaultTexts.notEnoughPeopleText);
                    TryToPlantBushels();
                }
                else if (amountOfAcresToPlant > GameState.landOwned)
                {
                    Debug.Log(DefaultTexts.notEnoughLandText);
                    TryToPlantBushels();
                }
                else
                {
                    GameState.bushels -= amountOfAcresToPlant * DefaultVariables.bushelsPerSeed;
                    GameState.landPlanted = amountOfAcresToPlant;
                }
            }

            /// <summary>
            /// Get the amount of bushels grown per acre
            /// </summary>
            /// <returns>Returns amount of bushels grown per acre</returns>
            public static int ReturnRandomBushelsPerAcre()
            {
                return rng.Next(DefaultVariables.minMaxBushelshPerAcre.min, DefaultVariables.minMaxBushelshPerAcre.max + 1);
            }
        #endregion

        #region Buy Sell Land
            /// <summary>
            /// Ask player how many land do they want to buy
            /// </summary>
            private static void TryToBuyLand()
            {
                int amountThatPlayerCanBuy = GameState.bushels / GameState.landPrice;
                Debug.Log($"How much acres of land do you want to buy? (You can buy {(int)(GameState.bushels / GameState.landPrice)} acres of land)");

                int.TryParse(Console.ReadLine(), out int parseResult);
                int amountOfLandToBuy = parseResult;

                if (amountOfLandToBuy * GameState.landPrice > GameState.bushels)
                {
                    Debug.Log(DefaultTexts.notEnoughMoneyText);
                    TryToBuyLand();
                }
                else
                {
                    if (amountOfLandToBuy > 0)
                    {
                        GameState.bushels -= amountOfLandToBuy * GameState.landPrice;
                        GameState.landOwned += amountOfLandToBuy;

                        BonusPrintMethods.Printbushels(GameState.bushels);

                        GameState.boughtLand = true;
                    }
                }
            }

            /// <summary>
            /// Ask player how many land do they want to sell
            /// </summary>
            private static void TryToSellLand()
                {
                Debug.Log($"How much acres of land do you want to sell? (You can sell {GameState.landOwned} acres of land)");

                int.TryParse(Console.ReadLine(), out int parseResult);
                int amountOfLandToSell = parseResult;

                if (amountOfLandToSell > GameState.landOwned)
                {
                    Debug.Log(DefaultTexts.notEnoughLandText);
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

            /// <summary>
            /// Returns random price of one acre of land.
            /// </summary>
            /// <returns>Returns random price of one acre of land</returns>
            public static int ReturnRandomLandPrice()
            {
                return rng.Next(DefaultVariables.minMaxLandPrice.min, DefaultVariables.minMaxLandPrice.max + 1);
            }
        #endregion

        #region Add Feed and Kill People
            private static void AddPopulation()
            {
                GameState.imigration = (int)(GameState.bushelsPerAcre * (20 * GameState.landOwned + GameState.bushels) / GameState.currentPopulation / 100 + 1);
                GameState.currentPopulation += GameState.imigration;

                GameState.totalPeople += GameState.imigration;
            }

            private static void TryToFeedPeople()
            {
                Debug.Log($"How many bushels do you wish to feed your people? (One person needs {DefaultVariables.minimumBushelsPerYearPerPerson} busheels per year)");

                int.TryParse(Console.ReadLine(), out int parseResult);
                int amountOfBushelsToFeedPeople = parseResult;

                if (amountOfBushelsToFeedPeople > GameState.bushels)
                {
                    Debug.Log(DefaultTexts.notEnoughMoneyText);
                    TryToFeedPeople();
                }
                else
                {
                    GameState.bushels -= amountOfBushelsToFeedPeople;
                    int bushelsNeeded = GameState.currentPopulation * DefaultVariables.minimumBushelsPerYearPerPerson;
                    GameState.amountOfPeopleThatStarved = (int)((bushelsNeeded - amountOfBushelsToFeedPeople) / DefaultVariables.minimumBushelsPerYearPerPerson);
                }
            }

            private static void KillPeople()
            {
                if (GameState.amountOfPeopleThatStarved > GameState.currentPopulation / 100 * DefaultVariables.percentageOfPeopleNeededToBeKilledInOneTurnToLose)
                {
                    EndGame();
                }

                GameState.percentageOfPeopleThatDiedInEveryYear.Add(GameState.amountOfPeopleThatStarved / GameState.currentPopulation * 100);
                GameState.totalPeopleThatDied += GameState.amountOfPeopleThatStarved;
                GameState.currentPopulation -= GameState.amountOfPeopleThatStarved;
                GameState.currentPopulation -= GameState.amountOfPeopleThatDiedFromPlagueDuringYear;
            }
        #endregion

        #region Rats and Plague
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

            private static void DoPlague()
            {
                if (ReturnRandomInt(0, 100) > DefaultVariables.plaguePropability)
                {
                    GameState.amountOfPeopleThatDiedFromPlagueDuringYear = GameState.currentPopulation / 2;
                }
                else
                {
                    GameState.amountOfPeopleThatDiedFromPlagueDuringYear = 0;
                }
            }
        #endregion

        #region Random values
            private static int ReturnRandomInt(int min, int max)
            {
                return rng.Next(min, max);
            }

            private static void RandomizeNumbers()
            {
                GameState.landPrice = ReturnRandomLandPrice();

                GameState.bushelsPerAcre = ReturnRandomBushelsPerAcre();
            }
        #endregion

        #region End game/year
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
                GameState.gameEnded = true;
                Report(true);
            }
        #endregion

        /// <summary>
        /// Writes Report to the console
        /// </summary>
        /// <param name="lastReport"> If true it will report different texts and information</param>
        private static void Report(bool lastReport = false)
        {
            GameState.WriteGameState(lastReport);
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
}