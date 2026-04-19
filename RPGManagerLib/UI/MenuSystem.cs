using System;
using System.Collections.Generic;

namespace RPGManagerLib.UI
{
    /// <summary>
    /// Minimal console menu helper that maps string keys to labeled actions with optional hints.
    /// </summary>
    public class MenuSystem
    {
        private readonly Dictionary<string, (string Description, string Hint, Action Action)> options = new();
        private readonly string title;

        /// <summary>
        /// Creates a new menu with a title header.
        /// </summary>
        /// <param name="menuTitle">Text displayed above the options.</param>
        public MenuSystem(string menuTitle)
        {
            title = menuTitle;
        }

        /// <summary>
        /// Adds or replaces an option in the menu.
        /// </summary>
        /// <param name="key">The trigger string entered by the user.</param>
        /// <param name="description">Short description shown in the list.</param>
        /// <param name="action">Action to execute when selected.</param>
        /// <param name="hint">Optional tagline shown after a dash on the same line.</param>
        public void AddOption(string key, string description, Action action, string hint = "")
        {
            options[key] = (description, hint, action);
        }

        /// <summary>
        /// Displays a menu with options and prompts the user to make a selection.
        /// </summary>
        /// <remarks>The menu is displayed in a loop until the user selects a valid option. Each option is
        /// displayed with its key and description. When a valid option is selected, the associated action is invoked,
        /// and the method exits. If an invalid choice is entered, the user is prompted to try again.</remarks>
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"--- {title} ---\n");
                // Match the padding to the longest label so the hint column stays aligned.
                int descriptionWidth = options.Values.Max(option => option.Description.Length) + 2;

                foreach (var opt in options)
                {
                    string desc = opt.Value.Description.PadRight(descriptionWidth);
                    string hint = opt.Value.Hint.Length > 0 ? $"— {opt.Value.Hint}" : "";
                    Console.WriteLine($"  [{opt.Key}] {desc}{hint}");
                }

                Console.Write("\n> ");
                string? choice = Console.ReadLine()?.Trim().ToLowerInvariant();

                if (choice is not null && options.ContainsKey(choice))
                {
                    options[choice].Action.Invoke();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice! Press ENTER to try again...");
                    Console.ReadLine();
                }
            }
        }
    }
}
