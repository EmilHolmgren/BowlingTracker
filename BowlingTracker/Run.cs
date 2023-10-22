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
            while (!gameEnded)
            {
                /* //This is a bad print function :(
                game.PrintScoreBoard();
                */
                
                //Temp print score board:
                Console.WriteLine("Frame " + game.GetCurrentFrameNum() + ", Roll " + game.GetCurrentRoll());
                Console.ForegroundColor = ConsoleColor.Green;    
                Console.WriteLine("Current score: " + game.GetLatestScore());
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("How many pins was knocked down:");
                Int32.TryParse(Console.ReadLine(), out int roll);
                game.SetNextRoll(roll);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("");
                gameEnded = game.DidGameEnd();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Game ended with a final score of: " + game.GetLatestScore());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}