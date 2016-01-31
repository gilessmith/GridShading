namespace GridShading.Solver
{
    using GridShading.KnownSquares;
    using GridShading.PossibilitiesGroups;

    public interface IProblemDefinition
    {
        IPossibilitiesSquareGrid PossibilitiesSquareGrid { get; }

        IGridPartialSolution StartingKnownSquares { get; }
    }
}