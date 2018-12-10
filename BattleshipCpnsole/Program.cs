using BattleshipStateTracker;
using System;

namespace BattleshipCpnsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello to the game of battleship!");
            Console.WriteLine("Please enter first player name");
            var player1 = Console.ReadLine();
            Console.WriteLine("Please enter second player name");
            var player2 = Console.ReadLine();
            Game game = new Game(player1, player2);
            /*
             as a way to extend and demo the solution 
             we could create a loop here where we would ask user(s) to take turns and enter attack parameter
             i.e. lat/long against opponent's board.
             After each hit we'd query board to see if there are ships left and if none are there the victor 
             would be declared
               */
        }
    }
}
