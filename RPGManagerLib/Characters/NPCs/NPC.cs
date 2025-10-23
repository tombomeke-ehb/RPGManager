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
        public string name { get; set; }

        /// <summary>
        /// The location this NPC belongs to.
        /// </summary>
        public Location location;

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
            this.name = name;
            this.location = location;
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
                var menu = new MenuSystem($"{name} — What do you want to do?");
                menu.AddOption("t", "Talk", () =>
                {
                    var line = GetNextDialogueLine();
                    Console.WriteLine($"{name}: {line}");
                    Console.WriteLine("(Press ENTER)");
                    Console.ReadLine();
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
    }
}
