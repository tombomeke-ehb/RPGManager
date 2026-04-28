using RPGManagerLib.Items.Weapons;

namespace RPGManagerLib.Items.Staffs
{
    internal class WindStaff : Staff
    {
        public WindStaff()
            : base(damageAmount: 3,
                  durability: 100,
                  rarity: Rarity.COMMON,
                  level: 1,
                  name: "Wind Staff",
                  element: Element.WIND,
                  variant: StaffVariant.WIND)
        { }
    }

    // Will probably later expand this to have a unique effect, but for now it's just a reskinned basic staff with a different element.
    // Light also rework the way this staff either gets made/gotten by either making a system where you randomly get a certain magic type after creating character, or where you can choose one
    // Or that you can make this staff or get it through a quest or something and that it empowers your spells of the same kind of element.
}
