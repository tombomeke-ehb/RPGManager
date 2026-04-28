namespace RPGManager
{
    /// <summary>
    /// Renders the startup and returning-player screens for the console application.
    /// </summary>
    internal static class StartupScreens
    {
        private const int IntroWidth = 68;
        private const int MinimumContentWidth = 24;
        private const string EnterRealmPrompt = "Press any key to enter the realm of Tavaryn...";

        /// <summary>
        /// Shows the appropriate startup screen before the main menu opens.
        /// </summary>
        /// <param name="characters">The currently loaded character roster.</param>
        public static void Show(IReadOnlyList<RPGManagerLib.Characters.Heroes.Character> characters)
        {
            if (characters.Count > 0)
            {
                ShowWelcomeBackScreen(characters);
            }
            else
            {
                ShowIntroScreen();
            }
        }

        private static void ShowIntroScreen()
        {
            Console.Clear();
            DrainPendingKeys();

            IntroLine[] introLines = BuildIntroLines(GetApplicationVersion(), GetIntroStoryLines());
            PlayIntro(introLines);
        }

        private static void PlayIntro(IReadOnlyList<IntroLine> introLines)
        {
            for (int i = 0; i < introLines.Count; i++)
            {
                if (IsSkipRequested())
                {
                    FinishIntro(introLines);
                    return;
                }

                IntroLine line = introLines[i];
                if (TryHandleSpacerLine(introLines, i, line))
                {
                    continue;
                }

                if (TryRenderAnimatedLine(introLines, i, line))
                {
                    return;
                }

                if (TryHandlePostDelay(introLines, line.PostDelayMs))
                {
                    return;
                }
            }

            WaitForEnterRealm(introLines);
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

        private static string GetApplicationVersion()
        {
            return System.Reflection.CustomAttributeExtensions
                .GetCustomAttribute<System.Reflection.AssemblyInformationalVersionAttribute>(
                    System.Reflection.Assembly.GetExecutingAssembly()
                )?.InformationalVersion ?? "dev";
        }

        private static string[] GetIntroStoryLines()
        {
            return
            [
                "The world stands at the edge of chaos.",
                "Legends whisper of heroes long forgotten,",
                "and dark powers rising beyond the misty mountains.",
                string.Empty,
                "From the ashes of old kingdoms, new champions emerge.",
                "Their fate lies in your hands as the Keeper of Destiny,",
                "and you shall forge their path through war, magic, and time itself.",
                string.Empty,
                "Raise your banners, summon your courage...",
                "and let the tale begin."
            ];
        }

        private static IntroLine[] BuildIntroLines(string version, IEnumerable<string> storyLines)
        {
            List<IntroLine> lines =
            [
                new(new string('=', IntroWidth), ConsoleColor.DarkYellow),
                new("R P G   M A N A G E R", ConsoleColor.Yellow, DelayMs: 18),
                new(new string('=', IntroWidth), ConsoleColor.DarkYellow),
                IntroLine.Spacer(),
                new($"Version {version}", ConsoleColor.DarkGray, DelayMs: 8),
                new("A Tombomeke Studios Production", ConsoleColor.DarkCyan, DelayMs: 10),
                new("Press ESC to skip", ConsoleColor.DarkGray),
                IntroLine.Spacer(),
                IntroLine.Spacer()
            ];

            foreach (string storyLine in storyLines)
            {
                if (storyLine.Length == 0)
                {
                    lines.Add(IntroLine.Spacer(postDelayMs: 120));
                    continue;
                }

                ConsoleColor color = storyLine.StartsWith("Raise") || storyLine.StartsWith("and let")
                    ? ConsoleColor.Cyan
                    : ConsoleColor.Gray;

                lines.Add(new IntroLine(storyLine, color, DelayMs: 14, PostDelayMs: 70));
            }

            lines.Add(IntroLine.Spacer());
            lines.Add(new IntroLine(new string('=', IntroWidth), ConsoleColor.DarkYellow));
            lines.Add(IntroLine.Spacer());
            lines.Add(new IntroLine("Developed by Tombomeke Studios © 2025", ConsoleColor.DarkGray));
            lines.Add(new IntroLine("www.tombomeke.com", ConsoleColor.DarkGray));
            lines.Add(IntroLine.Spacer());

            return lines.ToArray();
        }

        private static bool TryHandleSpacerLine(IReadOnlyList<IntroLine> introLines, int lineIndex, IntroLine line)
        {
            if (!line.IsSpacer)
            {
                return false;
            }

            return TryHandlePostDelay(introLines, line.PostDelayMs);
        }

        private static bool TryRenderAnimatedLine(IReadOnlyList<IntroLine> introLines, int lineIndex, IntroLine line)
        {
            if (line.DelayMs > 0)
            {
                if (AnimateCenteredLine(introLines, lineIndex))
                {
                    FinishIntro(introLines);
                    return true;
                }
            }
            else
            {
                WriteCentered(line.Text, line.Color);
            }

            return false;
        }

        private static bool TryHandlePostDelay(IReadOnlyList<IntroLine> introLines, int postDelayMs)
        {
            if (postDelayMs <= 0)
            {
                return false;
            }

            if (!DelayWithSkip(postDelayMs))
            {
                return false;
            }

            FinishIntro(introLines);
            return true;
        }

        private static void FinishIntro(IReadOnlyList<IntroLine> introLines)
        {
            RenderIntroFrame(introLines, completedLineCount: introLines.Count);
            WaitForEnterRealm(introLines);
        }

        private static void RenderIntroFrame(
            IReadOnlyList<IntroLine> introLines,
            int completedLineCount,
            bool includePrompt = false)
        {
            Console.Clear();

            for (int i = 0; i < completedLineCount && i < introLines.Count; i++)
            {
                RenderIntroLine(introLines[i]);
            }

            if (includePrompt)
            {
                WriteCentered("Press any key to enter the realm of Tavaryn...", ConsoleColor.White);
            }
        }

        private static void RenderIntroLine(IntroLine line)
        {
            if (line.IsSpacer)
            {
                Console.WriteLine();
                return;
            }

            WriteCentered(line.Text, line.Color);
        }

        private static bool AnimateCenteredLine(IReadOnlyList<IntroLine> introLines, int lineIndex)
        {
            IntroLine line = introLines[lineIndex];
            while (true)
            {
                int lastWidth = GetUsableWindowWidth();
                bool shouldRestartLine = false;

                foreach (string wrappedLine in WrapForConsole(line.Text))
                {
                    if (TryAnimateWrappedLine(introLines, lineIndex, line, wrappedLine, lastWidth, out shouldRestartLine))
                    {
                        return true;
                    }

                    if (shouldRestartLine)
                    {
                        break;
                    }
                }

                if (!shouldRestartLine && GetUsableWindowWidth() == lastWidth)
                {
                    return false;
                }
            }
        }

        private static bool TryAnimateWrappedLine(
            IReadOnlyList<IntroLine> introLines,
            int lineIndex,
            IntroLine line,
            string wrappedLine,
            int lastWidth,
            out bool shouldRestartLine)
        {
            shouldRestartLine = false;

            int leftPadding = GetCenteredPadding(wrappedLine);
            Console.Write(new string(' ', leftPadding));
            Console.ForegroundColor = line.Color;

            foreach (char character in wrappedLine)
            {
                if (IsSkipRequested())
                {
                    Console.ResetColor();
                    Console.WriteLine();
                    return true;
                }

                if (HasWindowWidthChanged(lastWidth))
                {
                    ResetAnimatedLineAfterResize(introLines, lineIndex);
                    shouldRestartLine = true;
                    return false;
                }

                Console.Write(character);

                if (DelayWithSkip(line.DelayMs))
                {
                    Console.ResetColor();
                    Console.WriteLine();
                    return true;
                }
            }

            Console.ResetColor();
            Console.WriteLine();
            return false;
        }

        private static void WaitForEnterRealm(IReadOnlyList<IntroLine> introLines)
        {
            DrainPendingKeys();
            RenderIntroFrame(introLines, introLines.Count, includePrompt: true);

            int lastWidth = GetUsableWindowWidth();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    Console.Clear();
                    return;
                }

                int currentWidth = GetUsableWindowWidth();
                if (currentWidth != lastWidth)
                {
                    lastWidth = currentWidth;
                    RenderEnterRealmPrompt(introLines);
                }

                Thread.Sleep(50);
            }
        }

        private static void RenderEnterRealmPrompt(IReadOnlyList<IntroLine> introLines)
        {
            RenderIntroFrame(introLines, introLines.Count, includePrompt: true);
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

        private static bool HasWindowWidthChanged(int previousWidth)
        {
            return GetUsableWindowWidth() != previousWidth;
        }

        private static void ResetAnimatedLineAfterResize(IReadOnlyList<IntroLine> introLines, int lineIndex)
        {
            Console.ResetColor();
            Console.WriteLine();
            RenderIntroFrame(introLines, completedLineCount: lineIndex);
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

        private readonly record struct IntroLine(
            string Text,
            ConsoleColor Color,
            int DelayMs = 0,
            int PostDelayMs = 0,
            bool IsSpacer = false)
        {
            public static IntroLine Spacer(int postDelayMs = 0) =>
                new(string.Empty, ConsoleColor.Gray, PostDelayMs: postDelayMs, IsSpacer: true);
        }
    }
}
