using System.Text.Json;
using System.Text.Json.Serialization;
using RPGManagerLib.Characters;

namespace RPGManagerLib.Saves
{
    public static class SaveManager
    {
        private const string SaveFolder = "Save";
        private const string SaveFileName = "characters.json";
        private static readonly string SaveFile = Path.Combine(SaveFolder, SaveFileName);

        public static List<Character> LoadCharacters()
        {
            if (!File.Exists(SaveFile))
                return new List<Character>();

            string json = File.ReadAllText(SaveFile);
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                PropertyNameCaseInsensitive = true
            };

            var saveDataList = JsonSerializer.Deserialize<List<CharacterSaveData>>(json, options)
                ?? new List<CharacterSaveData>();

            return saveDataList.Select(sd => sd.ToCharacter()).ToList();
        }

        public static void SaveCharacters(List<Character> characters)
        {
            if (!Directory.Exists(SaveFolder))
                Directory.CreateDirectory(SaveFolder);

            var saveData = characters.Select(c => new CharacterSaveData(c)).ToList();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };

            string json = JsonSerializer.Serialize(saveData, options);
            File.WriteAllText(SaveFile, json);
            Console.WriteLine($"Saved to: {SaveFile}");
        }
    }
}
