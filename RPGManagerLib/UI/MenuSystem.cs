using System;
using System.Collections.Generic;

namespace RPGManagerLib.UI
{
    /// <summary>
    /// Minimal console menu helper that maps string keys to actions.
    /// </summary>
    public class MenuSystem
    {
        private readonly Dictionary<string, (string Description, Action Action)> options = new();
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
        public void AddOption(string key, string description, Action action)
        {
            options[key] = (description, action);
        }

        /// <summary>
        /// Displays a menu with options and prompts the user to make a selection.
        /// </summary>
        /// <remarks>The menu is displayed in a loop until the user selects a valid option. Each option is
        /// displayed with its key and description. When a valid option is selected, the associated action is invoked,
        /// and the method exits. If an invalid choice is entered,  the user is prompted to try again.</remarks>
        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== {title} ===\n");

                foreach (var opt in options)
                {
                    Console.WriteLine($"{opt.Key}: {opt.Value.Description}");
                }

                Console.Write("\nChoose an option: ");
                string choice = Console.ReadLine()?.Trim().ToLower();

                if (options.ContainsKey(choice))
                {
                    options[choice].Action.Invoke();
                    break; // stops after 1 action; submenu can call Show() again
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
