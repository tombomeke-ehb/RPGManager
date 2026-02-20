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
                var classMatch = Regex.Match(text, @"(?<!\/\/.*)(?<![A-Za-z0-9_])(?:public\s+|internal\s+|abstract\s+|sealed\s+|partial\s+)*class\s+([A-Za-z0-9_]+)");
                if (!classMatch.Success) return null;

                string nsMatch = Regex.Match(text, @"namespace\s+([A-ZaZd0-9_.]+)").Groups[1].Value.Trim();
                string ns = string.IsNullOrWhiteSpace(nsMatch) ? "Global" : nsMatch;
                string cls = classMatch.Groups[1].Value.Trim();

                // Extract XML Summary
                string summary = "";
                var summaryMatch = Regex.Match(text, @"///\s*<summary>\s*(.*?)\s*///\s*</summary>", RegexOptions.Singleline);
                if (summaryMatch.Success)
                {
                    summary = Regex.Replace(summaryMatch.Groups[1].Value, @"///\s*", "").Trim();
                    summary = Regex.Replace(summary, @"\s+", " "); // collapse multiple spaces/newlines
                }

                // Extract Base Class / Interfaces
                string inheritance = "";
                var inheritanceMatch = Regex.Match(text, $@"class\s+{cls}\s*:\s*([A-Za-z0-9_,\s]+)");
                if (inheritanceMatch.Success)
                {
                    inheritance = inheritanceMatch.Groups[1].Value.Trim();
                }

                var methods = Regex.Matches(text, @"public\s+(?:virtual\s+|override\s+|abstract\s+|static\s+|async\s+)*[A-Za-z0-9_<>,\[\]\s]+\s+([A-Za-z0-9_]+)\s*\(")
                    .Cast<Match>()
                    .Select(m => m.Groups[1].Value)
                    .Where(m => m != cls && m != "ToString" && m != "Equals" && m != "GetHashCode") // skip constructor and common overrides
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
                    Summary = summary,
                    Inheritance = inheritance,
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
            
            sw.WriteLine("### 📊 Codebase Stats");
            sw.WriteLine($"- **Namespaces:** {nsCount}");
            sw.WriteLine($"- **Classes:** {classCount}");
            sw.WriteLine($"- **Unique Methods:** {methodCount}");
            sw.WriteLine($"- **Pending TODOs:** {todoCount}\n");

            foreach (var nsGroup in files)
            {
                if (!nsGroup.Any()) continue;
                string nsEmoji = emojis[emojiIndex++ % emojis.Length];
                sw.WriteLine($"\n## {nsEmoji} {nsGroup.Key}\n");

                foreach (var file in nsGroup)
                {
                    sw.WriteLine($"### [{file.Class}.cs]({file.Link})");
                    
                    if (!string.IsNullOrEmpty(file.Inheritance))
                    {
                        sw.WriteLine($"*Inherits from: `{file.Inheritance}`*  ");
                    }

                    if (!string.IsNullOrEmpty(file.Summary))
                    {
                        sw.WriteLine($"> {file.Summary}\n");
                    }

                    if (file.Methods.Any())
                    {
                        sw.WriteLine("**Public Methods:**");
                        foreach (var m in file.Methods)
                            sw.WriteLine($"- `{m}()`");
                    }
                    else
                        sw.WriteLine("_No unique public methods found._");

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
