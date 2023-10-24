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
            bool gameEnded = game.DidGameEnd();
            while (!gameEnded)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Frame " + game.GetCurrentFrameNum() + ": ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("How many pins did you knock down?");

                bool validInput = Int32.TryParse(Console.ReadLine(), out int roll);
                while (!validInput) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input needs to be an integer.");
                    Console.WriteLine("Please try again:");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("How many pins did you knock down?");
                    validInput = Int32.TryParse(Console.ReadLine(), out roll);
                }
                game.SetNextRoll(roll);
                
                Console.ForegroundColor = ConsoleColor.Green;    
                Console.WriteLine("Current score: " + game.GetCurrentScore());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("");
                
                gameEnded = game.DidGameEnd();
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Game ended with a final score of: " + game.GetCurrentScore());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}