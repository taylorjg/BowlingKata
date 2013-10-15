using System.Collections.Generic;
using BowlingKata;
using NUnit.Framework;

namespace BowlingKataTests
{
    [TestFixture]
    internal class TenPinBowlingScoringEngine2Tests
    {
        private TenPinBowlingScoringEngine2 _tenPinBowlingScoringEngine;

        [SetUp]
        public void SetUp()
        {
            _tenPinBowlingScoringEngine = new TenPinBowlingScoringEngine2();
        }

        [Test]
        public void ProcessRollsReturnsTenFrames()
        {
            var actual = _tenPinBowlingScoringEngine.ProcessRolls(new int[]{});
            Assert.That(actual.Length, Is.EqualTo(10));
        }

        [Test]
        public void ProcessRollsGivenAnUninterestingFrameReturnsCorrectFirstFrame()
        {
            var actual = _tenPinBowlingScoringEngine.ProcessRolls(new[] {2, 3});
            AssertFrameDetailsAreCorrect(actual, 1, FrameType.Uninteresting, 2 + 3, 2, 3);
        }

        [Test]
        public void ProcessRollsGivenASpareFrameFollowedByAnUninterestingFrameReturnsTheCorrectFirstAndSecondFrames()
        {
            var actual = _tenPinBowlingScoringEngine.ProcessRolls(new[] { 2, 8, 5, 1 });
            AssertFrameDetailsAreCorrect(actual, 1, FrameType.Spare, 2 + 8 + 5, 2, 8);
            AssertFrameDetailsAreCorrect(actual, 2, FrameType.Uninteresting, 5 + 1, 5, 1);
        }

        private enum FrameType
        {
            Uninteresting,
            Spare,
            Strike
        }

        private static void AssertFrameDetailsAreCorrect(
            IList<Frame2> frames,
            int frameNumber,
            FrameType frameType,
            int score,
            int? firstRoll,
            int? secondRoll)
        {
            var frame = frames[frameNumber - 1];

            Assert.That(frame.FrameNumber, Is.EqualTo(frameNumber));
            Assert.That(frame.Score, Is.EqualTo(score));
            //Assert.That(frame.RunningTotal, Is.EqualTo(firstFrame.Score));

            if (firstRoll.HasValue)
            {
                Assert.That(frame.FirstRoll.Value, Is.EqualTo(firstRoll));
                Assert.That(frame.FirstRoll.Symbol, Is.EqualTo(RollSymbols.RollToString(firstRoll.Value)));
            }
            else
            {
                Assert.That(frame.FirstRoll, Is.Null);
            }

            if (secondRoll.HasValue)
            {
                Assert.That(frame.SecondRoll.Value, Is.EqualTo(secondRoll));
                Assert.That(frame.SecondRoll.Symbol,
                            frameType == FrameType.Spare
                                ? Is.EqualTo(RollSymbols.SpareSymbol)
                                : Is.EqualTo(RollSymbols.RollToString(secondRoll.Value)));
            }
            else
            {
                Assert.That(frame.SecondRoll, Is.Null);
            }
        }
    }
}
