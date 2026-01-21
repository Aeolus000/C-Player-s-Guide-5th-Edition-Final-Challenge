using FinalBattlee.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBattlee.Items
{
    public abstract class Item
    {
        public string Name { get; protected set; }

        public abstract void Use(Unit target);
    }
}
