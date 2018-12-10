using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipStateTracker
{
    [Serializable]
    public class Game
    {
	   public string CurrentPlayer { get; private set; }
	   private string _firstPlayer;
	   private string _secondPlayer;

	   public Game()
	   {

	   }
	   public string Id { get; set; }
	   [JsonProperty()]
	   internal Dictionary<string, Board> Boards = new Dictionary<string, Board>();
	   public Game(string firstPlayer, string secondPlayer)
	   {
		  _firstPlayer = firstPlayer;
		  _secondPlayer = secondPlayer;
		  Boards.Add(firstPlayer, Board.CreateBoard(firstPlayer));
		  Boards.Add(secondPlayer, Board.CreateBoard(firstPlayer));
	   }
	   public List<string> Players
	   {
		  get
		  {
			 return new List<string> { _firstPlayer, _secondPlayer };
		  }
	   }
	   public void Initialize()
	   {
		  Boards = new Dictionary<string, Board>();
		  Boards.Add(_firstPlayer, Board.CreateBoard(_firstPlayer));
		  Boards.Add(_secondPlayer, Board.CreateBoard(_secondPlayer));
		  Id = Guid.NewGuid().ToString();
	   }
	   public HitResult AttackEnemy(string player, int x, int y)
	   {
		  return Boards[player].TryHit(x, y);
	   }
	   public bool PlayerHasShips(string player)
	   {
		  bool result = Boards[player].Battleships.Count > 0;
		  return result;
	   }

	   /// <summary>
	   /// Exposes specific player's board
	   /// </summary>
	   /// <param name="playerName"></param>
	   /// <returns></returns>
	   public Board GetPlayerBoard(string playerName)
	   {
		  return Boards[playerName];
	   }
    }
}
