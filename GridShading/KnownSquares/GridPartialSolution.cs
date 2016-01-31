namespace GridShading.KnownSquares
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GridPartialSolution : IGridPartialSolution
    {
        private List<IPartiallyCompleteGroup> solvedRows;

        private List<IPartiallyCompleteGroup> solvedColumns;

        public GridPartialSolution(ICollection<IPartiallyCompleteGroup> solvedRows, ICollection<IPartiallyCompleteGroup> solvedColumns)
        {
            if (solvedRows == null)
            {
                throw new ArgumentNullException("solvedRows");
            }

            if (solvedColumns == null)
            {
                throw new ArgumentNullException("solvedColumns");
            }

            this.solvedRows = solvedRows.ToList();
            this.solvedColumns = solvedColumns.ToList();

            this.CrossFillRowsAndColumns();
        }

        public IPartiallyCompleteGroup GetRow(int rowId)
        {
            if (rowId < 0)
            {
                throw new ArgumentException("GetRow - rowId must be greater than -1");
            }

            if (rowId >= this.solvedRows.Count)
            {
                throw new ArgumentException(string.Format("GetRow - rowId ({0}) cannot be greater than the number of rows in the solution ({1}).", rowId, this.solvedRows.Count));
            }

            return this.solvedRows[rowId];
        }

        public IPartiallyCompleteGroup GetColumn(int columnId)
        {
            if (columnId < 0)
            {
                throw new ArgumentException("GetColumn - columnId must be greater than -1");
            }

            if (columnId >= this.solvedRows.Count)
            {
                throw new ArgumentException(string.Format("GetColumn - columnId ({0}) cannot be greater than the number of columns in the solution ({1}).", columnId, this.solvedRows.Count));
            }

            return this.solvedColumns[columnId];
        }

        public bool IsSolved
        {
            get
            {
                return this.solvedRows.All(r => r.IsSolved()) && this.solvedColumns.All(c => c.IsSolved());
            }
        }

        public GridLocation GetNextUnsolvedSquare()
        {
            var rowId = 0;
            foreach (var row in this.solvedRows)
            {
                if (row.IsSolved())
                {
                    rowId++;
                    continue;
                }

                int columnId = row.GetNextUnsolvedSquare();
                return new GridLocation(columnId, rowId);
            }

            throw new InvalidOperationException("Could not find next unsolved square. Before calling this method check that the grid is unsolved.");
        }

        public IGridPartialSolution Copy()
        {
            var rows = this.solvedRows.Select(r => r.Copy()).ToList();
            var columns = this.solvedColumns.Select(c => c.Copy()).ToList();

            return new GridPartialSolution(rows, columns);
        }

        public void SetSquareBlack(GridLocation squareToGuess)
        {
            this.solvedRows[squareToGuess.RowId].SetSquareBlack(squareToGuess.ColumnId);
            this.solvedColumns[squareToGuess.ColumnId].SetSquareBlack(squareToGuess.RowId);
        }

        public void SetSquareWhite(GridLocation squareToGuess)
        {
            this.solvedRows[squareToGuess.RowId].SetSquareWhite(squareToGuess.ColumnId);
            this.solvedColumns[squareToGuess.ColumnId].SetSquareWhite(squareToGuess.RowId);
        }

        public ICollection<string> DrawSolution()
        {
            var drawing = new List<string>();
            foreach (var row in this.solvedRows)
            {
                drawing.Add(row.DrawGroup());
            }

            return drawing;
        }

        private void CrossFillRowsAndColumns()
        {
            var rowId = 0;
            foreach (var row in this.solvedRows)
            {
                foreach (var column in row.GetBlackSquares())
                {
                    this.solvedColumns[column].SetSquareBlack(rowId);
                }

                foreach (var column in row.GetWhiteSquares())
                {
                    this.solvedColumns[column].SetSquareWhite(rowId);
                }

                rowId++;
            }

            var columnId = 0;

            foreach (var column in this.solvedColumns)
            {
                foreach (var row in column.GetBlackSquares())
                {
                    this.solvedRows[row].SetSquareBlack(columnId);
                }

                foreach (var row in column.GetWhiteSquares())
                {
                    this.solvedRows[row].SetSquareWhite(columnId);
                }

                columnId++;
            }
        }
    }
}