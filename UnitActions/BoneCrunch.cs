using FinalBattlee.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBattlee.UnitActions
{
    public class BoneCrunch : UnitAction
    {
        public BoneCrunch()
        {
            Name = "Bone Crunch";
        }

        public override void RunAction(Unit owner, Unit target)
        {
            Console.WriteLine($"\n{owner.Name} used BONE CRUNCH on {target.Name}!");

            Random rng = new Random();
            if (rng.Next(2) == 0) { Console.WriteLine($"\n{owner.Name} MISSED!\n"); return; }
            target.TakeDamage(1);
            Console.WriteLine($"{target.Name} took 1 damage!\n");
        }
    }
}
