using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipStateTracker
{
    public class Player
    {
        public string Name { get; set; }
        public Board OwnBoard { get; set; }
        public Board FiringBoard { get; set; }
    }
}
