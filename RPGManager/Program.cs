using RPGManagerLib.UI;

namespace RPGManager
{
    internal class Program
    {
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
            GameMenu.Start();
        }
    }
}
