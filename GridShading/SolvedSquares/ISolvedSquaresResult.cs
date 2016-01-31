namespace GridShading.SolvedSquares
{
    using GridShading.KnownSquares;

    public interface ISolvedSquaresResult
    {
        bool StillValid { get; }

        bool AnyNewlySolvedSquares { get; }

        IPartiallyCompleteGroup Solved { get; }
    }
}