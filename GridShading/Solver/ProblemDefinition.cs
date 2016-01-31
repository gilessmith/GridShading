namespace GridShading.Solver
{
    using System.Collections.Generic;
    using System.Linq;

    using GridShading.DataStructures;
    using GridShading.KnownSquares;
    using GridShading.PossibilitiesGroups;

    public class ProblemDefinition : IProblemDefinition
    {
        private readonly int[][] rows;

        private readonly int[][] columns;

        private readonly string[] knownGridSquares;

        private GridPartialSolution partialPartialSolution;

        public ProblemDefinition(int[][] rows, int[][] columns, string[] knownGridSquares)
        {
            this.rows = rows;
            this.columns = columns;
            this.knownGridSquares = knownGridSquares;
        }

        public IPossibilitiesSquareGrid PossibilitiesSquareGrid
        {
            get
            {
                var possibleRows = this.rows.Select(r => new PossibilitiesPossibilitiesSquareGroup(r, this.rows.Length)).ToList();
                var possibleColumns = this.columns.Select(c => new PossibilitiesPossibilitiesSquareGroup(c, this.columns.Length)).ToList();

                return new SquarePossibilitiesSquareGrid(possibleRows, possibleColumns);
            }
        }

        public IGridPartialSolution StartingKnownSquares
        {
            get
            {
                if (this.partialPartialSolution != null)
                {
                    return this.partialPartialSolution;
                }

                // At the start there are no known white squares, so the only known squares are black ones. 
                // Therefore use this all unknown bitgroup for the whites.
                var allUnknown = new BitGroup(this.rows.Length);

                var startingRows = new List<IPartiallyCompleteGroup>();
                foreach (var row in this.knownGridSquares)
                {
                    var knownBlacks = new BitGroup(row.Length, row);
                    startingRows.Add(new PartiallyCompleteGroup(knownBlacks, allUnknown));
                }

                var startingColumns = new List<IPartiallyCompleteGroup>();
                for (int columnId = 0; columnId < this.columns.Length; columnId++)
                {
                    var columnIdLocal = columnId;
                    var columnData = string.Join(
                        string.Empty,
                        this.knownGridSquares.Select(r => r.Substring(columnIdLocal, 1)));
                    var knownBlacks = new BitGroup(columnData.Length, columnData);

                    startingColumns.Add(new PartiallyCompleteGroup(knownBlacks, allUnknown));
                }

                this.partialPartialSolution = new GridPartialSolution(startingRows, startingColumns);

                return this.partialPartialSolution;
            }
        }
    }
}