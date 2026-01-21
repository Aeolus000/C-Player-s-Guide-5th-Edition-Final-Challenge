using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalBattlee.UnitActions;

namespace FinalBattlee.Units
{
    public class UncodedOne : Unit
    {
        public UncodedOne(string name = "The Uncoded One", int maxHP = 15)
        {
            Name = name;
            MaxHP = maxHP;
            CurrentHP = MaxHP;
            Actions = [ new DoNothing(), new Unraveling(), new UseItem() ];
        }

    }
}
