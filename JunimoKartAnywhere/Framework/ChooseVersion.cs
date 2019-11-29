using StardewModdingAPI;
using StardewValley;
using StardewValley.Minigames;

namespace JunimoKartAnywhere.Framework
{
    /// <summary>Class that allows the player to choose between Old Junimo Kart and New Junimo Kart</summary>
    class ChooseVersion
    {
        //The mods config used to modify the console command during runtime
        private ModConfig Config;

        //SMAPI's Monitor used for logging to the console
        private IMonitor Monitor;

        public ChooseVersion(ModConfig config, IMonitor monitor)
        {
            Config = config;
            Monitor = monitor;
        }

        /// <summary>Shows the menu that allows the player to choose Endless or Progress from Old JK</summary>
        public void ShowOldResponses()
        {
            Response[] answerChoices = new Response[3]
            {
                new Response("Progress Old Junimo Kart", "Progress Old Junimo Kart"),
                new Response("Endless Old Junimo Kart", "Endless Old Junimo Kart"),
                new Response("Exit", "Exit")
            };

            GameLocation.afterQuestionBehavior versionSelected = new GameLocation.afterQuestionBehavior(RecieveOldResponse);
            Game1.player.currentLocation.createQuestionDialogue("Old Junimo Kart", answerChoices, versionSelected);
        }

        /// <summary>Recieves which mode that the player wants to play for old JA</summary>
        /// <param name="who">The current farmer</param>
        /// <param name="versionSelected">Which old gamemode to play</param>
        public void RecieveOldResponse(Farmer who, string versionSelected)
        {
            switch (versionSelected)
            {
                case "Progress Old Junimo Kart":
                    Game1.currentMinigame = new OldMineCart(5, 3);
                    break;
                case "Endless Old Junimo Kart":
                    Game1.currentMinigame = new OldMineCart(5, 2);
                    break;
                default:
                    Game1.activeClickableMenu.exitThisMenu();
                    break;
            }
        }

        /// <summary>Allows changing which version using a SMAPI command</summary>
        /// <param name="command">The command</param>
        /// <param name="args">The command arg, looking for a bool</param>
        public void SetChooseVersion(string command, string[] args)
        {
            if (args[0] == null)
                return;

            //Check if the command is valid a bool
            bool.TryParse(args[0], out bool canParse);
            if(!canParse)
                return;

            //Set the variable
            Config.OldVersion = bool.Parse(args[0]);
            if (Config.OldVersion)
            {
                Config.ChooseLevel = false;
                Monitor.Log("Junimo Kart old version is enabled.", LogLevel.Info);
            }
            else
                Monitor.Log("Junimo Kart old version is disabled.", LogLevel.Info);
        }
    }
}
