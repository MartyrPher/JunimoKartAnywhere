using StardewModdingAPI;
using StardewValley;
using StardewValley.Minigames;

namespace JunimoKartAnywhere.Framework
{
    /// <summary>Class that allows the player to choose a specific level</summary>
    class ChooseLevel
    {
        /// <summary>The mods config used to modify the console command during runtime</summary>
        private ModConfig Config;

        //SMAPI's Monitor used for logging to the console
        private IMonitor Monitor;

        public ChooseLevel(ModConfig config, IMonitor monitor)
        {
            Config = config;
            Monitor = monitor;
        }

        /// <summary>Shows the menu that allows the player to choose a specific level</summary>
        public void ShowLevelPickerMenu()
        {
            Response[] levelChooser = new Response[9];
            int i = 0;
            foreach (string levelName in LevelMap.KartLevelMap.Keys)
            {
                levelChooser[i] = new Response(levelName, levelName);
                i++;
            }
            GameLocation.afterQuestionBehavior levelSelected = new GameLocation.afterQuestionBehavior(RecieveResponse);
            Game1.player.currentLocation.createQuestionDialogue("Which Level?", levelChooser, levelSelected);
        }

        /// <summary>Recieves the chosen level and starts a progess mode game to that level</summary>
        /// <param name="who">The current farmer</param>
        /// <param name="levelSelected">The level that was selected</param>
        public void RecieveResponse(Farmer who, string levelSelected)
        {
            Game1.currentMinigame = new MineCart(-1, 3);
            MineCart.LevelTransition[] array = new MineCart.LevelTransition[1];
            array[0] = LevelMap.KartLevelMap[levelSelected];
            (Game1.currentMinigame as MineCart).LEVEL_TRANSITIONS = array;
        }

        /// <summary>Allows choosing which level using a SMAPI command</summary>
        /// <param name="command">The command</param>
        /// <param name="args">The command arg, looking for a bool</param>
        public void SetChooseLevel(string command, string[] args)
        {
            if (args[0] == null)
                return;

            //Check if the command is a valid bool
            bool.TryParse(args[0], out bool canParse);
            if (!canParse)
                return;

            //Set the variable 
            Config.ChooseLevel = bool.Parse(args[0]);
            if (Config.ChooseLevel)
            {
                Config.OldVersion = false;
                Monitor.Log("Junimo Kart level choosing is enabled.", LogLevel.Info);
            }
            else
                Monitor.Log("Junimo Kart level choosing is disabled.", LogLevel.Info);
        }

        ///TODO: When setting back to back afterQuestions, the menu randomly closes when selecting the answer
        ///to the second question.

        //public void ShowDefaultChoices()
        //{
        //    // Create the different responses
        //    Response[] answerChoices = new Response[3]
        //    {
        //        new Response("Progress", Game1.content.LoadString("Strings\\StringsFromCSFiles:Object.cs.12873")),
        //        new Response("Endless", Game1.content.LoadString("Strings\\StringsFromCSFiles:Object.cs.12875")),
        //        new Response("Exit", Game1.content.LoadString("Strings\\StringsFromCSFiles:TitleMenu.cs.11738"))
        //    };

        //    GameLocation.afterQuestionBehavior levelSelected = new GameLocation.afterQuestionBehavior(RecieveOriginalResponse);
        //    //Show the question dialogue
        //    Game1.player.currentLocation.createQuestionDialogue(Game1.content.LoadString("Strings\\Locations:Saloon_Arcade_Minecart_Menu"), answerChoices, levelSelected);
        //}

        //public void RecieveOriginalResponse(Farmer who, string typeSelected)
        //{
        //    switch (typeSelected)
        //    {
        //        case "Progress":
        //            ShowLevelPickerMenu();
        //            Monitor.Log("Showing Picker Menu", LogLevel.Alert);
        //            break;
        //        case "Endless":
        //            Game1.currentMinigame = new MineCart(-1, 2);
        //            Monitor.Log("Starting Endless", LogLevel.Alert);
        //            break;
        //        default:
        //            Game1.activeClickableMenu.exitThisMenu();
        //            Monitor.Log("Exiting Menu", LogLevel.Alert);
        //            break;
        //    }
        //}
    }
}
