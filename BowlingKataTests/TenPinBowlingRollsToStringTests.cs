using BowlingKata;
using NUnit.Framework;

namespace BowlingKataTests
{
    [TestFixture]
    internal class TenPinBowlingRollsToStringTests
    {
        private TenPinBowlingCalculator _tenPinBowlingCalculator;

        [SetUp]
        public void SetUp()
        {
            _tenPinBowlingCalculator = new TenPinBowlingCalculator();
        }

        [TestCase(new[] { 3, 7, 1, 2 }, "3/12")]
        [TestCase(new[] { 10, 10, 1, 2 }, "XX12")]
        [TestCase(new[] { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 }, "5/5/5/5/5/5/5/5/5/5/5")]
        [TestCase(new[] { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }, "XXXXXXXXXXXX")]
        [TestCase(new[] { 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0 }, "9-9-9-9-9-9-9-9-9-9-")]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 7, 4 }, "------------------3/4")]
        [TestCase(new[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 10, 1, 2 }, "------------------X12")]
        public void SequenceOfRollsGivesTheExpectedString(int[] rolls, string expectedString)
        {
            var actual = _tenPinBowlingCalculator.RollsToString(rolls);
            Assert.That(actual, Is.EqualTo(expectedString));
        }
    }
}
