namespace GridShadingTests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Timers;

    using GridShading;

    using NUnit.Framework;

    [TestFixture]
    public class SquareGroupTest
    {
        [Test]
        public void Ctor_WithNullInputData_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SquareGroup(null, 1));
        }

        [Test]
        public void Ctor_WithZero_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new SquareGroup(new int[] { }, 0));
        }

        [Test]
        public void Ctor_WhenSequenceWillNotFitWithinLength_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new SquareGroup(new[] { 1, 1 }, 2));
        }

        [Test]
        public void AllCombinations_ForSingleBlackSequenceInRowOfLength1_ReturnsSingleBlackSquare()
        {
            var s = new SquareGroup(new[] { 1 }, 1);

            var results = s.AllCombinations();

            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results.First(), Is.EqualTo(new BitSet(1, "1")));
        }

        [Test]
        public void AllCombinations_ForSingleBlackSequenceInRowOfLength2_ReturnsTwoCombinationsWithTheBlackSequenceInOppositeLocations()
        {
            var s = new SquareGroup(new[] { 1 }, 2);

            var results = s.AllCombinations();

            Assert.That(results.Count, Is.EqualTo(2));
            Assert.That(results.HasExactBitSets("10", "01"), Is.True);
        }

        [Test]
        [TestCase(1, new []{ 1, 1 }, 4, new[] { "1010", "1001", "0101" })]
        [TestCase(2, new []{1, 1 }, 5, new[] { "10100", "10010", "10001", "01010", "01001", "00101" })]
        [TestCase(3, new []{1, 1, 1 }, 7, new[] { "1010100", "1010010", "1010001", "1001010", "1001001", "1000101", "0101010", "0101001", "0100101", "0010101" })]
        [TestCase(4, new []{1, 2 }, 5, new[] { "10110", "10011", "01011" })]
        public void AllCombinations_TestCombinations(int testCaseId, int[] blackSequences, int rowLength, string[] expectedResults)
        {
            var s = new SquareGroup(blackSequences, rowLength);

            var results = s.AllCombinations();

            Assert.That(results.HasExactBitSets(expectedResults), Is.True);
        }

        [Test]
        public void FilterCombinations_WithEmptyFilterLists_ReturnsAllCombinations()
        {
            var s = new SquareGroup(new[] { 1 }, 2);

            var allCombinations = s.AllCombinations();
            var filteredCombinations = s.FilterCombinations(new int[] { }, new int[] { });

            Assert.That(allCombinations.HasExactBitSets(filteredCombinations));
        }

        [Test]
        [TestCase(1, new[] { 1 }, 2, new[] { 0 }, new int[] { }, new[] { "10" })]
        [TestCase(2, new[] { 1 }, 2, new[] { 1 }, new int[] { }, new[] { "01" })]
        [TestCase(3, new[] { 1 }, 2, new int[] { }, new[] { 1 }, new[] { "10" })]
        [TestCase(4, new[] { 1 }, 2, new int[] { }, new[] { 0 }, new[] { "01" })]
        [TestCase(5, new[] { 1, 1 }, 5, new int[] { }, new[] { 0 }, new[] { "01010", "01001", "00101" })] // zeroth cell is 0
        [TestCase(6, new[] { 1, 1 }, 5, new int[] { 2 }, new[] { 0 }, new[] { "00101" })] // zeroth cell is 0 and 2nd cell is 1 
        public void FilterCombinations(int testCaseId, int[] inputSequences, int listLength, int[] blackSquareFilterIndexes, int[] whiteSquareFilterIndexes, string[] expectedBitSets)
        {
            var s = new SquareGroup(inputSequences, listLength);

            var filteredCombinations = s.FilterCombinations(blackSquareFilterIndexes, whiteSquareFilterIndexes);

            Assert.That(filteredCombinations.HasExactBitSets(expectedBitSets), Is.True);
        }

        [Ignore("This test is a simple performance test. The generation of combinations should only happen once for each row or column, so not too performance sensitive.")]
        [Test]
        public void AllCombinations_PerformanceTestForLongRow()
        {
            var s = new SquareGroup(new[] { 1, 1, 1, 1, 1, 1, 1, 1 }, 31);

            Stopwatch t = new Stopwatch();
            t.Start();
            var results = s.AllCombinations();
            t.Stop();

            Assert.That(t.ElapsedMilliseconds, Is.LessThan(2000));
        }
    }


}
