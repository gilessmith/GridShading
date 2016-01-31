namespace GridShadingTests
{
    using System.Linq;

    using GridShading.KnownSquares;

    using NUnit.Framework;

    [TestFixture]
    public class PartiallyCompleteGroupTests
    {
        [Test]
        public void Constructor_PassedListOfUnknownSquares_CreatesGroupWithNoKnownBlackSquares()
        {
            var partiallyComplete = new PartiallyCompleteGroup(2, "..");
            var initialBlackBits = partiallyComplete.GetBlackSquares();

            Assert.That(initialBlackBits.Count, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_PassedListOfKnownAndUnknownSquares_CreatesGroupWithKnownBlackSquares()
        {
            var partiallyComplete = new PartiallyCompleteGroup(2, "1.");
            var initialBlackBits = partiallyComplete.GetBlackSquares();

            Assert.That(initialBlackBits.Count, Is.EqualTo(1));
            Assert.That(initialBlackBits.First(), Is.EqualTo(0));
        }

        [Test]
        public void Constructor_PassedListOfKnownAndUnknownSquares_NeverSetsAnyWhiteSquares()
        {
            var partiallyComplete = new PartiallyCompleteGroup(2, "1.");
            var initialWhiteBits = partiallyComplete.GetWhiteSquares();

            Assert.That(initialWhiteBits.Count, Is.EqualTo(0));
        }

        [Test]
        public void SetBlack_WhenNoSquaresAreBlack_SetsUnknownBitToKnown()
        {
            var partiallyComplete = new PartiallyCompleteGroup(10, "..");
            var squareIndex = 0;

            partiallyComplete.SetSquareBlack(squareIndex);

            var blackBits = partiallyComplete.GetBlackSquares();
            Assert.That(blackBits.Count, Is.EqualTo(1));
            Assert.That(blackBits.First(), Is.EqualTo(squareIndex));
        }

        [Test]
        public void SetBlack_WhenSquareIsAlreadyBlack_AppliesNoChange()
        {
            var partiallyComplete = new PartiallyCompleteGroup(10, "1.");
            var initialBlackSquares = partiallyComplete.GetBlackSquares();
            var squareIndex = 0;

            partiallyComplete.SetSquareBlack(squareIndex);

            var blackBits = partiallyComplete.GetBlackSquares();
            Assert.That(blackBits, Is.EqualTo(initialBlackSquares));
        }
    }
}