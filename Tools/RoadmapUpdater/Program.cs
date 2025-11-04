using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RPGManager.Tools
{
    internal class Program
    {
        static void Main()
        {
            string root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../.."));
            string libPath = Path.Combine(root, "RPGManagerLib");
            string roadmapPath = Path.Combine(root, "ROADMAP.md");

            Console.WriteLine("🛠 Generating dynamic roadmap for RPGManagerLib...");

            if (!Directory.Exists(libPath))
            {
                Console.WriteLine("❌ RPGManagerLib directory not found at: " + libPath);
                return;
            }

            string repoUrl = "https://github.com/tombomeke-ehb/RPGManager/main/";
            var csFiles = Directory.GetFiles(libPath, "*.cs", SearchOption.AllDirectories);

            // --- analyze files safely ---
            var files = csFiles.Select(f =>
            {
                string text = File.ReadAllText(f);

                // match only true class definitions (ignore doc comments and words like "className")
                var classMatch = Regex.Match(text, @"(?<!\/\/.*)(?<![A-Za-z0-9_])class\s+([A-Za-z0-9_]+)");
                if (!classMatch.Success) return null;

                string ns = Regex.Match(text, @"namespace\s+([A-Za-z0-9_.]+)").Groups[1].Value.Trim();
                string cls = classMatch.Groups[1].Value.Trim();
                if (string.IsNullOrWhiteSpace(ns) || string.IsNullOrWhiteSpace(cls)) return null;

                var methods = Regex.Matches(text, @"public\s+[A-Za-z0-9_<>,\[\]\s]+\s+([A-Za-z0-9_]+)\s*\(")
                    .Cast<Match>()
                    .Select(m => m.Groups[1].Value)
                    .Where(m => m != cls) // skip constructor
                    .Distinct()
                    .ToList();

                var todos = Regex.Matches(text, @"//\s*TODO[: ](.*)", RegexOptions.IgnoreCase)
                    .Cast<Match>()
                    .Select(m => m.Groups[1].Value.Trim())
                    .ToList();

                string relativePath = Path.GetRelativePath(root, f).Replace("\\", "/");
                string fileLink = $"{repoUrl}{relativePath}";

                return new
                {
                    FileName = Path.GetFileName(f),
                    Namespace = ns,
                    Class = cls,
                    Methods = methods,
                    Todos = todos,
                    Link = fileLink
                };
            })
            .Where(x => x != null)
            .GroupBy(x => x.Namespace)
            .OrderBy(g => g.Key)
            .ToList();

            int nsCount = files.Count;
            int classCount = files.Sum(g => g.Count());
            int methodCount = files.Sum(g => g.SelectMany(c => c.Methods).Count());
            int todoCount = files.Sum(g => g.SelectMany(c => c.Todos).Count());

            // --- Emoji rotation ---
            string[] emojis = { "🧱", "⚔️", "📜", "🧙", "🏹", "🐉", "🏰", "🧭", "🪄", "🧰", "🎯" };
            int emojiIndex = 0;

            // Keep any manual content above the marker
            string manualSection = "";
            if (File.Exists(roadmapPath))
            {
                var existing = File.ReadAllText(roadmapPath);
                int markerIndex = existing.IndexOf("<!-- AUTO-GENERATED BELOW");
                if (markerIndex >= 0)
                    manualSection = existing[..markerIndex].TrimEnd() + "\n\n";
            }

            using var sw = new StreamWriter(roadmapPath, false);
            sw.WriteLine(manualSection);
            sw.WriteLine("<!-- AUTO-GENERATED BELOW – DO NOT EDIT -->");
            sw.WriteLine("\n# 🧮 Project Overview (auto-generated)\n");
            sw.WriteLine("> Automatically generated from RPGManagerLib source files.\n");
            sw.WriteLine($"_Last updated: **{DateTime.Now:yyyy-MM-dd HH:mm}**_\n");
            sw.WriteLine($"🧩 **{nsCount} Namespaces · {classCount} Classes · {methodCount} Methods · {todoCount} TODOs**\n");

            foreach (var nsGroup in files)
            {
                if (!nsGroup.Any()) continue;
                string nsEmoji = emojis[emojiIndex++ % emojis.Length];
                sw.WriteLine($"\n## {nsEmoji} {nsGroup.Key}\n");

                foreach (var file in nsGroup)
                {
                    sw.WriteLine($"### [{file.Class}.cs]({file.Link})");
                    if (file.Methods.Any())
                    {
                        sw.WriteLine("**Public Methods:**");
                        foreach (var m in file.Methods)
                            sw.WriteLine($"- `{m}()`");
                    }
                    else
                        sw.WriteLine("_No public methods found._");

                    if (file.Todos.Any())
                    {
                        sw.WriteLine("\n**TODOs:**");
                        foreach (var todo in file.Todos)
                            sw.WriteLine($"- [ ] {todo}");
                    }

                    sw.WriteLine();
                }
            }

            Console.WriteLine($"✅ Roadmap generated successfully → {roadmapPath}");
        }
    }
}
