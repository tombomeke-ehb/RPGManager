using System;
using System.Collections.Generic;

namespace RPGManagerLib.UI
{
    public class MenuSystem
    {
        private readonly Dictionary<string, (string Description, Action Action)> options = new();
        private readonly string title;

        public MenuSystem(string menuTitle)
        {
            title = menuTitle;
        }

        public void AddOption(string key, string description, Action action)
        {
            options[key] = (description, action);
        }

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
                    break; // stop na 1 actie; submenu kan Show() opnieuw aanroepen
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
