using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingTracker
{
    internal class Run
    {
        static void Main(string[] args) {
            //Create a game
            Game game = new Game();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to Bowling Tracker!");
            bool gameEnded = game.DidGameEnd();
            Console.ForegroundColor = ConsoleColor.White;
            while (!gameEnded)
            {
                game.PrintScoreBoard();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("How many pins was knocked down:");
                Int32.TryParse(Console.ReadLine(), out int roll);
                game.SetNextRoll(roll);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Score update: " + game.GetLatestScore());
                gameEnded = game.DidGameEnd();
            }
            Console.WriteLine("Game ended with a final score of: " + game.GetLatestScore());
        }
    }
}