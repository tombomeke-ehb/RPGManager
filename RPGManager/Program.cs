using RPGManagerLib.Saves;
using RPGManagerLib.UI;

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
        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <param name="args">Optional command line arguments (unused).</param>
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            var characters = SaveManager.LoadCharacters();
            StartupScreens.Show(characters);
            GameMenu.Start();
        }
    }
}
