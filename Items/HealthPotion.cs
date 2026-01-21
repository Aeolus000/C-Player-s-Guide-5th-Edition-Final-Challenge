using FinalBattlee.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBattlee.Items
{
    public class HealthPotion : Item
    {
        public HealthPotion()
        {
            Name = "Health Potion";
        }

        public override void Use(Unit target)
        {
            target.Heal(5);

            Console.WriteLine($"\n{Name} was used to heal 5 HP on {target.Name}!");
            Console.WriteLine($"\n{target.Name} now has {target.CurrentHP} / {target.MaxHP}!");
        }
    }
}
