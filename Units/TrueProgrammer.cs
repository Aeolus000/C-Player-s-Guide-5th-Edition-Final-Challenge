using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalBattlee.UnitActions;

namespace FinalBattlee.Units
{
    public class TrueProgrammer : Unit
    {
        public TrueProgrammer(string _name = "True Programmer", int _maxHP = 25)
        {
            Name = _name;
            MaxHP = _maxHP;
            CurrentHP = MaxHP;
            Actions = [new DoNothing(), new Punch(), new UseItem() ];
        }

    }
}
