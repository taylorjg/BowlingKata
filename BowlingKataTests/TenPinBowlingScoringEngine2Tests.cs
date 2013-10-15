using System.Collections.Generic;
using System.Linq;
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
        public void ProcessRollsGivenRegularReturnsTheCorrectFrames()
        {
            var actual = _tenPinBowlingScoringEngine.ProcessRolls(new[] {2, 3});
            AssertFrameDetailsAreCorrect(actual, 1, 2 + 3, 2, 3);
        }

        [Test]
        public void ProcessRollsGivenSpareRegularReturnsTheCorrectFrames()
        {
            var actual = _tenPinBowlingScoringEngine.ProcessRolls(new[] { 2, 8, 5, 1 });
            AssertFrameDetailsAreCorrect(actual, 1, 2 + 8 + 5, 2, 8);
            AssertFrameDetailsAreCorrect(actual, 2, 5 + 1, 5, 1);
        }

        [Test]
        public void ProcessRollsGivenStrikeRegularReturnsTheCorrectFrames()
        {
            var actual = _tenPinBowlingScoringEngine.ProcessRolls(new[] { 10, 5, 1 });
            AssertFrameDetailsAreCorrect(actual, 1, 10 + 5 + 1, 10);
            AssertFrameDetailsAreCorrect(actual, 2, 5 + 1, 5, 1);
        }

        [Test]
        public void ProcessRollsGivenStrikeStrikeRegularReturnsTheCorrectFrames()
        {
            var actual = _tenPinBowlingScoringEngine.ProcessRolls(new[] { 10, 10, 5, 1 });
            AssertFrameDetailsAreCorrect(actual, 1, 10 + 10 + 5, 10);
            AssertFrameDetailsAreCorrect(actual, 2, 10 + 5 + 1, 10);
            AssertFrameDetailsAreCorrect(actual, 3, 5 + 1, 5, 1);
        }

        [Test]
        public void ProcessRollsGivenLastFrameSpareReturnsTheCorrectFrames()
        {
            var actual = _tenPinBowlingScoringEngine.ProcessRolls(Enumerable.Repeat(0, 18).Concat(new[] { 3, 7, 5 }));
            AssertFrameDetailsAreCorrect(actual, 1, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 2, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 3, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 4, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 5, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 6, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 7, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 8, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 9, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 10, 3 + 7 + 5, 3, 7, 5);
        }

        [Test]
        public void ProcessRollsGivenLastFrameStrikeReturnsTheCorrectFrames()
        {
            var actual = _tenPinBowlingScoringEngine.ProcessRolls(Enumerable.Repeat(0, 18).Concat(new[]{10, 1, 2}));
            AssertFrameDetailsAreCorrect(actual, 1, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 2, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 3, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 4, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 5, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 6, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 7, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 8, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 9, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 10, 10 + 1 + 2, 10, 1, 2);
        }

        private static void AssertFrameDetailsAreCorrect(IList<Frame2> frames, int frameNumber, int score, int? firstRoll = null, int? secondRoll = null, int? thirdRoll = null)
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
                            (frame.FirstRoll.Value + frame.SecondRoll.Value == 10)
                                ? Is.EqualTo(RollSymbols.SpareSymbol)
                                : Is.EqualTo(RollSymbols.RollToString(secondRoll.Value)));
            }
            else
            {
                Assert.That(frame.SecondRoll, Is.Null);
            }

            if (thirdRoll.HasValue)
            {
                Assert.That(frame.ThirdRoll.Value, Is.EqualTo(thirdRoll));
                Assert.That(frame.ThirdRoll.Symbol, Is.EqualTo(RollSymbols.RollToString(thirdRoll.Value)));
            }
            else
            {
                Assert.That(frame.ThirdRoll, Is.Null);
            }
        }
    }
}
