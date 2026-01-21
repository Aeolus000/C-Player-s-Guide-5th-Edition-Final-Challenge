using FinalBattlee.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalBattlee.UnitActions
{
    public class Punch : UnitAction
    {
        public Punch()
        {
            Name = "Punch";
        }

        public override void RunAction(Unit owner, Unit target)
        {
            Console.WriteLine($"\n{owner.Name} used {Name} on {target.Name}!");         
            target.TakeDamage(1);
            Console.WriteLine($"{target.Name} took 1 damage!\n");
        }
    }
}
