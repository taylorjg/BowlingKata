using System.Linq;
using BowlingKata;
using NUnit.Framework;

namespace BowlingKataTests
{
    [TestFixture]
    internal class TenPinBowlingCalculatorTests
    {
        private TenPinBowlingCalculator _tenPinBowlingCalculator;

        [SetUp]
        public void SetUp()
        {
            _tenPinBowlingCalculator = new TenPinBowlingCalculator();
        }

        [Test]
        public void EmptySequenceGivesZero()
        {
            var actual = _tenPinBowlingCalculator.Calculate();
            Assert.That(actual, Is.EqualTo(0));
        }

        [Test]
        public void SequenceOfOneGivesOne()
        {
            var actual = _tenPinBowlingCalculator.Calculate(1);
            Assert.That(actual, Is.EqualTo(1));
        }

        [Test]
        public void SequenceOfOneOneGivesTwo()
        {
            var actual = _tenPinBowlingCalculator.Calculate(1, 1);
            Assert.That(actual, Is.EqualTo(2));
        }

        [Test]
        public void SequenceOfSpareThenTwoMoreRollsGivesCorrectTotal()
        {
            // Triangulation

            var actual1 = _tenPinBowlingCalculator.Calculate(3, 7, 1, 2);
            Assert.That(actual1, Is.EqualTo((3 + 7 + 1) + (1 + 2)));

            var actual2 = _tenPinBowlingCalculator.Calculate(5, 5, 3, 4);
            Assert.That(actual2, Is.EqualTo((5 + 5 + 3) + (3 + 4)));
        }

        [Test]
        public void SequenceOfStrikeThenTwoMoreRollsGivesCorrectTotal()
        {
            var actual = _tenPinBowlingCalculator.Calculate(10, 3, 4);
            Assert.That(actual, Is.EqualTo((10 + 3 + 4) + (3 + 4)));
        }

        [Test]
        public void SequenceOfTenPairsOfNineAndAMissGives90()
        {
            var actual = _tenPinBowlingCalculator.Calculate(9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0);
            Assert.That(actual, Is.EqualTo(90));
        }

        [Test]
        public void SequenceOfTenPairsOfFiveAndFiveWithFinalFiveGives150()
        {
            var actual = _tenPinBowlingCalculator.Calculate(Enumerable.Repeat(5, 21).ToArray());
            Assert.That(actual, Is.EqualTo(150));
        }

        [Test]
        public void SequenceOfTenPairsOfFiveAndFiveWithFinalSevenGives152()
        {
            var actual = _tenPinBowlingCalculator.Calculate(Enumerable.Repeat(5, 20).Concat(new[] {7}).ToArray());
            Assert.That(actual, Is.EqualTo(152));
        }

        [Test]
        public void SequenceOfStrikeThenSpareThenOneMoreRollGivesCorrectTotal()
        {
            var actual = _tenPinBowlingCalculator.Calculate(10, 8, 2, 5);
            Assert.That(actual, Is.EqualTo((10 + 8 + 2) + (8 + 2 + 5) + (5)));
        }

        [Test]
        public void SequenceOfStrikeThenStrikeThenOneTwoGivesCorrectTotal()
        {
            var actual = _tenPinBowlingCalculator.Calculate(10, 10, 1, 2);
            Assert.That(actual, Is.EqualTo((10 + 10 + 1) + (10 + 1 + 2) + (1 + 2)));
        }

        [Test]
        public void LastFrameSpareIsHandledProperly()
        {
            var actual = _tenPinBowlingCalculator.Calculate(Enumerable.Repeat(0, 18).Concat(new[] { 7, 3, 1 }).ToArray());
            Assert.That(actual, Is.EqualTo((7 + 3 + 1)));
        }

        [Test]
        public void LastFrameStrikeIsHandledProperly()
        {
            var actual = _tenPinBowlingCalculator.Calculate(Enumerable.Repeat(0, 18).Concat(new[] { 10, 1, 2 }).ToArray());
            Assert.That(actual, Is.EqualTo((10 + 1 + 2)));
        }

        [Test]
        public void SequenceOf12StrikesGives300()
        {
            var actual = _tenPinBowlingCalculator.Calculate(Enumerable.Repeat(10, 12).ToArray());
            Assert.That(actual, Is.EqualTo(300));
        }

        [Test]
        // http://bowling.about.com/od/rulesofthegame/a/bowlingscoring.htm
        public void SequenceWithABitOfEverythingGivesCorrectTotal()
        {
            var actual = _tenPinBowlingCalculator.Calculate(new[] {10, 7, 3, 7, 2, 9, 1, 10, 10, 10, 2, 3, 6, 4, 7, 3, 3});
            Assert.That(actual, Is.EqualTo(168));
        }
    }
}
