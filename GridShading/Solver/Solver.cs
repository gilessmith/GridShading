namespace GridShading.Solver
{
    using GridShading.KnownSquares;
    using GridShading.PossibilitiesGroups;

    public class Solver
    {
        public SolveResult Solve(IProblemDefinition gridToSolve)
        {
            var iterativeSolver = new RecursiveSolver(gridToSolve.PossibilitiesSquareGrid);

            return iterativeSolver.Solve(gridToSolve.StartingKnownSquares);
        }

        private class RecursiveSolver
        {
            private readonly IPossibilitiesSquareGrid possibilitiesSquareGrid;

            public RecursiveSolver(IPossibilitiesSquareGrid possibilitiesSquareGrid)
            {
                this.possibilitiesSquareGrid = possibilitiesSquareGrid;
            }

            public SolveResult Solve(IGridPartialSolution gridPartialPartialSolution)
            {
                if (gridPartialPartialSolution.IsSolved)
                {
                    return new SolveResult(true, gridPartialPartialSolution);
                }

                // Solves as many squares as possible without trying to guess values
                var result = this.possibilitiesSquareGrid.SolveSquares(gridPartialPartialSolution);

                if (result.NoValidSolution)
                {
                    return new SolveResult(false, null);
                }

                if (result.PartialSolution.IsSolved)
                {
                    return new SolveResult(true, result.PartialSolution);
                }

                var solution = result.PartialSolution.Copy();
                var squareToGuess = solution.GetNextUnsolvedSquare();

                solution.SetSquareBlack(squareToGuess);
                var blackGuessResult = this.Solve(solution);

                if (blackGuessResult.Solved)
                {
                    return blackGuessResult;
                }

                solution = result.PartialSolution.Copy();
                solution.SetSquareWhite(squareToGuess);
                return this.Solve(solution);
            }
        }
    }
}