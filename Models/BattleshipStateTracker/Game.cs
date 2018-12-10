using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleshipStateTracker
{
    [Serializable]
    public class Game
    {
	   public string CurrentPlayer { get; private set; }
        public List<Player> Players { get; set; }
	   public Game()
	   {

	   }
	   public string Id { get; set; }
	   public Game(string firstPlayer, string secondPlayer)
	   {
            Players = new List<Player> {
                new Player { Name = firstPlayer, OwnBoard = Board.CreateBoard(firstPlayer), FiringBoard = Board.CreateBoard(secondPlayer) },
                new Player { Name = secondPlayer, OwnBoard = Board.CreateBoard(secondPlayer), FiringBoard = Board.CreateBoard(firstPlayer) }
            };
	   }

        public void Initialize()
        {
            string firstPlayer = "";
            string secondPlayer = "";
            if (!string.IsNullOrEmpty(Id))
            {
                firstPlayer = Players[0].Name;
                secondPlayer = Players[1].Name;
                Players = new List<Player> {
                    new Player { Name = firstPlayer, OwnBoard = Board.CreateBoard(firstPlayer), FiringBoard = Board.CreateBoard(secondPlayer) },
                    new Player { Name = secondPlayer, OwnBoard = Board.CreateBoard(secondPlayer), FiringBoard = Board.CreateBoard(firstPlayer) }
                };
            }

            Id = Guid.NewGuid().ToString();
        }
        public HitResult AttackEnemy(string player, int x, int y)
        {
            var result = Players.Single(p => p.Name == player).OwnBoard.TryHit(x, y);
            Players.Single(p => p.Name != player).FiringBoard.Squares.Single(s => s.X == x && s.Y == y).IsHit = result != HitResult.Missed;
            return result;
        }
        public bool PlayerHasShips(string playerName)
        {
            bool result = Players.Single(p => p.Name == playerName).OwnBoard.Battleships.Count > 0;
            return result;
        }

	   /// <summary>
	   /// Exposes specific player's board
	   /// </summary>
	   /// <param name="playerName"></param>
	   /// <returns></returns>
	   public Board GetPlayerBoard(string playerName)
	   {
		  return Players.Single(p=>p.Name==playerName).OwnBoard;
	   }
    }
}
