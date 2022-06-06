using BowlingGame.Core;

namespace BowlingGame.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();

            for (int currentFrame = 1; currentFrame <= 10; currentFrame++)
            {
                var frameScore = 0;
                var frameRollCount = 0;

                while (true)
                {
                    Console.Write($"[Frame {currentFrame}/10 | Roll {frameRollCount + 1}] Enter number of Pins knocked down: ");
                    var input = Console.ReadLine();

                    if (int.TryParse(input, out int pins) && pins >= 0 && pins <= 10)
                    {
                        if (pins < 0 || pins > 10)
                        {
                            WriteErrorMessage("Number of Pins must be between 0 and 10");
                            continue;
                        }
                        else if (frameScore + pins > 10 && currentFrame < 10)
                        {
                            WriteErrorMessage($"Max number of remaining pins to be knocked down in current frame: {10 - frameScore}");
                            continue;
                        }

                        game.Roll(pins);
                        
                        frameRollCount++;
                        frameScore += pins;

                        if (ShouldBreakCurrentFrameLoop(currentFrame, frameRollCount, frameScore))
                        {
                            break;
                        }
                    }
                    else
                    {
                        WriteErrorMessage("Enter a valid number between 0 and 10");
                    }
                }
            }

            var score = game.Score();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Total game score: {score}");
            Console.ResetColor();
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private static bool ShouldBreakCurrentFrameLoop(int currentFrame, int frameRollCount, int frameScore)
        {
            if (currentFrame == 10)
            {
                if (frameRollCount == 3 || (frameRollCount == 2 && frameScore < 10))
                {
                    return true;
                }
            }
            else if (frameRollCount == 2 || frameScore == 10)
            {
                return true;
            }

            return false;
        }

        private static void WriteErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}