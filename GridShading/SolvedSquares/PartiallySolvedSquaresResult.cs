namespace GridShading.SolvedSquares
{
    using GridShading.KnownSquares;

    public class PartiallySolvedSquaresResult : ISolvedSquaresResult
    {
        private readonly PartiallyCompleteGroup partiallyComplete;

        public PartiallySolvedSquaresResult(PartiallyCompleteGroup partiallyComplete)
        {
            this.partiallyComplete = partiallyComplete;
        }

        public bool StillValid
        {
            get
            {
                return true;
            }
        }

        public bool AnyNewlySolvedSquares
        {
            get
            {
                return true;
            }
        }

        public IPartiallyCompleteGroup Solved
        {
            get
            {
                return this.partiallyComplete;
            }
        }
    }
}