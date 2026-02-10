using RPGManagerLib.Locations;

namespace RPGManagerLib.Characters.NPCs
{
    public class BlackSmith : NPC
    {
        public BlackSmith(Location location)
            : base("Black Smith", location)
        {
        }

        public override void Interact()
        {
            System.Console.WriteLine("The blacksmith looks up from the forge but has nothing to say right now.");
        }

        public override void Trade()
        {
            System.Console.WriteLine("The blacksmith's shop is not ready for trading yet.");
        }
    }
}
