namespace BowlingGame.Core
{
    public class Game
    {
        // Total of 10 frames per game.
        // Each frame has a possibility of 2 rolls + an additional possible third roll in the tenth frame (10*2+1 = 21)
        private readonly int[] _rolls = new int[21];
        private int _currentRoll;

        public void Roll(int pins)
        {
            if (pins < 0 || pins > 10) throw new ArgumentOutOfRangeException(nameof(pins), pins, "Value must be between 0 and 10");
            if (_currentRoll >= _rolls.Length) throw new InvalidOperationException($"Each game only allows a maximum of {_rolls.Length} rolls");

            _rolls[_currentRoll] = pins;
            _currentRoll++;
        }

        public int Score()
        {
            var score = 0;
            var rollIndex = 0;

            // Calculate and summarize score for all 10 frames
            for (int i = 0; i < 10; i++)
            {
                if (IsStrike())
                {
                    score += CaulculateStrikeScore();
                    rollIndex++;
                }
                else if (IsSpare())
                {
                    score += CalculateSpareScore();
                    rollIndex += 2;
                }
                else
                {
                    score += CalculateNormalScore();
                    rollIndex += 2;
                }
            }

            return score;

            bool IsStrike() => _rolls[rollIndex] == 10;
            bool IsSpare() => _rolls[rollIndex] + _rolls[rollIndex + 1] == 10;

            // Strike score = 10 (current frame) + the combined score of the two rolls in the next frame
            int CaulculateStrikeScore() => 10 + _rolls[rollIndex + 1] + _rolls[rollIndex + 2];

            // Spare score = 10 (current frame) + the score of the first roll in next frame
            int CalculateSpareScore() => 10 + _rolls[rollIndex + 2];

            // Normal score = the combined score of the two rolls in current frame
            int CalculateNormalScore() => _rolls[rollIndex] + _rolls[rollIndex + 1];
        }
    }
}