using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FinalBattlee.UnitActions;

namespace FinalBattlee.Units
{
    public class Skeleton : Unit
    {
        public Skeleton()
        {
            Name = "Skeleton";
            MaxHP = 5;
            CurrentHP = MaxHP;
            Actions = [ new DoNothing(), new BoneCrunch(), new UseItem() ];
            AssignID(this);

        }

    }
}
