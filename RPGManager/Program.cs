using RPGManagerLib;

namespace RPGManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my RPGManager");

            Character character = new Character(56);

            Console.WriteLine(character);

            character.Heal(20);
            Console.WriteLine(character);
            character.Damage(50);
        }
    }
}
