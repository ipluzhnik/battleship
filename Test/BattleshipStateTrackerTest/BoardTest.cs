using BattleshipStateTracker;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace BattleshipStateTrackerTest
{
    [TestClass]
    public class BoardTest
    {
	   private Game _game;
	   private readonly string _firstPlayer = "bob";
	   private readonly string _secondPlayer = "suzie";

	   /// <summary>
	   /// this test confirms that we can either create manually or automatically ships
	   /// note that Game is the top container in the hierarchy
	   /// </summary>
	   [TestMethod]
	   public void TestBoardCreation()
	   {
		  
		  _game = new Game(_firstPlayer, _secondPlayer);
		  _game.Initialize();
		  var board1 = _game.GetPlayerBoard(_firstPlayer);
		  var board2 = _game.GetPlayerBoard(_secondPlayer);

		  var bs = board1.CreateBattleship(false, Alingment.Horizontal, 10, 0, 0);
		  Assert.IsNotNull(bs);
		  bs = board1.CreateBattleship(false, Alingment.Horizontal, 11, 0, 0);
		  Assert.IsNull(bs);
		  bs = board1.CreateBattleship(true, Alingment.Horizontal);
		  Assert.AreEqual(10, bs.Squares.Count);
		  Assert.AreEqual(1, bs.Squares[0].Y);
		  Assert.AreEqual(0, bs.Squares[0].X);

		  bs = board2.CreateBattleship(false, Alingment.Vertical, 10, 0, 0);
		  Assert.IsNotNull(bs);
		  bs = board2.CreateBattleship(false, Alingment.Vertical, 11, 0, 0);
		  Assert.IsNull(bs);
		  bs = board2.CreateBattleship(true, Alingment.Vertical);
		  Assert.AreEqual(10, bs.Squares.Count);
		  Assert.AreEqual(1, bs.Squares[0].X);
		  Assert.AreEqual(0, bs.Squares[0].Y);

	   }

	   /// <summary>
	   /// purpose of the test is to confirm that we can successfully serialise and deserialise the game
	   /// for instance if we are to convert the engine as a Web API based we'd need to each time retrieve the game
	   /// perform and action and then save it to a database or other persistant storage
	   /// </summary>
	   [TestMethod]
	   public void TestGameSerialization()
	   {
		  TestBoardCreation();//that would create a board for the first player with two horizontal 10 cell ships
		  string json = JsonConvert.SerializeObject(_game);
		  var game = JsonConvert.DeserializeObject<Game>(json);
		  Assert.AreEqual(_game.Id, game.Id);
		  Assert.AreEqual(2, game.Players.Count);
		  Assert.AreEqual(_game.GetPlayerBoard(_firstPlayer).Battleships.Count, game.GetPlayerBoard(_firstPlayer).Battleships.Count);
		  /*
		   etc etc
		   so the idea is - have a service that
		   -Create and initialize the game returning ID
		   -allow creation of battleship for each player - on each game modification save it to a data store
		   - each method would have a Game ID as a parameter
		   -allow attacking - again after each attack save the Game state to a database
		   by the way if there is a SQL Server used pass the whole json to a stored proc
		   then either denormalize it to individual tables or store it as json in nvarchar(max) field
		   if NoSQL db engine is used save the document to a collection
		  */
	   }
	   /// <summary>
	   /// This test checks that game correctly reports hit or miss for the player attack
	   /// i.e. attack on the player's board. Also it would report if a player still has ships or lost 
	   /// </summary>
	   [TestMethod]
	   public void TestGamePlay()
	   {
		  TestBoardCreation();//that would create a board for the first player with two horizontal 10 cell ships
		  HitResult result;
		  for (int i = 0; i < 9; i++)
		  {
			 result = _game.AttackEnemy(_firstPlayer, i, 0);
			 Assert.AreEqual(HitResult.Wounded, result);
		  }
		  result = _game.AttackEnemy(_firstPlayer, 9, 0);
		  Assert.AreEqual(HitResult.Destroyed, result);
		  Assert.AreEqual(true,_game.PlayerHasShips(_firstPlayer));

		  for (int i = 0; i < 9; i++)
		  {
			 result = _game.AttackEnemy(_firstPlayer, i, 1);
			 Assert.AreEqual(HitResult.Wounded, result);
		  }
		  result = _game.AttackEnemy(_firstPlayer, 9, 1);
		  Assert.AreEqual(HitResult.Destroyed, result);
		  Assert.AreEqual(false,_game.PlayerHasShips(_firstPlayer));
	   }
    }
}
