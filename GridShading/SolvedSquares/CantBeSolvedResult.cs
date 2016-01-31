namespace GridShading.SolvedSquares
{
    using System;

    using GridShading.KnownSquares;

    public class CantBeSolvedResult : ISolvedSquaresResult
    {
        public bool StillValid
        {
            get
            {
                return false;
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
                throw new NotImplementedException();
            }
        }

    }
}