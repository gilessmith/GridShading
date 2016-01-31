namespace GridShading.KnownSquares
{
    using System.Collections.Generic;

    public interface IGridPartialSolution
    {
        IPartiallyCompleteGroup GetRow(int rowId);

        IPartiallyCompleteGroup GetColumn(int columnId);

        bool IsSolved { get; }

        GridLocation GetNextUnsolvedSquare();

        IGridPartialSolution Copy();

        void SetSquareBlack(GridLocation squareToGuess);

        void SetSquareWhite(GridLocation squareToGuess);

        ICollection<string> DrawSolution();
    }
}