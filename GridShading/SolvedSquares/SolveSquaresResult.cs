namespace GridShading.SolvedSquares
{
    using System;

    using GridShading.KnownSquares;

    public class SolveSquaresResult
    {
        public SolveSquaresResult(IGridPartialSolution partialSolution, bool noValidSolution) : this(partialSolution, noValidSolution, "abc")
        {
            
        }

        public SolveSquaresResult(IGridPartialSolution partialSolution, bool noValidSolution, string name)
        {
            if (!noValidSolution && partialSolution == null)
            {
                throw new ArgumentException("If noValidSolution is false, then solution must not be null.");
            }

            this.NoValidSolution = noValidSolution;
            this.Name = name;
            this.PartialSolution = partialSolution;
        }

        public bool NoValidSolution { get; private set; }

        public string Name { get; set; }

        public IGridPartialSolution PartialSolution { get; private set; }
    }
}