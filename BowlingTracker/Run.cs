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
            Console.WriteLine("Welcome to Bowling Tracker!");
            bool gameEnded = game.DidGameEnd();
            
            while (!gameEnded)
            {
                Console.WriteLine("Frame " + game.GetCurrentFrameNum() + ", Roll " + game.GetCurrentRoll());
                Console.WriteLine("Please input how many pins was knocked down:");                
                int roll; 
                Int32.TryParse(Console.ReadLine(), out roll);
                game.SetNextRoll(roll);
                Console.WriteLine("Score update: " + game.GetLatestScore());
                gameEnded = game.DidGameEnd();
            }
            Console.WriteLine("Game ended with a final score of: " + game.GetLatestScore());
        }
    }
}