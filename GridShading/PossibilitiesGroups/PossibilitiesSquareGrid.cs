namespace GridShading.PossibilitiesGroups
{
    using System;
    using System.Collections.Generic;

    using GridShading.KnownSquares;
    using GridShading.SolvedSquares;

    /// <summary>
    /// An instance of PossibilitiesSquareGrid represents a all the possible rows and columns that could be 
    /// possible based on the row and column group data for the problem (the number and size of each group of 
    /// black squares in the row/column). 
    /// 
    /// The solve squares method takes a grid containing all the known squares, and filters the possibilities
    /// and if the filtering surfaces any new known squares then they are added to the partially solved grid.
    /// </summary>
    public class SquarePossibilitiesSquareGrid : IPossibilitiesSquareGrid
    {
        private readonly ICollection<PossibilitiesPossibilitiesSquareGroup> rows;

        private readonly ICollection<PossibilitiesPossibilitiesSquareGroup> columns;

        public SquarePossibilitiesSquareGrid(ICollection<PossibilitiesPossibilitiesSquareGroup> rows, ICollection<PossibilitiesPossibilitiesSquareGroup> columns)
        {
            if (rows == null)
            {
                throw new ArgumentNullException("rows");
            }

            if (columns == null)
            {
                throw new ArgumentNullException("columns");
            }

            // Assume that the grid is square
            if (rows.Count != columns.Count)
            {
                throw new ArgumentException("Rows and columns should be of equal length.");
            }

            this.rows = rows;
            this.columns = columns;
        }

        public SolveSquaresResult SolveSquares(IGridPartialSolution gridPartialSolution)
        {
            bool anySquaresSolved;
            IGridPartialSolution partialSolution;
            do
            {
                int rowId = 0;
                anySquaresSolved = false;
                var solvedRows = new List<IPartiallyCompleteGroup>();

                foreach (var row in this.rows)
                {
                    var result = row.SolveSquares(gridPartialSolution.GetRow(rowId));
                    anySquaresSolved = anySquaresSolved || result.AnyNewlySolvedSquares;

                    if (!result.StillValid)
                    {
                        return new SolveSquaresResult(gridPartialSolution, false);
                    }

                    solvedRows.Add(result.Solved);
                    
                    rowId++;
                }

                int columnId = 0;
                var solvedColumns = new List<IPartiallyCompleteGroup>();

                foreach (var column in this.columns)
                {
                    var result = column.SolveSquares(gridPartialSolution.GetColumn(columnId));
                    anySquaresSolved = anySquaresSolved || result.AnyNewlySolvedSquares;

                    if (!result.StillValid)
                    {
                        return new SolveSquaresResult(gridPartialSolution, true);
                    }

                    solvedColumns.Add(result.Solved);

                    columnId++;
                }
                
                partialSolution = new GridPartialSolution(solvedRows, solvedColumns);

                if (partialSolution.IsSolved)
                {
                    return new SolveSquaresResult(partialSolution, false);
                }

                gridPartialSolution = partialSolution.Copy();
            }
            while (anySquaresSolved);
            


            return new SolveSquaresResult(partialSolution, false);
        }
    }
}