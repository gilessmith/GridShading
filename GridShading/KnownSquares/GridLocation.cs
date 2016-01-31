namespace GridShading.KnownSquares
{
    public class GridLocation
    {
        public GridLocation(int columnId, int rowId)
        {
            this.RowId = rowId;
            this.ColumnId = columnId;
        }

        public int RowId { get; private set; }

        public int ColumnId { get; private set; }
    }
}