using System.Text.Json;
using System.Text.Json.Serialization;
using RPGManagerLib.Characters.Heroes;

namespace RPGManagerLib.Saves
{
    public static class SaveManager
    {
        private static readonly string SaveFolder =
            string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("RPGMANAGER_SAVE_DIR"))
                ? "Save"
                : Environment.GetEnvironmentVariable("RPGMANAGER_SAVE_DIR")!;
        private const string SaveFileName = "characters.json";
        private static readonly string SaveFile = Path.Combine(SaveFolder, SaveFileName);

        /// <summary>
        /// Loads a list of characters from a saved file.
        /// </summary>
        /// <remarks>This method reads character data from a JSON file specified by the <c>SaveFile</c>
        /// path.  If the file does not exist, an empty list is returned. The JSON data is deserialized into  a list of
        /// <see cref="CharacterSaveData"/> objects, which are then converted to <see cref="Character"/>
        /// objects.</remarks>
        /// <returns>A list of <see cref="Character"/> objects loaded from the saved file. If the file does not exist  or the
        /// data is invalid, an empty list is returned.</returns>
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


        /// <summary>
        /// Saves a list of characters to a file in JSON format.
        /// </summary>
        /// <remarks>The method ensures that the save directory exists before writing the file.  The JSON
        /// output is formatted with indentation for readability and includes support for serializing enums as strings.
        /// The save file is written to a predefined location, and any existing file at that location will be
        /// overwritten.</remarks>
        /// <param name="characters">The list of characters to save. Each character is serialized into a structured JSON format.</param>
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

        /// <summary>
        /// Saves or updates a single character by name, preserving other characters in the save file.
        /// </summary>
        public static void SaveOrUpdateCharacter(Character character)
        {
            var existing = LoadCharacters();
            int idx = existing.FindIndex(c => string.Equals(c.Name, character.Name, StringComparison.OrdinalIgnoreCase));
            if (idx >= 0) existing[idx] = character; else existing.Add(character);
            SaveCharacters(existing);
        }
    }
}
