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

            var csFiles = Directory.GetFiles(libPath, "*.cs", SearchOption.AllDirectories);

            // 🧩 Improved processing block
            var validFiles = csFiles
                .Select(f =>
                {
                    string text = File.ReadAllText(f);
                    string ns = Regex.Match(text, @"namespace\s+([A-Za-z0-9_.]+)").Groups[1].Value;
                    string cls = Regex.Match(text, @"class\s+([A-Za-z0-9_]+)").Groups[1].Value;
                    if (string.IsNullOrWhiteSpace(cls)) return null; // skip files without classes

                    var methods = Regex.Matches(text, @"public\s+[A-Za-z0-9_<>,\[\]]+\s+([A-Za-z0-9_]+)\s*\(")
                        .Select(m => m.Groups[1].Value).Distinct().ToList();
                    var todos = Regex.Matches(text, @"//\s*TODO[: ](.*)", RegexOptions.IgnoreCase)
                        .Select(m => m.Groups[1].Value.Trim()).ToList();
                    return new { File = Path.GetFileName(f), Namespace = ns, Class = cls, Methods = methods, Todos = todos };
                })
                .Where(e => e != null && !string.IsNullOrWhiteSpace(e.Namespace))
                .GroupBy(e => e.Namespace)
                .OrderBy(g => g.Key);

            // 📊 Summary counters
            int nsCount = validFiles.Count();
            int classCount = validFiles.Sum(g => g.Count());
            int methodCount = validFiles.Sum(g => g.SelectMany(c => c.Methods).Count());
            int todoCount = validFiles.Sum(g => g.SelectMany(c => c.Todos).Count());

            using var sw = new StreamWriter(roadmapPath, false);
            sw.WriteLine("# 🗺️ RPG Manager – Dynamic Roadmap\n");
            sw.WriteLine("> Automatically generated from RPGManagerLib source files.\n");
            sw.WriteLine($"_Last updated: **{DateTime.Now:yyyy-MM-dd HH:mm}**_\n");
            sw.WriteLine($"🧩 **{nsCount} Namespaces · {classCount} Classes · {methodCount} Methods · {todoCount} TODOs**\n");

            foreach (var nsGroup in validFiles)
            {
                if (!nsGroup.Any()) continue;

                sw.WriteLine($"\n## 📦 {nsGroup.Key}\n");

                foreach (var file in nsGroup)
                {
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
