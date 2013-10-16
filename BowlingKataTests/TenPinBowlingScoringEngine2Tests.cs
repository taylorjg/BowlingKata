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
            AssertFrameDetailsAreCorrect(actual, 1, 2 + 3, 2 + 3, 2, 3);
        }

        [Test]
        public void ProcessRollsGivenSpareRegularReturnsTheCorrectFrames()
        {
            var actual = _tenPinBowlingScoringEngine.ProcessRolls(new[] { 2, 8, 5, 1 });
            AssertFrameDetailsAreCorrect(actual, 1, 2 + 8 + 5, 2 + 8 + 5, 2, 8);
            AssertFrameDetailsAreCorrect(actual, 2, 5 + 1, (2 + 8 + 5) + (5 + 1), 5, 1);
        }

        [Test]
        public void ProcessRollsGivenStrikeRegularReturnsTheCorrectFrames()
        {
            var actual = _tenPinBowlingScoringEngine.ProcessRolls(new[] { 10, 5, 1 });
            AssertFrameDetailsAreCorrect(actual, 1, 10 + 5 + 1, 10 + 5 + 1, 10);
            AssertFrameDetailsAreCorrect(actual, 2, 5 + 1, (10 + 5 + 1) + (5 + 1), 5, 1);
        }

        [Test]
        public void ProcessRollsGivenStrikeStrikeRegularReturnsTheCorrectFrames()
        {
            var actual = _tenPinBowlingScoringEngine.ProcessRolls(new[] { 10, 10, 5, 1 });
            AssertFrameDetailsAreCorrect(actual, 1, 10 + 10 + 5, 10 + 10 + 5, 10);
            AssertFrameDetailsAreCorrect(actual, 2, 10 + 5 + 1, (10 + 10 + 5) + (10 + 5 + 1), 10);
            AssertFrameDetailsAreCorrect(actual, 3, 5 + 1, (10 + 10 + 5) + (10 + 5 + 1) + (5 + 1), 5, 1);
        }

        [Test]
        public void ProcessRollsGivenLastFrameSpareReturnsTheCorrectFrames()
        {
            var actual = _tenPinBowlingScoringEngine.ProcessRolls(Enumerable.Repeat(0, 18).Concat(new[] { 3, 7, 5 }));
            AssertFrameDetailsAreCorrect(actual, 1, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 2, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 3, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 4, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 5, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 6, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 7, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 8, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 9, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 10, 3 + 7 + 5, 0 + 3 + 7 + 5, 3, 7, 5);
        }

        [Test]
        public void ProcessRollsGivenLastFrameStrikeReturnsTheCorrectFrames()
        {
            var actual = _tenPinBowlingScoringEngine.ProcessRolls(Enumerable.Repeat(0, 18).Concat(new[]{10, 1, 2}));
            AssertFrameDetailsAreCorrect(actual, 1, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 2, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 3, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 4, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 5, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 6, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 7, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 8, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 9, 0, 0, 0, 0);
            AssertFrameDetailsAreCorrect(actual, 10, 10 + 1 + 2, 0 + 10 + 1 + 2, 10, 1, 2);
        }

        [TestCase(new int[] { }, 0)]
        [TestCase(new[] { 1, 1 }, 2)]
        [TestCase(new[] { 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0 }, 90)]
        [TestCase(new[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, 150)]
        [TestCase(new[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 7 }, 152)]
        [TestCase(new[] { 10, 10, 1, 2 }, (10 + 10 + 1) + (10 + 1 + 2) + (1 + 2))]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 1, 2 }, 13)]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 3, 1 }, 11)]
        [TestCase(new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, 300)]
        [TestCase(new[] { 10, 7, 3, 7, 2, 9, 1, 10, 10, 10, 2, 3, 6, 4, 7, 3, 3 }, 168)]
        public void CalculateTotalReturnsTheCorrectTotal(int[] rolls, int expectedTotal)
        {
            var actual = _tenPinBowlingScoringEngine.CalculateTotalScore(rolls);
            Assert.That(actual, Is.EqualTo(expectedTotal));
        }

        [TestCase(new[] { 3, 7, 1, 2 }, "3/12")]
        [TestCase(new[] { 10, 10, 1, 2 }, "XX12")]
        [TestCase(new[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, "5/5/5/5/5/5/5/5/5/5/5")]
        [TestCase(new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, "XXXXXXXXXXXX")]
        [TestCase(new[] { 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0 }, "9-9-9-9-9-9-9-9-9-9-")]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 7, 4 }, "------------------3/4")]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 1, 2 }, "------------------X12")]
        [TestCase(new[] { 10, 7, 3, 7, 2, 9, 1, 10, 10, 10, 2, 3, 6, 4, 7, 3, 3 }, "X7/729/XXX236/7/3")]
        public void RollsToStringReturnsTheCorrectString(int[] rolls, string expectedString)
        {
            var actual = _tenPinBowlingScoringEngine.RollsToString(rolls);
            Assert.That(actual, Is.EqualTo(expectedString));
        }

        private static void AssertFrameDetailsAreCorrect(
            IList<Frame2> frames,
            int frameNumber,
            int score,
            int runningTotal,
            int? firstRoll = null,
            int? secondRoll = null,
            int? thirdRoll = null)
        {
            var frame = frames[frameNumber - 1];

            Assert.That(frame.FrameNumber, Is.EqualTo(frameNumber));
            Assert.That(frame.Score, Is.EqualTo(score));
            Assert.That(frame.RunningTotal, Is.EqualTo(runningTotal));

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
