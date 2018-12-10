using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipStateTracker
{
    public class Square
    {
        public Square(int x, int y)
        {
            //initialise the square with coordinates passed by factory class
            Y = y;
            X = x;
        }
        public bool IsHit { get; set; }
        public bool IsEmpty { get; set; }
        public int Y { get; }
        public int X { get; }
        
    }
}
