using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipStateTracker
{
    public class Board
    {
        public int Height { get; } = 10;//Background #2: Each have a 10x10 board
        public int Width { get; } = 10;//DITTO
        public List<Square> Squares{get; private set;}
        public List<Battleship> Battleships { get; private set;}
        public string Player { get; private set; }
        public static Board CreateBoard(string playerName)
        {
            var board = new Board { Player = playerName };
            for (int x = 0; x < board.Height; x++)
            {
                for (int y = 0; y < board.Width; y++)
                {
                    board.Squares.Add(new Square(x, y));
                }
            }
            return board;
        }
        public Battleship CreateBattleship(bool random, Alingment alingment=Alingment.Horizontal, int size =-1, int x = -1, int y = -1)
        {
            var bs = new Battleship();
            if (random)//if random find the largest Battleship that we can allocate to the board
            {
                var bsHorizontal = FindLargestHorizontalShip();
                var bsVertical = FindLargestVerticalShip();
                return bsHorizontal;
            }
            return bs;
        }

        private Battleship FindLargestVerticalShip()
        {
            return null;
        }

        private Battleship FindLargestHorizontalShip()
        {
            //the algorithm: 1iterate through each row cell by cell.
            //once empty cell is found move to the righ until the the cell is not empty of the board edge is reached
            //keep track of sizes. If the size is larger than the previous one create new battleship
            //return largest battleship
            int size = 0;
            Battleship battleship=null;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Squares.Where(s => s.X == x && s.Y == y).First().IsEmpty)
                    {
                        for (int i = x+1; i < Width; i++)
                        {
                            if (Squares.Where(s => s.X == i && s.Y == y).First().IsEmpty && size<i-x)
                            {
                                battleship = new Battleship();
                                battleship.AddSquares(Squares.Where(s =>s.Y==y && s.X<=i));
                            }
                        }
                    }
                }
            }
            return battleship;
        }
    }
}
