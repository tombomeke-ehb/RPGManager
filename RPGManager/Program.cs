using RPGManagerLib.Characters.NPCs;
using RPGManagerLib.Items.Weapons;
using RPGManagerLib.Items.Weapons.Bows;
using RPGManagerLib.Items.Weapons.Melee;
using RPGManagerLib.Locations;
using RPGManagerLib.UI;

namespace RPGManager
{
    /// <summary>
    /// Console entry point for the RPG Manager demo application.
    /// </summary>
    /// <remarks>
    /// Displays a styled intro screen and runs sample code to exercise
    /// the underlying library. Hook up to <c>GameMenu.Start()</c> to
    /// launch the interactive flow when ready.
    /// </remarks>
    internal class Program
    {
        /// <summary>
        /// Application entry point.
        /// </summary>
        /// <param name="args">Optional command line arguments (unused).</param>
        static void Main(string[] args)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("====================================================================");
            Console.WriteLine("                        R P G   M A N A G E R");
            Console.WriteLine("====================================================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("                     A Tombomeke Studios Production");
            Console.ResetColor();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("The world stands at the edge of chaos.");
            Console.WriteLine("Legends whisper of heroes long forgotten,");
            Console.WriteLine("and dark powers rising beyond the misty mountains.");
            Console.WriteLine();
            Console.WriteLine("From the ashes of old kingdoms, new champions emerge.");
            Console.WriteLine("Their fate lies in your hands — as the Keeper of Destiny,");
            Console.WriteLine("you shall forge their path through war, magic, and time itself.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Raise your banners, summon your courage...");
            Console.WriteLine("and let the tale begin.");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("====================================================================");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("                Developed by Tombomeke Studios © 2025");
            Console.WriteLine("                     www.tombomeke.com");
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("Press any key to enter the realm...");
            Console.ReadKey(true);
            Console.Clear();

            // Launch the interactive game menu
            GameMenu.Start();
        }
    }
}
