namespace GridShadingTests
{
    using GridShading.DataStructures;

    using NUnit.Framework;

    [TestFixture]
    public class BitGroupTests
    {
        [Test]
        [TestCase("00", "00", true)]
        [TestCase("10", "10", true)]
        [TestCase("11", "10", true)]
        [TestCase("10", "11", false)]
        [TestCase("00", "01", false)]
        [TestCase("10", "01", false)]
        public void MatchBlacks(string bitsToCheck, string knownBlackSquares, bool shouldMatch)
        {
            var result = new BitGroup(4, bitsToCheck).MatchBlacks(new BitGroup(4, knownBlackSquares));

            Assert.That(result, Is.EqualTo(shouldMatch));
        }

        [Test]
        [TestCase("0", "0", true)]
        [TestCase("1", "0", true)]
        [TestCase("0", "1", true)]
        [TestCase("1", "1", false)]
        [TestCase("01", "10", true)]
        [TestCase("10", "10", false)]
        [TestCase("11", "10", false)]
        public void MatchWhites(string bitsToCheck, string knownWhiteSquares, bool shouldMatch)
        {
            var result = new BitGroup(4, bitsToCheck).MatchWhites(new BitGroup(4, knownWhiteSquares));

            Assert.That(result, Is.EqualTo(shouldMatch));
        }

        [Test]
        public void BitGroup_Invert()
        {
            var start = new BitGroup(2, "10");
            var inverted = start.Invert();
            var doubleInverted = inverted.Invert();

            Assert.That(doubleInverted, Is.EqualTo(start));
        }

        [Test]
        public void BitGroup_Invert_InvertsTheString()
        {
            var start = new BitGroup(2, "101");
            var inverted = start.Invert();

            Assert.That(inverted, Is.EqualTo(new BitGroup(2, "010")));
        }

        [Test]
        public void BitGroup_Set()
        {
            var start = new BitGroup(2, "10");
            start.SetValue(1);

            Assert.That(start, Is.EqualTo(new BitGroup(2, "11")));
        }
    }
}