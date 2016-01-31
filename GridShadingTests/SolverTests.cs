namespace GridShadingTests
{
    using GridShading;
    using GridShading.KnownSquares;
    using GridShading.PossibilitiesGroups;
    using GridShading.SolvedSquares;
    using GridShading.Solver;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class SolverTests
    {
        private Mock<IGridPartialSolution> gridSolution;

        private Mock<IPossibilitiesSquareGrid> gridPossibilities;

        private Mock<IProblemDefinition> problemDefinition;

        [SetUp]
        public void Setup()
        {
            this.gridSolution = new Mock<IGridPartialSolution>();
            this.gridPossibilities = new Mock<IPossibilitiesSquareGrid>();
            this.problemDefinition = new Mock<IProblemDefinition>();
            this.problemDefinition.Setup(p => p.PossibilitiesSquareGrid).Returns(this.gridPossibilities.Object);
            this.problemDefinition.Setup(p => p.StartingKnownSquares).Returns(this.gridSolution.Object);
        }
        
        [Test]
        public void Solve_CalledWithSolvedGridSolution_ReturnsSolvedGrid()
        {
            var solver = this.CreateSolver();

            this.gridSolution.Setup(gs => gs.IsSolved).Returns(true);

            var result = solver.Solve(this.problemDefinition.Object);

            Assert.That(result.Solved, Is.True);
            Assert.That(result.GridPartialSolution, Is.SameAs(this.gridSolution.Object));
        }

        [Test]
        public void Solve_WhenNoValidSolutionsForGrid_ReturnsEmptyGridSolution()
        {
            var solver = this.CreateSolver();

            this.gridPossibilities
                .Setup(gp => gp.SolveSquares(It.IsAny<IGridPartialSolution>()))
                .Returns(new SolveSquaresResult(null, true));

            var result = solver.Solve(this.problemDefinition.Object);

            Assert.That(result.Solved, Is.False);
        }

        [Test]
        public void Solve_WhenAllSquaresCanBeSolvedWithoutGuesses_ReturnsSuccessResultWithSolvedGrid()
        {
            var solver = this.CreateSolver();
            var solvedGridSolution = new Mock<IGridPartialSolution>();
            solvedGridSolution.Setup(gs => gs.IsSolved).Returns(true);

            this.gridPossibilities.Setup(gp => gp.SolveSquares(It.IsAny<IGridPartialSolution>())).Returns(new SolveSquaresResult(solvedGridSolution.Object, false));

            var result = solver.Solve(this.problemDefinition.Object);

            Assert.That(result.Solved, Is.True);
            Assert.That(result.GridPartialSolution, Is.SameAs(solvedGridSolution.Object));
        }

        [Test]
        public void Solve_WhenSquaresCantAllBeSolvedAndCorrectGuessIsMade_SolveIsCalledIterativelyReturningSuccess()
        {
            var solver = this.CreateSolver();

            var innerSolution = new Mock<IGridPartialSolution>();
            innerSolution.Setup(gs => gs.GetNextUnsolvedSquare()).Returns(new GridLocation(0, 0));
            innerSolution.Setup(gs => gs.Copy()).Returns(innerSolution.Object);

            var solvedSolution = new Mock<IGridPartialSolution>();
            solvedSolution.Setup(s => s.IsSolved).Returns(true);

            this.gridPossibilities.Setup(gp => gp.SolveSquares(It.IsAny<IGridPartialSolution>())).Returns(new SolveSquaresResult(innerSolution.Object, false, "a"));
            this.gridPossibilities.Setup(gp => gp.SolveSquares(innerSolution.Object)).Returns(new SolveSquaresResult(solvedSolution.Object, false, "b"));

            var result = solver.Solve(this.problemDefinition.Object);

            this.gridPossibilities.Verify(gp => gp.SolveSquares(innerSolution.Object), Times.Once);
            Assert.That(result.Solved, Is.True);
        }

        [Test]
        public void Solve_WhenSquaresCantAllBeSolvedAndIncorrectGuessIsMade_SolveIsCalledIterativelyReturningFailThenAlternateGuessIsMade()
        {
            var solver = this.CreateSolver();

            var innerSolution = new Mock<IGridPartialSolution>();
            innerSolution.Setup(gs => gs.GetNextUnsolvedSquare()).Returns(new GridLocation(0, 0));
            innerSolution.Setup(gs => gs.Copy()).Returns(innerSolution.Object);

            var solvedSolution = new Mock<IGridPartialSolution>();
            solvedSolution.Setup(s => s.IsSolved).Returns(true);

            this.gridPossibilities.Setup(gp => gp.SolveSquares(It.IsAny<IGridPartialSolution>())).Returns(new SolveSquaresResult(innerSolution.Object, false, "a"));
            this.gridPossibilities.Setup(gp => gp.SolveSquares(innerSolution.Object)).Returns(new SolveSquaresResult(solvedSolution.Object, false, "b"));

            var result = solver.Solve(this.problemDefinition.Object);

            this.gridPossibilities.Verify(gp => gp.SolveSquares(innerSolution.Object), Times.Once);
            Assert.That(result.Solved, Is.True);
        }

        private Solver CreateSolver()
        {
            return new Solver();
        }
    }
}
