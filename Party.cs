using FinalBattlee.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalBattlee.Items;

namespace FinalBattlee
{
    public class Party
    {
        public string PartyName { get; }

        private List<Unit> _units = new List<Unit>();
        
        public Player PlayerOwner { get; private set; }
        public List<Item> Inventory { get; private set; }

        public IReadOnlyList<Unit> Units => _units;         // yes I looked this up

        public Party(string partyName, Player player)
        {
            PartyName = partyName;
            PlayerOwner = player;

            Inventory = new List<Item>();
            Inventory.Add(new HealthPotion());
        }

        public void Add(Unit unit) { _units.Add(unit); unit.Party = this; }
        public void Remove(Unit unit) { _units.Remove(unit); }

    }
}
