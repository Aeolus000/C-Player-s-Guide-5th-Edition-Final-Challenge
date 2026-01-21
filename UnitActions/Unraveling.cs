using FinalBattlee.Units;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalBattlee.UnitActions
{
    public class Unraveling : UnitAction
    {

        public Unraveling()
        {
            Name = "Unraveling";
        }
        public override void RunAction(Unit owner, Unit target)
        {
            Random random = new Random();
            int damage = random.Next(3);
            target.TakeDamage(damage);

            Console.WriteLine($"\n{owner.Name} used {Name} on {target.Name}");
            Console.WriteLine($"{target.Name} took {damage} damage!\n");
        }
    }
}
