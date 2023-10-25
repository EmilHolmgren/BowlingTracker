using System.Runtime.CompilerServices;

namespace BowlingTracker
{
    internal class App
    {
        static void Main(string[] args) 
        {
            //Create a game
            Game game = new Game();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Welcome to Bowling Tracker! \n - an app to easily calculate your bowling scores");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Frame, PinsKnocked (input 0-10)");
            
            bool gameEnded = game.DidGameEnd();
            
            while (!gameEnded)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(game.GetCurrentFrameNum() + ", ");

                string input = Console.ReadLine();
                bool validInput = IsValidInput(input, game);

                while (!validInput) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Valid input, please: ");

                    Console.ForegroundColor = ConsoleColor.White;
                    input = Console.ReadLine();
                    validInput = IsValidInput(input, game);
                }
                
                Console.ForegroundColor = ConsoleColor.Green;    
                Console.WriteLine("Score: " + game.GetCurrentScore());
                
                Console.ForegroundColor = ConsoleColor.Blue;              
                gameEnded = game.DidGameEnd();
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Game ended with a final score of: " + game.GetCurrentScore());
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static bool IsValidInput(string input, Game game)
        {
            bool result;
            result = int.TryParse(input, out int roll);
            if (result)
            {
                try 
                {
                    game.SetNextRoll(roll);
                }
                catch
                {
                    result = false;
                }
            }
            return result;
        }
    }
}