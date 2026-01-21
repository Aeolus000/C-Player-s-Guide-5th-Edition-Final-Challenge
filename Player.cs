using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBattlee
{
    public class Player
    {
        public PlayerType PlayerType { get; private set; }
        public string Name { get; private set; }

        public Player(PlayerType type = PlayerType.Human)
        {
            //Name = name;                            // not sure the players even need a name tbh, it will never really come up
            PlayerType = type;
        }
    }
    

    public enum PlayerType { Human, Computer }
}
