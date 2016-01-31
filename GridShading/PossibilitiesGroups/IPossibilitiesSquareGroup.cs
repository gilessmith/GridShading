namespace GridShading.PossibilitiesGroups
{
    using GridShading.KnownSquares;
    using GridShading.SolvedSquares;

    public interface IPossibilitiesSquareGroup
    {
        ISolvedSquaresResult SolveSquares(IPartiallyCompleteGroup partiallyCompleteForGroup);
    }
}