using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Minigames;
using System.Collections.Generic;

namespace JunimoKartAnywhere.Framework
{
    /// <summary>Class that allows a player to resume a game from the start of the last level obtained</summary>
    public class ChooseProgress : IResponse
    {
        private IModHelper Helper;

        private IMonitor Monitor;

        /// <summary>Config option to save progress</summary>
        public bool SaveProgress = true;

        private bool ResumeProgress = false;

        private int LivesLeft;

        private bool LastLevelWasPerfect;

        private int CoinCount;

        private List<MineCart.LevelTransition> LevelTransitions;

        public ChooseProgress(IModHelper helper, IMonitor monitor)
        {
            Helper = helper;
            Monitor = monitor;
        }

        public void ShowSaveProgressResponses()
        {
            // Create the different responses
            Response[] answerChoices = new Response[3]
            {
                new Response("Progress (New)", "Progress (New)"),
                new Response("Progress (Resume)", "Progress (Resume)"),
                new Response("Exit", Game1.content.LoadString("Strings\\StringsFromCSFiles:TitleMenu.cs.11738"))
            };

            GameLocation.afterQuestionBehavior progressBehavior = new GameLocation.afterQuestionBehavior(RecieveResponse);
            //Show the question dialogue
            Game1.player.currentLocation.createQuestionDialogue("Save Progress Mode!", answerChoices, progressBehavior);
        }

        public void RecieveResponse(Farmer who, string progressSelected)
        {
            switch (progressSelected)
            {
                case "Progress (New)":
                    Game1.currentMinigame = new MineCart(-1, 3);
                    Helper.Events.GameLoop.UpdateTicked += UpdateTicked;
                    break;
                case "Progress (Resume)":
                    Game1.currentMinigame = new MineCart(-1, 3);
                    ResumeProgress = true;
                    Helper.Events.GameLoop.UpdateTicked += UpdateTicked;
                    break;
                default:
                    Game1.activeClickableMenu.exitThisMenu();
                    break;
            }
        }

        private void UpdateTicked(object sender, UpdateTickedEventArgs e)
        {
            if (Game1.currentMinigame == null)
            {
                Helper.Events.GameLoop.UpdateTicked -= UpdateTicked;
                return;
            }

            if (Helper.Reflection.GetField<int>((Game1.currentMinigame as MineCart), "livesLeft").GetValue() != 0 && ResumeProgress)
            {
                ResumeGame();
                ResumeProgress = false;
            }

            if (!ResumeProgress)
            {
                LivesLeft = Helper.Reflection.GetField<int>((Game1.currentMinigame as MineCart), "livesLeft").GetValue();
                LastLevelWasPerfect = Helper.Reflection.GetField<bool>((Game1.currentMinigame as MineCart), "lastLevelWasPerfect").GetValue();
                CoinCount = (Game1.currentMinigame as MineCart).coinCount;
            }

            if (Helper.Reflection.GetField<bool>((Game1.currentMinigame as MineCart), "reachedFinish").GetValue())
            {
                RemoveTransitionFromArray();
            }
        }

        private void ResumeGame()
        {
            (Game1.currentMinigame as MineCart).coinCount = CoinCount;
            Helper.Reflection.GetField<int>((Game1.currentMinigame as MineCart), "livesLeft").SetValue(LivesLeft);
            Helper.Reflection.GetField<bool>((Game1.currentMinigame as MineCart), "lastLevelWasPerfect").SetValue(LastLevelWasPerfect);
            (Game1.currentMinigame as MineCart).LEVEL_TRANSITIONS = LevelTransitions.ToArray();
        }


        /*
         * THOUGHT PROCESS
         * The level tranistions are an array
         * When the user completes a level, then remove that level from the array.
         * Save that array to a variable and set that array to the Level Area when the game starts.
         * 
         * Challenges:
         *  Some levels are random and it's hard to get which level the player is actually on 
         */
        private void RemoveTransitionFromArray()
        {
            LevelTransitions = new List<MineCart.LevelTransition>((Game1.currentMinigame as MineCart).LEVEL_TRANSITIONS);
            LevelTransitions.RemoveAt(0);
        }
    }
}
