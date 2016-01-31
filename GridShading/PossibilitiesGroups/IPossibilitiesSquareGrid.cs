namespace GridShading.PossibilitiesGroups
{
    using GridShading.KnownSquares;
    using GridShading.SolvedSquares;

    public interface IPossibilitiesSquareGrid
    {
        SolveSquaresResult SolveSquares(IGridPartialSolution gridPartialSolution);

        //ICollection<ISquareGroup> Groups { get; set; }
    }
}