namespace GridShading.SolvedSquares
{
    using GridShading.KnownSquares;

    public class NothingSolvedThisRoundResult : ISolvedSquaresResult
    {
        private readonly IPartiallyCompleteGroup partiallyCompleteForGroup;

        public NothingSolvedThisRoundResult(IPartiallyCompleteGroup partiallyCompleteForGroup)
        {
            this.partiallyCompleteForGroup = partiallyCompleteForGroup;
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
                return false;
            }
        }

        public IPartiallyCompleteGroup Solved
        {
            get
            {
                return this.partiallyCompleteForGroup;
            }
        }
    }
}