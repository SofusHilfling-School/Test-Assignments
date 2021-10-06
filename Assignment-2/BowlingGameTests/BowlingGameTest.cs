using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingGameTests
{
    [TestClass]
    public class BowlingGameTest
    {
        public Game GameObj { get; set; }

        [TestInitialize]
        public void Init()
        {
            GameObj = new Game();
        }

        [TestMethod]
        public void TestGutterGame()
        {
            RollMany(times: 20, pins: 0);
            Assert.AreEqual(expected: 0, GameObj.Score());
        }

        [TestMethod]
        public void TestAllOnes()
        {
            RollMany(times: 20, pins: 1);
            Assert.AreEqual(expected: 20, GameObj.Score());
        }

        [TestMethod]
        public void TestOneSpare()
        {
            RollSpare();
            GameObj.Roll(pins: 3);
            RollMany(times: 17, pins: 0);
            Assert.AreEqual(expected: 16, GameObj.Score());
        }

        [TestMethod]
        public void TestOneStrike()
        {
            RollStrike();
            GameObj.Roll(pins: 3);
            GameObj.Roll(pins: 4);
            RollMany(times: 16, pins: 0);
            Assert.AreEqual(expected: 24, GameObj.Score());
        }

        [TestMethod]
        public void TestPerfectGame()
        {
            RollMany(times: 12, pins: 10);
            Assert.AreEqual(expected: 300, GameObj.Score());
        }

        private void RollMany(int times, int pins)
        {
            for (int i = 0; i < times; i++)
                GameObj.Roll(pins);
        }

        private void RollStrike()
            => GameObj.Roll(pins: 10);

        private void RollSpare()
        {
            GameObj.Roll(pins: 5);
            GameObj.Roll(pins: 5);
        }
    }

    public class Game
    {
        private int[] rolls = new int[21];
        private int currentRoll = 0;

        public void Roll(int pins)
            => rolls[currentRoll++] = pins;

        public int Score()
        {
            int score = 0;
            int frameIndex = 0;
            for (int frame = 0; frame < 10; frame++)
            {
                if (IsStrike(frameIndex))
                {
                    score += 10 + StrikeBonus(frameIndex);
                    frameIndex++;
                }
                else if (IsSpare(frameIndex))
                {
                    score += 10 + SpareBonus(frameIndex);
                    frameIndex += 2;
                } 
                else
                {
                    score += SumOfBallsInFrame(frameIndex);
                    frameIndex += 2;
                }
            }
            return score;
        }

        private bool IsStrike(int frameIndex)
            => rolls[frameIndex] == 10;

        private bool IsSpare(int frameIndex)
            => rolls[frameIndex] + rolls[frameIndex + 1] == 10;

        private int SumOfBallsInFrame(int frameIndex)
            => rolls[frameIndex] + rolls[frameIndex + 1];

        private int SpareBonus(int frameIndex)
            => rolls[frameIndex + 2];

        private int StrikeBonus(int frameIndex)
            => rolls[frameIndex + 1] + rolls[frameIndex + 2];
    }
}
