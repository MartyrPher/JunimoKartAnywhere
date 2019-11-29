using JunimoKartAnywhere.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;

namespace JunimoKartAnywhere
{
    /// <summary>The mods entry point.</summary>
    public class ModEntry : Mod
    {
        /// <summary>The mods config</summary>
        private ModConfig Config;

        /// <summary>The Instance of <see cref="LevelMap"/></summary>
        private LevelMap LevelsMapped = new LevelMap();

        /// <summary>The Instance of <see cref="ChooseLevel"/></summary>
        private ChooseLevel ChosenLevel;

        /// <summary>The Instance of <see cref="ChooseProgress"/></summary>
        /// <remarks>Cut for the time being, will have to revisted later</remarks>
        //private ChooseProgress ChosenProgress;

        /// <summary>The Instance of <see cref="ChooseVersion"/></summary>
        private ChooseVersion ChosenVersion;


        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<ModConfig>();
            Helper.WriteConfig(Config);

            ChosenLevel = new ChooseLevel(Config, Monitor);
            ChosenVersion = new ChooseVersion(Config, Monitor);

            //Cut for the meantime
            //ChosenProgress = new ChooseProgress(Helper, Monitor);

            Helper.Events.Input.ButtonPressed += ButtonPressed;

            Helper.ConsoleCommands.Add("choose_level", "Allows the player to be able to choose a specific level in Junino Kart.\n\nUsage: choose_level <bool>\n- bool: true or false.", ChosenLevel.SetChooseLevel);
            Helper.ConsoleCommands.Add("old_version", "Allows the player to be able to play the old Junimo Kart.\n\nUsage: old_version <bool>\n- bool: true or false.", ChosenVersion.SetChooseVersion);
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Equals(Config.StartGameKey) && Config.ChooseLevel && Game1.currentMinigame == null)
            {
                ChosenLevel.ShowLevelPickerMenu();
            }
            else if (e.Button.Equals(Config.StartGameKey) && Game1.currentMinigame == null && Config.OldVersion)
            {
                ChosenVersion.ShowOldResponses();
            }
            else if (e.Button.Equals(Config.StartGameKey) && Game1.currentMinigame == null)
            {
                ShowOriginalResponses();
            }
        }

        /// <summary>Shows the regular Junimo Kart Options</summary>
        private void ShowOriginalResponses()
        {
            // Create the different responses
            Response[] answerChoices = new Response[3]
            {
                new Response("Progress", Game1.content.LoadString("Strings\\StringsFromCSFiles:Object.cs.12873")),
                new Response("Endless", Game1.content.LoadString("Strings\\StringsFromCSFiles:Object.cs.12875")),
                new Response("Exit", Game1.content.LoadString("Strings\\StringsFromCSFiles:TitleMenu.cs.11738"))
            };

            //Show the question dialogue
            Game1.player.currentLocation.createQuestionDialogue(Game1.content.LoadString("Strings\\Locations:Saloon_Arcade_Minecart_Menu"), answerChoices, "MinecartGame");
        }
    }
}
