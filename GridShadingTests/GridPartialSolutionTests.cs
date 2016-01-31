namespace GridShadingTests
{
    using System.Collections.Generic;

    using GridShading.KnownSquares;

    using NUnit.Framework;

    [TestFixture]
    public class GridPartialSolutionTests
    {
        [Test]
        public void Constructor_MapsRowValuesToColumns()
        {
            var r1 = new PartiallyCompleteGroup(2, "11");
            var r2 = new PartiallyCompleteGroup(2, "11");

            var c1 = new PartiallyCompleteGroup(2, "..");
            var c2 = new PartiallyCompleteGroup(2, "..");

            var grid = new GridPartialSolution(
                new List<IPartiallyCompleteGroup>() { r1, r2 },
                new List<IPartiallyCompleteGroup>() { c1, c2 });

            for (int columnId = 0; columnId < 2; columnId++)
            {
                var column = grid.GetColumn(columnId);
                Assert.That(column.GetBlackSquares().Count, Is.EqualTo(2));
            }
        }

        [Test]
        public void Constructor_MapsColumnValuesToRows()
        {
            var c1 = new PartiallyCompleteGroup(2, "11");
            var c2 = new PartiallyCompleteGroup(2, "11");

            var r1 = new PartiallyCompleteGroup(2, "..");
            var r2 = new PartiallyCompleteGroup(2, "..");

            var grid = new GridPartialSolution(
                new List<IPartiallyCompleteGroup>() { r1, r2 },
                new List<IPartiallyCompleteGroup>() { c1, c2 });

            for (int rowId = 0; rowId < 2; rowId++)
            {
                var row = grid.GetRow(rowId);
                Assert.That(row.GetBlackSquares().Count, Is.EqualTo(2));
            }
        }
    }
}