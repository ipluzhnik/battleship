using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleshipStateTracker
{
    public class Battleship
    {
        public List<Square> Squares { get; private set; }
        public bool Destroyed { get; private set; }

        public void AddSquares(IEnumerable<Square> squares)
        {
            foreach (var item in squares)
            {
                item.IsEmpty = false;
            }
        }
        public bool TryToHit(int x, int y)
        {
            var square = Squares.Where(s => s.X == x && s.Y == y).FirstOrDefault();
            if (square!=null)
            {
                square.IsHit = true;
            }
            return Squares.All(s => s.IsHit);
        }
    }
}
