using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalBattlee.UnitActions;

namespace FinalBattlee.Units
{
    public abstract class Unit
    {

        public int ID { get; protected set; }
        public string Name { get; protected set; }
        public int MaxHP { get; protected set; }
        public int CurrentHP { get; protected set; }
        public Party Party { get; set; }
        //protected int _id;
        //protected string _name;
        //protected int _maxHP;
        //protected int _currentHP;

        private static int _lastID = 0;

        public List<UnitAction> Actions { get; protected set; }

        protected Unit()
        {
            ID = ++_lastID;                   // this doesn't work, I read this online and it's supposedly supposed to run on each child created, but doesn't seem to work at all
        }

        public virtual void TakeDamage(int damage)
        {
            CurrentHP -= damage;
            if (CurrentHP < 0) { CurrentHP = 0; };
        }

        public virtual void Heal(int heal)            // I actually used negative damage for healing earlier, but that's obviously not intuitive, so I'll add a proper Heal method here
        {
            CurrentHP += heal;
            if (CurrentHP > MaxHP) { CurrentHP = MaxHP; };
        }

        public static void AssignID(Unit unit) { unit.ID = _lastID + 1; _lastID++; }        // running this in constructor of unit DOES work

    }
}
