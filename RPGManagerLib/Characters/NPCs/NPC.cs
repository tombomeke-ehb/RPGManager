using System;
using System.Collections.Generic;
using System.Linq;
using RPGManagerLib.Locations;
using RPGManagerLib.Characters.Heroes;
using RPGManagerLib.UI;

namespace RPGManagerLib.Characters.NPCs
{
    /// <summary>
    /// Base class for all NPCs. Provides dialogue management and a default
    /// interaction menu (Talk/Leave plus subclass-specific options).
    /// </summary>
    public abstract class NPC
    {
        /// <summary>
        /// Display name of the NPC.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The location this NPC belongs to.
        /// </summary>
        public Location Location { get; }

        /// <summary>
        /// Dialogue phases (states) used to vary lines over time.
        /// </summary>
        public enum DialoguePhase { Default, QuestOffered, QuestAccepted, QuestCompleted }

        /// <summary>
        /// Lines grouped by dialogue phase.
        /// </summary>
        protected readonly Dictionary<DialoguePhase, List<string>> dialogueByPhase = new();

        /// <summary>
        /// Current phase for this NPC.
        /// </summary>
        protected DialoguePhase currentPhase = DialoguePhase.Default;

        private readonly Dictionary<DialoguePhase, int> phaseIndices = new();

        /// <summary>
        /// Read-only view of the configured dialogue.
        /// </summary>
        public IReadOnlyDictionary<DialoguePhase, List<string>> Dialogue => dialogueByPhase;

        /// <summary>
        /// Initializes an NPC with a name and location.
        /// </summary>
        public NPC(string name, Location location)
        {
            Name = name;
            Location = location;
        }

        /// <summary>
        /// Default interaction loop showing a simple NPC menu.
        /// Subclasses can add extra options by overriding AddMenuOptions.
        /// </summary>
        public virtual void Interact(Character player)
        {
            bool exit = false;
            while (!exit)
            {
                var menu = new MenuSystem($"{Name} — What do you want to do?");
                menu.AddOption("t", "Talk", () =>
                {
                    var line = GetNextDialogueLine();
                    Speak(line);
                    WaitForPlayer();
                });

                // Allow subclasses to append their actions
                AddMenuOptions(menu, player);

                menu.AddOption("l", "Leave", () => exit = true);
                menu.Show();
            }
        }

        /// <summary>
        /// Hook for NPC-specific options like shop/upgrade/quest.
        /// </summary>
        protected virtual void AddMenuOptions(MenuSystem menu, Character player) { }

        /// <summary>
        /// Sets dialogue lines for a specific phase.
        /// </summary>
        protected void SetDialogue(DialoguePhase phase, IEnumerable<string> lines)
        {
            dialogueByPhase[phase] = lines.ToList();
            if (!phaseIndices.ContainsKey(phase)) phaseIndices[phase] = 0;
        }

        /// <summary>
        /// Advances the current phase and resets its index.
        /// </summary>
        public void AdvanceDialoguePhase(DialoguePhase nextPhase)
        {
            currentPhase = nextPhase;
            phaseIndices[currentPhase] = 0;
        }

        /// <summary>
        /// Returns the next dialogue line for the current phase, cycling at the end.
        /// </summary>
        protected string GetNextDialogueLine()
        {
            if (!dialogueByPhase.TryGetValue(currentPhase, out var lines) || lines.Count == 0)
            {
                return "...";
            }

            var idx = phaseIndices[currentPhase];
            var line = lines[idx];
            idx = (idx + 1) % lines.Count;
            phaseIndices[currentPhase] = idx;
            return line;
        }

        /// <summary>
        /// Writes a formatted line spoken by this NPC.
        /// </summary>
        protected void Speak(string message)
        {
            Console.WriteLine($"{Name}: {message}");
        }

        /// <summary>
        /// Waits for the player to press enter, used to pause dialog flows.
        /// </summary>
        protected void WaitForPlayer()
        {
            Console.WriteLine("(Press ENTER)");
            Console.ReadLine();
        }

        /// <summary>
        /// Simple yes/no confirmation helper shared by NPC interactions.
        /// </summary>
        protected static bool PromptYesNo(string prompt)
        {
            Console.Write(prompt);
            var response = Console.ReadLine()?.Trim().ToLowerInvariant();
            return response == "y" || response == "yes";
        }
    }
}
