namespace GridShading.Solver
{
    using GridShading.KnownSquares;

    public class SolveResult   
    {
        public SolveResult(bool solved, IGridPartialSolution gridPartialSolution)
        {
            this.GridPartialSolution = gridPartialSolution;
            this.Solved = solved;
        }

        public bool Solved { get; private set; }

        public IGridPartialSolution GridPartialSolution { get; private set; }
    }
}