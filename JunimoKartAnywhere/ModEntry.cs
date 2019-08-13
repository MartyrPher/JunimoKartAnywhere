using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Minigames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JunimoKartAnywhere
{
    /// <summary>The mods entry point.</summary>
    public class ModEntry : Mod
    {
        /// <summary>The mods config</summary>
        private ModConfig config;

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            this.config = helper.ReadConfig<ModConfig>();
            this.Helper.WriteConfig(this.config);

            this.Helper.Events.Input.ButtonPressed += this.ButtonPressed;
        }


        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Equals(this.config.startGameKey) && Game1.currentMinigame == null)
            { 
                //Create the different responses
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
}
