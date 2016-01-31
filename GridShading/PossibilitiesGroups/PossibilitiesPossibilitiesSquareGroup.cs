namespace GridShading.PossibilitiesGroups
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using GridShading.DataStructures;
    using GridShading.KnownSquares;
    using GridShading.SolvedSquares;

    public class PossibilitiesPossibilitiesSquareGroup : IPossibilitiesSquareGroup
    {
        private readonly ICollection<int> blackSequences;

        private readonly int groupLength;

        private List<BitGroup> allCombinations;

        public PossibilitiesPossibilitiesSquareGroup(ICollection<int> blackSequences, int groupLength)
        {
            if (blackSequences == null)
            {
                throw new ArgumentNullException("blackSequences");
            }

            if (groupLength < 1)
            {
                throw new ArgumentException("groupLength must be 1 or greater.", "groupLength");
            }

            this.blackSequences = blackSequences;
            this.groupLength = groupLength;

            if (this.MinSequenceLength(blackSequences) > groupLength)
            {
                throw new ArgumentException(string.Format(
                    "listLength ({0}) must be long enough to fit the sequence ({1}) which is at least {2}.",
                    this.groupLength,
                    string.Join(",", blackSequences),
                    this.MinSequenceLength(blackSequences)));
            }
        }

        public ICollection<BitGroup> AllCombinations()
        {
            if (this.allCombinations == null)
            {
                var results = this.InnerAllCombinations(this.blackSequences, this.groupLength);

                this.allCombinations = results.Select(s => new BitGroup(this.groupLength, s)).ToList();
            }

            return this.allCombinations;
        }
        
        public List<BitGroup> FilterCombinations(IPartiallyCompleteGroup partiallyCompleteForGroup)
        {
            return this.AllCombinations().Where(partiallyCompleteForGroup.GroupMatchesKnownSquares).ToList();
        }
        
        public ISolvedSquaresResult SolveSquares(IPartiallyCompleteGroup partiallyCompleteForGroup)
        {
            var filteredCombinations = this.FilterCombinations(partiallyCompleteForGroup);

            if (filteredCombinations.Count == 0)
            {
                return new CantBeSolvedResult();
            }   

            if (filteredCombinations.Count == 1)
            {
                return new SolvedSquares.SolvedSquaresResult(filteredCombinations.First());
            }

            var partiallyComplete = new PartiallyCompleteGroup(filteredCombinations.First());

            foreach (var possibleCombination in filteredCombinations)
            {
                partiallyComplete = partiallyComplete.Merge(new PartiallyCompleteGroup(possibleCombination));
            }

            if (partiallyComplete.Equals(partiallyCompleteForGroup))
            {
                return new NothingSolvedThisRoundResult(partiallyCompleteForGroup);
            }

            return new PartiallySolvedSquaresResult(partiallyComplete);
        }

        private ICollection<string> InnerAllCombinations(ICollection<int> sequenceSections, int remainingLength)
        {
            var spareSquares = remainingLength - this.MinSequenceLength(sequenceSections);

            if (sequenceSections.Count == 0)
            {
                return new[] { new string('0', spareSquares) };
            }

            var ending = "0";
            if (sequenceSections.Count == 1)
            {
                ending = string.Empty;
            }

            var ret = new List<string>();

            for (int i = 0; i <= spareSquares; i++)
            {
                var stub = new string('0', i) + new string('1', sequenceSections.First()) + ending;

                ret.AddRange(this.InnerAllCombinations(sequenceSections.Skip(1).ToList(), remainingLength - stub.Length).Select(s => stub + s));
            }

            return ret;
        }

        private int MinSequenceLength(ICollection<int> sequences)
        {
            if (sequences.Count == 0)
            {
                return 0;
            }

            return sequences.Sum() + sequences.Count - 1;
        }
    }
}
