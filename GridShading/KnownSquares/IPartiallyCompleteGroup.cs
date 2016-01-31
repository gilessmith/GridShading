namespace GridShading.KnownSquares
{
    using System.Collections.Generic;

    using GridShading.DataStructures;

    public interface IPartiallyCompleteGroup
    {
        bool IsSolved();

        bool GroupMatchesKnownSquares(BitGroup group);

        int GetNextUnsolvedSquare();

        IPartiallyCompleteGroup Copy();

        ICollection<int> GetBlackSquares();

        ICollection<int> GetWhiteSquares();

        void SetSquareBlack(int squareIndex);

        void SetSquareWhite(int squareIndex);

        string DrawGroup();
    }
}