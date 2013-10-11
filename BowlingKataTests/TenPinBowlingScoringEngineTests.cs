using System.Collections.Generic;
using BowlingKata;
using NUnit.Framework;

namespace BowlingKataTests
{
    [TestFixture]
    internal class TenPinBowlingScoringEngineTests
    {
        [Test]
        public void ProcessRollsPassesTheCorrectFrameDetailsToTheHandleFrameActionRoutine()
        {
            var tenPinBowlingScoringEngine = new TenPinBowlingScoringEngine();

            var frames = new List<Frame>();
            tenPinBowlingScoringEngine.ProcessRolls(frames.Add, 10, 3, 4, 2, 8, 5, 0);

            Assert.That(frames.Count, Is.EqualTo(4));

            var frame1 = frames[0];
            Assert.That(frame1.FrameNumber, Is.EqualTo(1));
            Assert.That(frame1.Score, Is.EqualTo(10 + 3 + 4));
            Assert.That(frame1.RunningTotal, Is.EqualTo(frame1.Score));
            Assert.That(frame1.FirstRoll, Is.EqualTo(RollSymbols.StrikeSymbol));
            Assert.That(frame1.SecondRoll, Is.EqualTo(RollSymbols.BlankSymbol));
            Assert.That(frame1.ThirdRoll, Is.EqualTo(RollSymbols.BlankSymbol));

            var frame2 = frames[1];
            Assert.That(frame2.FrameNumber, Is.EqualTo(2));
            Assert.That(frame2.Score, Is.EqualTo(3 + 4));
            Assert.That(frame2.RunningTotal, Is.EqualTo(frame1.Score + frame2.Score));
            Assert.That(frame2.FirstRoll, Is.EqualTo("3"));
            Assert.That(frame2.SecondRoll, Is.EqualTo("4"));
            Assert.That(frame2.ThirdRoll, Is.EqualTo(RollSymbols.BlankSymbol));

            var frame3 = frames[2];
            Assert.That(frame3.FrameNumber, Is.EqualTo(3));
            Assert.That(frame3.Score, Is.EqualTo(2 + 8 + 5));
            Assert.That(frame3.RunningTotal, Is.EqualTo(frame1.Score + frame2.Score + frame3.Score));
            Assert.That(frame3.FirstRoll, Is.EqualTo("2"));
            Assert.That(frame3.SecondRoll, Is.EqualTo(RollSymbols.SpareSymbol));
            Assert.That(frame3.ThirdRoll, Is.EqualTo(RollSymbols.BlankSymbol));

            var frame4 = frames[3];
            Assert.That(frame4.FrameNumber, Is.EqualTo(4));
            Assert.That(frame4.Score, Is.EqualTo(5 + 0));
            Assert.That(frame4.RunningTotal, Is.EqualTo(frame1.Score + frame2.Score + frame3.Score + frame4.Score));
            Assert.That(frame4.FirstRoll, Is.EqualTo("5"));
            Assert.That(frame4.SecondRoll, Is.EqualTo(RollSymbols.GutterSymbol));
            Assert.That(frame4.ThirdRoll, Is.EqualTo(RollSymbols.BlankSymbol));
        }

        [TestCase(new int[] {}, 0)]
        [TestCase(new[] {1, 1}, 2)]
        [TestCase(new[] {9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0}, 90)]
        [TestCase(new[] {5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5}, 150)]
        [TestCase(new[] {5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 7}, 152)]
        [TestCase(new[] {10, 10, 1, 2}, (10 + 10 + 1) + (10 + 1 + 2) + (1 + 2))]
        [TestCase(new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 1, 2}, 13)]
        [TestCase(new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 3, 1}, 11)]
        [TestCase(new[] {10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10}, 300)]
        [TestCase(new[] {10, 7, 3, 7, 2, 9, 1, 10, 10, 10, 2, 3, 6, 4, 7, 3, 3}, 168)]
        public void CalculateTotalReturnsTheCorrectTotal(int[] rolls, int expectedTotal)
        {
            var tenPinBowlingScoringEngine = new TenPinBowlingScoringEngine();
            var actual = tenPinBowlingScoringEngine.CalculateTotal(rolls);
            Assert.That(actual, Is.EqualTo(expectedTotal));
        }

        // TODO: add tests re partial frames...

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
            var tenPinBowlingScoringEngine = new TenPinBowlingScoringEngine();
            var actual = tenPinBowlingScoringEngine.RollsToString(rolls);
            Assert.That(actual, Is.EqualTo(expectedString));
        }
    }
}
