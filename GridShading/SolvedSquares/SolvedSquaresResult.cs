namespace GridShading.SolvedSquares
{
    using GridShading.DataStructures;
    using GridShading.KnownSquares;

    public class SolvedSquaresResult : ISolvedSquaresResult
    {
        private readonly BitGroup knownGroup;

        public SolvedSquaresResult(BitGroup knownGroup)
        {
            this.knownGroup = knownGroup;
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
                return new PartiallyCompleteGroup(this.knownGroup, this.knownGroup.Invert());
            }
        }
    }
}