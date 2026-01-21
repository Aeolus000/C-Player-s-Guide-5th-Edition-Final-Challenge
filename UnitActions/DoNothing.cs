using FinalBattlee.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FinalBattlee.UnitActions
{
    public class DoNothing : UnitAction
    {
        public DoNothing()
        {
            Name = "Do Nothing";
        }

        public override void RunAction(Unit owner, Unit target) => Console.WriteLine($"\n{owner.Name} did NOTHING\n");
    }
}
