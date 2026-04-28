using RPGManagerLib.Saves;
using RPGManagerLib.UI;
using System.Linq;

namespace RPGManager
{
    /// <summary>
    /// Console entry point for the RPG Manager demo application.
    /// </summary>
    /// <remarks>
    /// Displays a styled intro screen and launches the main game menu.
    /// </remarks>
    internal class Program
    {
        private const int IntroWidth = 68;
        private const int MinimumContentWidth = 24;

        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <param name="args">Optional command line arguments (unused).</param>
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;
            var characters = SaveManager.LoadCharacters();

            if (characters.Count > 0)
            {
                ShowWelcomeBackScreen(characters);
            }
            else
            {
                ShowIntroScreen();
            }

            GameMenu.Start();
        }

        private static void ShowIntroScreen()
        {
            Console.Clear();
            DrainPendingKeys();

            string version = System.Reflection.CustomAttributeExtensions
                .GetCustomAttribute<System.Reflection.AssemblyInformationalVersionAttribute>(
                    System.Reflection.Assembly.GetExecutingAssembly()
                )?.InformationalVersion ?? "dev";

            WriteCentered(new string('=', IntroWidth), ConsoleColor.DarkYellow);
            TypeCentered("R P G   M A N A G E R", ConsoleColor.Yellow, 18);
            WriteCentered(new string('=', IntroWidth), ConsoleColor.DarkYellow);
            Console.WriteLine();

            TypeCentered($"Version {version}", ConsoleColor.DarkGray, 8);
            TypeCentered("A Tombomeke Studios Production", ConsoleColor.DarkCyan, 10);
            WriteCentered("Press ESC to skip", ConsoleColor.DarkGray);
            Console.WriteLine();

            string[] storyLines =
            {
                "The world stands at the edge of chaos.",
                "Legends whisper of heroes long forgotten,",
                "and dark powers rising beyond the misty mountains.",
                "",
                "From the ashes of old kingdoms, new champions emerge.",
                "Their fate lies in your hands as the Keeper of Destiny,",
                "and you shall forge their path through war, magic, and time itself.",
                "",
                "Raise your banners, summon your courage...",
                "and let the tale begin."
            };

            // Remember the starting console width. If the user resizes the window during the animation
            // we'll stop the typing animation and render the remaining text immediately to avoid
            // visual reflow/jumps.
            int initialWidth = GetUsableWindowWidth();

            for (int i = 0; i < storyLines.Length; i++)
            {
                string line = storyLines[i];

                if (IsSkipRequested())
                {
                    RenderFullIntroBody(storyLines);
                    WaitForEnterRealm();
                    return;
                }

                // If the window was resized since the intro started, stop animating and render the
                // remaining lines immediately centered using the current console width. This avoids
                // lines shifting unexpectedly while typing.
                if (GetUsableWindowWidth() != initialWidth)
                {
                    RenderFullIntroBody(storyLines.Skip(i));
                    WaitForEnterRealm();
                    return;
                }

                if (line.Length == 0)
                {
                    Console.WriteLine();
                    DelayWithSkip(120);
                    continue;
                }

                ConsoleColor color = line.StartsWith("Raise") || line.StartsWith("and let")
                    ? ConsoleColor.Cyan
                    : ConsoleColor.Gray;

                TypeCentered(line, color, 14);
                DelayWithSkip(70);
            }

            RenderIntroFooter();
            WaitForEnterRealm();
        }

        private static void ShowWelcomeBackScreen(IReadOnlyList<RPGManagerLib.Characters.Heroes.Character> characters)
        {
            Console.Clear();
            DrainPendingKeys();
            string leadHero = characters[0].Name;
            string rosterLabel = characters.Count == 1 ? "1 hero stands ready." : $"{characters.Count} heroes stand ready.";

            WriteCentered(new string('=', IntroWidth), ConsoleColor.DarkYellow);
            WriteCentered("WELCOME BACK TO TAVARYN", ConsoleColor.Yellow);
            WriteCentered(new string('=', IntroWidth), ConsoleColor.DarkYellow);
            Console.WriteLine();
            WriteCentered($"{leadHero} still answers your call.", ConsoleColor.Gray);
            WriteCentered(rosterLabel, ConsoleColor.Gray);
            Console.WriteLine();
            WriteCentered("Press ENTER to continue your journey...", ConsoleColor.White);
            WaitForEnter();
            Console.Clear();
        }

        private static void RenderFullIntroBody(IEnumerable<string> storyLines)
        {
            Console.WriteLine();

            foreach (string line in storyLines)
            {
                if (line.Length == 0)
                {
                    Console.WriteLine();
                    continue;
                }

                ConsoleColor color = line.StartsWith("Raise") || line.StartsWith("and let")
                    ? ConsoleColor.Cyan
                    : ConsoleColor.Gray;

                WriteCentered(line, color);
            }

            RenderIntroFooter();
        }

        private static void RenderIntroFooter()
        {
            Console.WriteLine();
            WriteCentered(new string('=', IntroWidth), ConsoleColor.DarkYellow);
            Console.WriteLine();
            WriteCentered("Developed by Tombomeke Studios © 2025", ConsoleColor.DarkGray);
            WriteCentered("www.tombomeke.com", ConsoleColor.DarkGray);
            Console.WriteLine();
        }

        private static void WaitForEnterRealm()
        {
            DrainPendingKeys();
            WriteCentered("Press any key to enter the realm of Tavaryn...", ConsoleColor.White);
            Console.ReadKey(true);
            Console.Clear();
        }

        private static void WaitForEnter()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    return;
                }
            }
        }

        private static bool DelayWithSkip(int totalDelayMs)
        {
            const int SliceMs = 20;

            for (int elapsed = 0; elapsed < totalDelayMs; elapsed += SliceMs)
            {
                if (IsSkipRequested())
                {
                    return true;
                }

                Thread.Sleep(Math.Min(SliceMs, totalDelayMs - elapsed));
            }

            return false;
        }

        private static void TypeCentered(string text, ConsoleColor color, int delayMs)
        {
            foreach (string wrappedLine in WrapForConsole(text))
            {
                int leftPadding = GetCenteredPadding(wrappedLine);
                Console.Write(new string(' ', leftPadding));
                Console.ForegroundColor = color;

                foreach (char character in wrappedLine)
                {
                    Console.Write(character);

                    if (DelayWithSkip(delayMs))
                    {
                        Console.ResetColor();
                        Console.WriteLine();
                        return;
                    }
                }

                Console.ResetColor();
                Console.WriteLine();
            }
        }

        private static void WriteCentered(string text, ConsoleColor color)
        {
            foreach (string wrappedLine in WrapForConsole(text))
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"{new string(' ', GetCenteredPadding(wrappedLine))}{wrappedLine}");
                Console.ResetColor();
            }
        }

        private static int GetCenteredPadding(string text)
        {
            int width = GetUsableWindowWidth();
            return Math.Max((width - text.Length) / 2, 0);
        }

        private static IEnumerable<string> WrapForConsole(string text)
        {
            int maxWidth = Math.Max(Math.Min(GetUsableWindowWidth() - 2, IntroWidth), MinimumContentWidth);

            if (string.IsNullOrWhiteSpace(text) || text.Length <= maxWidth)
            {
                yield return text;
                yield break;
            }

            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string currentLine = string.Empty;

            foreach (string word in words)
            {
                string candidate = string.IsNullOrEmpty(currentLine) ? word : $"{currentLine} {word}";
                if (candidate.Length <= maxWidth)
                {
                    currentLine = candidate;
                    continue;
                }

                if (!string.IsNullOrEmpty(currentLine))
                {
                    yield return currentLine;
                }

                currentLine = word;
            }

            if (!string.IsNullOrEmpty(currentLine))
            {
                yield return currentLine;
            }
        }

        private static int GetUsableWindowWidth()
        {
            try
            {
                return Math.Max(Console.WindowWidth, MinimumContentWidth);
            }
            catch (IOException)
            {
                return IntroWidth + 4;
            }
        }

        private static bool IsSkipRequested()
        {
            if (!Console.KeyAvailable)
            {
                return false;
            }

            ConsoleKeyInfo key = Console.ReadKey(true);
            return key.Key == ConsoleKey.Escape;
        }

        private static void DrainPendingKeys()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }
        }
    }
}
