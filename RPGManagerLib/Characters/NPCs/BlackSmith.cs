using RPGManagerLib.Locations;

namespace RPGManagerLib.Characters.NPCs
{
    /// <summary>
    /// A smith NPC that can upgrade player weapons and offer a simple quest.
    /// </summary>
    public class BlackSmith : NPC
    {
        private bool questOffered = false;
        private bool questCompleted = false;
        private RPGManagerLib.Quests.Quest? smithQuest;

        public BlackSmith(Location location)
            : base("Black Smith", location)
        {
            SetDialogue(DialoguePhase.Default, new[]
            {
                "Hammer's hot, steel is ready.",
                "Need your gear fixed or forged?",
                "I can sharpen that blade for a fair price."
            });

            SetDialogue(DialoguePhase.QuestOffered, new[]
            {
                "Got a job for you: fetch me 3 iron ore.",
                "Bring them back and I'll cut you a deal."
            });

            SetDialogue(DialoguePhase.QuestCompleted, new[]
            {
                "Good work. As promised, discounts for my favorite smith's hand.",
                "Spread the word: best steel in town!"
            });
        }

        /// <summary>
        /// Adds smith-specific actions to the interaction menu.
        /// </summary>
        protected override void AddMenuOptions(UI.MenuSystem menu, Heroes.Character player)
        {
            EnsureQuestState(player);
            menu.AddOption("u", "Upgrade a weapon", () => UpgradeWeaponFlow(player));
            menu.AddOption("w", questOffered ? (questCompleted ? "Quest complete!" : "Ask about the job") : "Ask for work",
                () => HandleQuestFlow(player));
        }

        /// <summary>
        /// Handles the console flow for upgrading a weapon for gold.
        /// </summary>
        private void UpgradeWeaponFlow(Heroes.Character player)
        {
            // Currently only Warrior has a weapons list
            if (player is not Heroes.Warrior warrior || warrior.Weapons == null || warrior.Weapons.Count == 0)
            {
                Console.WriteLine($"{name}: You don't carry any weapons I can work on.");
                Console.WriteLine("(Press ENTER)");
                Console.ReadLine();
                return;
            }

            // Filter to actual weapons
            var weapons = warrior.Weapons.OfType<Items.Weapons.Weapon>().ToList();
            if (weapons.Count == 0)
            {
                Console.WriteLine($"{name}: I only upgrade weapons, not other gear.");
                Console.WriteLine("(Press ENTER)");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Choose a weapon to upgrade:");
            for (int i = 0; i < weapons.Count; i++)
            {
                var w = weapons[i];
                var minLvlForCurrent = Items.Weapons.Weapon.GetMinLevelForRarity(w.Rarity);
                var nextThreshold = w.GetNextRarityThresholdLevel();
                string progress = nextThreshold is int nt
                    ? $"Progress: {Math.Max(0, w.Level - minLvlForCurrent)}/{nt - minLvlForCurrent} to next tier"
                    : "Max tier reached";
                Console.WriteLine($"{i + 1}. {w.Name} (Lvl {w.Level}, {w.Rarity}, Damage {w.DamageAmount} | Eff {w.GetEffectiveDamage()}, Dur {w.Durability} | Eff {w.GetEffectiveDurability()}) — {progress}");
            }
            Console.Write("Number (or 'q' to cancel): ");
            var input = Console.ReadLine();
            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase)) return;
            if (!int.TryParse(input, out int choice) || choice < 1 || choice > weapons.Count) return;

            var selected = weapons[choice - 1];

            // Simple price model: base 50 + 25 per level + 100 per rarity tier (COMMON=0)
            int rarityIndex = (int)selected.Rarity; // enum order from COMMON upwards
            int price = 50 + (25 * selected.Level) + (100 * rarityIndex);

            Console.WriteLine($"Price to upgrade {selected.Name}: {price} gold. You have {player.Gold}.");
            if (player.Gold < price)
            {
                Console.WriteLine($"{name}: Come back with more coin.");
                Console.WriteLine("(Press ENTER)");
                Console.ReadLine();
                return;
            }

            Console.Write("Proceed with upgrade? (y/n): ");
            var confirm = Console.ReadLine()?.Trim().ToLower();
            if (confirm != "y") return;

            player.Gold -= price;
            selected.Level += 1;
            selected.DamageAmount = (int)(selected.DamageAmount * 1.10); // +10% base damage on upgrade
            bool rarityChanged = selected.SyncRarityWithLevel();

            var minAfter = Items.Weapons.Weapon.GetMinLevelForRarity(selected.Rarity);
            var nextAfter = selected.GetNextRarityThresholdLevel();
            string progressAfter = nextAfter is int nt2
                ? $"Progress: {Math.Max(0, selected.Level - minAfter)}/{nt2 - minAfter} to next tier"
                : "Max tier reached";

            Console.WriteLine($"{name}: Done. {selected.Name} is now Lvl {selected.Level} [{selected.Rarity}]" + (rarityChanged ? " — rarity increased!" : "."));
            Console.WriteLine($"Damage {selected.DamageAmount} | Eff {selected.GetEffectiveDamage()}, Dur {selected.Durability} | Eff {selected.GetEffectiveDurability()}");
            Console.WriteLine(progressAfter);
            Console.WriteLine($"Gold remaining: {player.Gold}");
            Console.WriteLine("(Press ENTER)");
            Console.ReadLine();

            // Persist immediately in case of crash
            RPGManagerLib.Saves.SaveManager.SaveOrUpdateCharacter(player);
        }

        /// <summary>
        /// Minimal placeholder quest interaction flow.
        /// </summary>
        private void HandleQuestFlow(Heroes.Character player)
        {
            EnsureQuestState(player);
            if (!questOffered)
            {
                questOffered = true;
                smithQuest = new RPGManagerLib.Quests.Quest(
                    title: "Iron Procurement",
                    description: "Bring 3 iron ore to the blacksmith."
                );
                smithQuest.Start();
                // Track quest on the player if it doesn't already exist
                if (!player.Quests.Any(q => string.Equals(q.Title, smithQuest.Title, StringComparison.OrdinalIgnoreCase)))
                    player.Quests.Add(smithQuest);
                RPGManagerLib.Saves.SaveManager.SaveOrUpdateCharacter(player);
                AdvanceDialoguePhase(DialoguePhase.QuestOffered);
                Console.WriteLine($"{name}: {GetNextDialogueLine()}");
                Console.WriteLine($"{name}: {GetNextDialogueLine()}");
                Console.WriteLine("(Press ENTER)");
                Console.ReadLine();
                return;
            }

            if (!questCompleted)
            {
                Console.WriteLine($"{name}: Still waiting on those ore chunks.");
                Console.Write("Mark quest as completed for demo? (y/n): ");
                var done = Console.ReadLine()?.Trim().ToLower();
                if (done == "y")
                {
                    questCompleted = true;
                    smithQuest?.Complete();
                    RPGManagerLib.Saves.SaveManager.SaveOrUpdateCharacter(player);
                }
                else
                {
                    Console.WriteLine("(Press ENTER)");
                    Console.ReadLine();
                    return;
                }
            }

            // If you had a way to check inventory, you could flip questCompleted
            AdvanceDialoguePhase(DialoguePhase.QuestCompleted);
            Console.WriteLine($"{name}: {GetNextDialogueLine()}");
            Console.WriteLine("(Press ENTER)");
            Console.ReadLine();
            RPGManagerLib.Saves.SaveManager.SaveOrUpdateCharacter(player);
        }

        private void EnsureQuestState(Heroes.Character player)
        {
            // Align local flags to player's quest list so state survives reloads
            var existing = player.Quests?.FirstOrDefault(q => string.Equals(q.Title, "Iron Procurement", StringComparison.OrdinalIgnoreCase));
            if (existing != null)
            {
                smithQuest = existing;
                questOffered = true;
                questCompleted = existing.State == RPGManagerLib.Quests.QuestState.Completed;
            }
        }
    }
}
