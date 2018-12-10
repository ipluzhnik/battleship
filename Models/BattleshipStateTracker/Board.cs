using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleshipStateTracker
{
    public class Board
    {
        public int Height { get; } = 10;//Background #2: Each have a 10x10 board
        public int Width { get; } = 10;//DITTO
	   internal List<Square> Squares { get; } = new List<Square>();

	   public List<Battleship> Battleships { get; } = new List<Battleship>();

	   public string Player { get; private set; }

	   public static Board CreateBoard(string playerName)
        {
            var board = new Board { Player = playerName };
            for (int x = 0; x < board.Height; x++)
            {
                for (int y = 0; y < board.Width; y++)
                {
                    board.Squares.Add(new Square(x, y) { IsEmpty = true });
                }
            }
            return board;
        }

        /// <summary>
        /// Creates a new battleship either manually or authomatically finding ther largest empty slot
        /// </summary>
        /// <param name="random"></param>
        /// <param name="alingment"></param>
        /// <param name="size"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
	   public Battleship CreateBattleship(bool random, Alingment alingment=Alingment.Horizontal, int size =-1, int x = -1, int y = -1)
        {
          Battleship bs ;
		  if (random)//if random find the largest Battleship that we can allocate to the board
		  {
			 if (alingment==Alingment.Horizontal)
			 {
				bs=FindLargestHorizontalShip();
			 }
			 else
			 {
				bs=FindLargestVerticalShip();
			 }
		  }
		  else
		  {
                bs = BuildBattleship(alingment,size,x,y);
		  }
		  if (bs!=null)
		  {
			 Battleships.Add(bs);
		  }
            return bs;
        }

        private Battleship BuildBattleship(Alingment alingment, int size, int x, int y)
        {
            var bs = new Battleship();
            if (Squares.Where(s => s.X == x && s.Y == y).FirstOrDefault() == null
                || Squares.Where(s => (s.X == x + size - 1 && s.Y == y && alingment == Alingment.Horizontal) ||
                (s.Y == y + size - 1 && s.X == x && alingment == Alingment.Vertical)).FirstOrDefault() == null)
            {
                bs = null;
            }
            else
            {
                if (alingment == Alingment.Horizontal)
                {
                    if (Squares.Any(s => (s.Y == y && (s.X >= x && s.X <= x + size - 1)) && !s.IsEmpty))
                    {
                        bs = null;
                    }
                    else
                    {
                        bs.AddSquares(Squares.Where(s => (s.Y == y && (s.X >= x && s.X <= x + size))));
                    }
                }
                else
                {
                    if (Squares.Any(s => (s.X == x && (s.Y >= y && s.Y <= y + size - 1)) && !s.IsEmpty))
                    {
                        bs = null;
                    }
                    else
                    {
                        bs.AddSquares(Squares.Where(s => (s.X == x && (s.Y >= y && s.Y <= y + size - 1))));
                    }
                }
            }
            return bs;
        }

        public HitResult TryHit(int x, int y)
	   {
		  Battleship destroyed = null;
		  var result = HitResult.Missed;
		  foreach (var bs in Battleships)
		  {
			 result = bs.TryToHit(x, y);
			 if (result==HitResult.Destroyed)
			 {
				destroyed = bs;
			 }
			 if (result !=HitResult.Missed)
			 {
				break;
			 }
		  }
		  if (destroyed!=null)
		  {
			 Battleships.Remove(destroyed);
		  }
		  return result;
	   }
	   private Battleship FindLargestVerticalShip()
        {
		  int size = 0;
		  Battleship battleship = null;
		  for (int x = 0; x < Width; x++)
		  {
			 for (int y = 0; y < Height; y++)
			 {
				if (Squares.Where(s => s.X == x && s.Y == y).First().IsEmpty)
				{
				    for (int i = y + 1; i < Height; i++)
				    {
					   if (Squares.Where(s => s.Y == i && s.X == x).First().IsEmpty && size <= i - y)
					   {
						  battleship = new Battleship();
						  battleship.AddSquares(Squares.Where(s => s.X == x && s.Y>=y && s.Y <= i));
						  size = battleship.Squares.Count;
					   }
				    }
				}
			 }
		  }
		  return battleship;
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
						  size = battleship.Squares.Count;
                            }
                        }
                    }
                }
            }
            return battleship;
        }
    }
}
