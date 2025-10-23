using System;
using System.Collections.Generic;
using System.Linq;
using RPGManagerLib.Characters.Heroes;
using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Locations;
using RPGManagerLib.Quests;
using RPGManagerLib.Saves;
using RPGManagerLib.UI;

namespace RPGManagerLib.Characters.NPCs
{
    /// <summary>
    /// A smith NPC that can upgrade player weapons and offer a simple quest.
    /// </summary>
    public class BlackSmith : NPC
    {
        private const string QuestTitle = "Iron Procurement";
        private const string QuestDescription = "Bring 3 iron ore to the blacksmith.";

        /// <summary>
        /// Percentage increase applied to weapon damage each time the smith upgrades it.
        /// </summary>
        private const double DamageUpgradeMultiplier = 1.10;

        private Quest? smithQuest;

        private bool QuestOffered => smithQuest != null;
        private bool QuestCompleted => smithQuest?.State == QuestState.Completed;

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
        protected override void AddMenuOptions(MenuSystem menu, Character player)
        {
            EnsureQuestState(player);
            menu.AddOption("u", "Upgrade a weapon", () => UpgradeWeaponFlow(player));

            var questLabel = QuestOffered
                ? (QuestCompleted ? "Quest complete!" : "Ask about the job")
                : "Ask for work";

            menu.AddOption("w", questLabel, () => HandleQuestFlow(player));
        }

        /// <summary>
        /// Handles the console flow for upgrading a weapon for gold.
        /// </summary>
        private void UpgradeWeaponFlow(Character player)
        {
            if (player is not Warrior warrior)
            {
                Speak("You don't carry any weapons I can work on.");
                WaitForPlayer();
                return;
            }

            var weapons = GetUpgradableWeapons(warrior);
            if (weapons.Count == 0)
            {
                Speak("I only upgrade weapons, not other gear.");
                WaitForPlayer();
                return;
            }

            var selected = PromptWeaponSelection(weapons);
            if (selected == null)
            {
                return;
            }

            int price = CalculateUpgradePrice(selected);
            if (!TryChargeForUpgrade(player, selected, price))
            {
                return;
            }

            ApplyUpgrade(player, selected);
        }

        /// <summary>
        /// Minimal placeholder quest interaction flow.
        /// </summary>
        private void HandleQuestFlow(Character player)
        {
            EnsureQuestState(player);
            if (!QuestOffered)
            {
                OfferQuest(player);
                return;
            }

            if (!QuestCompleted && !TryCompleteQuest(player))
            {
                return;
            }

            CelebrateQuestCompletion(player);
        }

        /// <summary>
        /// Ensures the in-memory quest reference matches what the player already knows
        /// about the smith's quest. This avoids duplicating quest entries when the
        /// player returns later in the conversation.
        /// </summary>
        /// <param name="player">The interacting player character.</param>
        private void EnsureQuestState(Character player)
        {
            var existing = player.Quests?
                .FirstOrDefault(q => string.Equals(q.Title, QuestTitle, StringComparison.OrdinalIgnoreCase));
            if (existing != null)
            {
                smithQuest = existing;
            }
        }

        /// <summary>
        /// Returns a list of weapons owned by the warrior that can be upgraded.
        /// </summary>
        /// <param name="warrior">The warrior whose inventory is being examined.</param>
        private static List<Weapon> GetUpgradableWeapons(Warrior warrior)
        {
            return warrior.Weapons?
                .OfType<Weapon>()
                .ToList() ?? new List<Weapon>();
        }

        /// <summary>
        /// Shows an indexed list of weapons and returns the chosen option.
        /// </summary>
        /// <param name="weapons">Weapons that the smith can upgrade.</param>
        /// <returns>The selected weapon, or null if the player cancels.</returns>
        private Weapon? PromptWeaponSelection(IReadOnlyList<Weapon> weapons)
        {
            Console.WriteLine("Choose a weapon to upgrade:");
            for (int i = 0; i < weapons.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {FormatWeaponSummary(weapons[i])}");
            }

            Console.Write("Number (or 'q' to cancel): ");
            var input = Console.ReadLine();
            if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            if (!int.TryParse(input, out int choice) || choice < 1 || choice > weapons.Count)
            {
                return null;
            }

            return weapons[choice - 1];
        }

        /// <summary>
        /// Builds a human-readable, single line description of the weapon including
        /// its stats and upgrade progress.
        /// </summary>
        private static string FormatWeaponSummary(Weapon weapon)
        {
            int effectiveDamage = weapon.GetEffectiveDamage();
            int effectiveDurability = weapon.GetEffectiveDurability();
            string progress = DescribeUpgradeProgress(weapon);

            var parts = new[]
            {
                $"{weapon.Name} — Level {weapon.Level} ({weapon.Rarity})",
                $"Damage {weapon.DamageAmount} (Effective {effectiveDamage})",
                $"Durability {weapon.Durability} (Effective {effectiveDurability})",
                progress
            };

            return string.Join(" | ", parts);
        }

        /// <summary>
        /// Describes how far the weapon is from reaching the next rarity tier.
        /// </summary>
        private static string DescribeUpgradeProgress(Weapon weapon)
        {
            var minLvlForCurrent = Weapon.GetMinLevelForRarity(weapon.Rarity);
            var nextThreshold = weapon.GetNextRarityThresholdLevel();
            if (nextThreshold is int nextRarityLevel)
            {
                int progressTowardNext = Math.Max(0, weapon.Level - minLvlForCurrent);
                int levelsRequired = nextRarityLevel - minLvlForCurrent;
                return $"Progress {progressTowardNext}/{levelsRequired} toward next rarity tier";
            }

            return "Maximum rarity reached";
        }

        /// <summary>
        /// Calculates the gold cost for upgrading the provided weapon.
        /// </summary>
        private static int CalculateUpgradePrice(Weapon weapon)
        {
            int rarityIndex = (int)weapon.Rarity;
            return 50 + (25 * weapon.Level) + (100 * rarityIndex);
        }

        /// <summary>
        /// Confirms the player has enough gold and wants to spend it on the upgrade.
        /// </summary>
        /// <param name="player">The interacting character paying for the work.</param>
        /// <param name="selected">Weapon being upgraded.</param>
        /// <param name="price">Quoted gold cost.</param>
        /// <returns>True if the gold was taken and the flow can continue.</returns>
        private bool TryChargeForUpgrade(Character player, Weapon selected, int price)
        {
            Console.WriteLine($"Price to upgrade {selected.Name}: {price} gold. You have {player.Gold}.");
            if (player.Gold < price)
            {
                Speak("Come back with more coin.");
                WaitForPlayer();
                return false;
            }

            if (!PromptYesNo("Proceed with upgrade? (y/n): "))
            {
                return false;
            }

            player.Gold -= price;
            return true;
        }

        /// <summary>
        /// Applies the mechanical upgrade, persists the change, and presents the result
        /// to the player in a readable summary.
        /// </summary>
        private void ApplyUpgrade(Character player, Weapon weapon)
        {
            weapon.Level += 1;
            weapon.DamageAmount = (int)Math.Round(weapon.DamageAmount * DamageUpgradeMultiplier, MidpointRounding.AwayFromZero);
            bool rarityChanged = weapon.SyncRarityWithLevel();

            DisplayUpgradeSummary(player, weapon, rarityChanged);
            SaveManager.SaveOrUpdateCharacter(player);
        }

        /// <summary>
        /// Writes a concise report of the new weapon stats and informs the player about
        /// any rarity increases or gold spent.
        /// </summary>
        private void DisplayUpgradeSummary(Character player, Weapon weapon, bool rarityChanged)
        {
            Speak($"The work is done. {weapon.Name} is now level {weapon.Level} ({weapon.Rarity})." +
                (rarityChanged ? " Its rarity has improved!" : string.Empty));

            Console.WriteLine("Updated weapon stats:");
            Console.WriteLine($"  Damage: {weapon.DamageAmount} (Effective {weapon.GetEffectiveDamage()})");
            Console.WriteLine($"  Durability: {weapon.Durability} (Effective {weapon.GetEffectiveDurability()})");
            Console.WriteLine($"  {DescribeUpgradeProgress(weapon)}");
            Console.WriteLine($"Gold remaining: {player.Gold}");
            WaitForPlayer();
        }

        /// <summary>
        /// Starts the smith's quest for the first time and notifies the player.
        /// </summary>
        private void OfferQuest(Character player)
        {
            smithQuest = new Quest(QuestTitle, QuestDescription);
            smithQuest.Start();

            if (!player.Quests.Any(q => string.Equals(q.Title, QuestTitle, StringComparison.OrdinalIgnoreCase)))
            {
                player.Quests.Add(smithQuest);
            }

            SaveManager.SaveOrUpdateCharacter(player);
            AdvanceDialoguePhase(DialoguePhase.QuestOffered);
            Speak(GetNextDialogueLine());
            Speak(GetNextDialogueLine());
            WaitForPlayer();
        }

        /// <summary>
        /// Provides a placeholder completion flow that can be extended later.
        /// </summary>
        private bool TryCompleteQuest(Character player)
        {
            Speak("Still waiting on those ore chunks.");
            if (!PromptYesNo("Mark quest as completed for demo? (y/n): "))
            {
                WaitForPlayer();
                return false;
            }

            smithQuest?.Complete();
            SaveManager.SaveOrUpdateCharacter(player);
            return true;
        }

        /// <summary>
        /// Plays the dialogue and persistence steps once the quest is marked complete.
        /// </summary>
        private void CelebrateQuestCompletion(Character player)
        {
            AdvanceDialoguePhase(DialoguePhase.QuestCompleted);
            Speak(GetNextDialogueLine());
            WaitForPlayer();
            SaveManager.SaveOrUpdateCharacter(player);
        }
    }
}
