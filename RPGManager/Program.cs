using RPGManagerLib;

namespace RPGManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my RPGManager");

            Character character = new("Tom", 100, DateTime.Now, 1);

            character.Heal(10);

            character.Damage(20);
        }
    }
}
