using System.Text.Json;
using System.Text.Json.Serialization;
using RPGManagerLib.Characters.Heroes;

namespace RPGManagerLib.Saves
{
    /// <summary>
    /// Provides static methods for saving and loading character data in JSON format.
    /// </summary>
    /// <remarks>The SaveManager class manages the persistence of character data by reading from and writing
    /// to a JSON file in a designated save directory. It ensures that the save directory exists before saving and
    /// handles errors gracefully during file operations. The class supports serialization of enums as strings and
    /// formats JSON output for readability. All operations are performed on a predefined file location, and existing
    /// files are overwritten when saving.</remarks>
    public static class SaveManager
    {
        // TODO: Promote the root save payload from List<CharacterSaveData> to a SaveGame object when WorldState and roster data are added.
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
            try
            {
                if (!File.Exists(SaveFile))
                    return new List<Character>();

                string json = File.ReadAllText(SaveFile);

                if (string.IsNullOrWhiteSpace(json))
                    return new List<Character>();

                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter() },
                    PropertyNameCaseInsensitive = true
                };

                // TODO: Load schema version, WorldState, and roster metadata here once the save format expands beyond character snapshots.
                var saveDataList = JsonSerializer.Deserialize<List<CharacterSaveData>>(json, options)
                    ?? new List<CharacterSaveData>();

                return saveDataList.Select(sd => sd.ToCharacter()).ToList();
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Save file contains invalid JSON. Starting with empty character list.");
                //Console.WriteLine($"Details: {ex.Message}"); Debugging
                return new List<Character>();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading save file.");
                //Console.WriteLine($"Details: {ex.Message}"); Debugging
                return new List<Character>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error while loading save file.");
                //Console.WriteLine($"Details: {ex.Message}"); Debugging
                return new List<Character>();
            }
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

            // TODO: Serialize WorldState, roster unlocks, and future save-version metadata alongside characters.
            string json = JsonSerializer.Serialize(saveData, options);
            File.WriteAllText(SaveFile, json);
            Console.WriteLine($"Saved to: {SaveFile}");
        }
    }
}
