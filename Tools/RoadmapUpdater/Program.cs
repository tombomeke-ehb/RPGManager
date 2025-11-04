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
            // Locate the RPGManagerLib folder (4 levels up from bin/Debug/net8.0)
            string root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../.."));
            string libPath = Path.Combine(root, "RPGManagerLib");
            string roadmapPath = Path.Combine(root, "ROADMAP.md");

            Console.WriteLine("🛠 Generating dynamic roadmap for RPGManagerLib...");

            if (!Directory.Exists(libPath))
            {
                Console.WriteLine("❌ RPGManagerLib directory not found at: " + libPath);
                return;
            }

            var csFiles = Directory.GetFiles(libPath, "*.cs", SearchOption.AllDirectories);

            var groupedByNamespace = csFiles
                .Select(f =>
                {
                    string text = File.ReadAllText(f);
                    string ns = Regex.Match(text, @"namespace\s+([A-Za-z0-9_.]+)").Groups[1].Value;
                    string cls = Regex.Match(text, @"class\s+([A-Za-z0-9_]+)").Groups[1].Value;
                    var methods = Regex.Matches(text, @"public\s+[A-Za-z0-9_<>,\[\]]+\s+([A-Za-z0-9_]+)\s*\(")
                        .Select(m => m.Groups[1].Value).Distinct().ToList();
                    var todos = Regex.Matches(text, @"//\s*TODO[: ](.*)", RegexOptions.IgnoreCase)
                        .Select(m => m.Groups[1].Value.Trim()).ToList();
                    return new { File = Path.GetFileName(f), Namespace = ns, Class = cls, Methods = methods, Todos = todos };
                })
                .Where(e => !string.IsNullOrWhiteSpace(e.Namespace))
                .GroupBy(e => e.Namespace)
                .OrderBy(g => g.Key);

            using var sw = new StreamWriter(roadmapPath, false);
            sw.WriteLine("# 🗺️ RPG Manager – Dynamic Roadmap\n");
            sw.WriteLine("> Automatically generated from RPGManagerLib source files.\n");
            sw.WriteLine($"_Last updated: **{DateTime.Now:yyyy-MM-dd HH:mm}**_\n");

            foreach (var nsGroup in groupedByNamespace)
            {
                sw.WriteLine($"\n## 📦 {nsGroup.Key}\n");

                foreach (var file in nsGroup)
                {
                    if (string.IsNullOrWhiteSpace(file.Class)) continue;

                    sw.WriteLine($"### 🧱 {file.Class}.cs");
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
