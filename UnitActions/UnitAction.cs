using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalBattlee.Units;

namespace FinalBattlee.UnitActions
{
    public abstract class UnitAction
    {
        public string Name { get; protected set; }

        public abstract void RunAction(Unit owner, Unit target);       // I could have this return a string so I can have the UI print stuff about it. Or I could make a logger, I guess.
    }
}
