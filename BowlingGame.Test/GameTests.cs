using BowlingGame.Core;

namespace BowlingGame.Test
{
    [TestClass]
    public class GameTests
    {
        private readonly Game _game = new();

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CannotRollInvalidNumberOfPins_Negative()
        {
            _game.Roll(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CannotRollInvalidNumberOfPins_Positive()
        {
            _game.Roll(11);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CannotRollMoreThan21Times()
        {
            RollMany(0, 22);
        }

        [TestMethod]
        public void CanRollZero()
        {
            _game.Roll(0);
            Assert.AreEqual(0, _game.Score());
        }

        [TestMethod]
        public void CanRollOnce()
        {
            _game.Roll(5);
            Assert.AreEqual(5, _game.Score());
        }

        [TestMethod]
        public void CanRollAllOnes()
        {
            RollMany(1, 20);
            Assert.AreEqual(20, _game.Score());
        }

        [TestMethod]
        public void CanRollSpare()
        {
            _game.Roll(7);
            _game.Roll(3);
            _game.Roll(5);
            Assert.AreEqual(20, _game.Score());
        }

        [TestMethod]
        public void CanRollStrike()
        {
            _game.Roll(10);
            _game.Roll(4);
            _game.Roll(4);
            Assert.AreEqual(26, _game.Score());
        }

        [TestMethod]
        public void CanRollPerfectGame()
        {
            RollMany(10, 12);
            Assert.AreEqual(300, _game.Score());
        }

        [TestMethod]
        public void CanRollThreeTimesInTenthFrame()
        {
            RollMany(0, 18);
            _game.Roll(5);
            _game.Roll(5);
            _game.Roll(5);
            Assert.AreEqual(15, _game.Score());
        }

        [TestMethod]
        public void CanRollAllStrikesInLastFrame()
        {
            RollMany(0, 18);
            _game.Roll(10);
            _game.Roll(10);
            _game.Roll(10);
            Assert.AreEqual(30, _game.Score());
        }

        private void RollMany(int pins, int rolls)
        {
            for (int i = 0; i < rolls; i++)
            {
                _game.Roll(pins);
            }
        }
    }
}