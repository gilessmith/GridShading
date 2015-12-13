namespace GridShading
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SquareGroup
    {
        private readonly ICollection<int> blackSequences;

        private readonly int listLength;

        private List<BitSet> allCombinations;

        public SquareGroup(ICollection<int> blackSequences, int listLength)
        {
            if (blackSequences == null)
            {
                throw new ArgumentNullException("blackSequences");
            }

            if (listLength < 1)
            {
                throw new ArgumentException("listLength must be 1 or greater.", "listLength");
            }

            this.blackSequences = blackSequences;
            this.listLength = listLength;

            if (this.MinSequenceLength(blackSequences) > listLength)
            {
                throw new ArgumentException(string.Format(
                    "listLength ({0}) must be long enough to fit the sequence ({1}) which is at least {2}.",
                    this.listLength,
                    string.Join(",", blackSequences),
                    this.MinSequenceLength(blackSequences)));
            }
        }

        public ICollection<BitSet> AllCombinations()
        {
            if (this.allCombinations == null)
            {
                var results = this.InnerAllCombinations(this.blackSequences, this.listLength);

                this.allCombinations = results.Select(s => new BitSet(this.listLength, s)).ToList();
            }

            return this.allCombinations;
        }

        public ICollection<BitSet> FilterCombinations(ICollection<int> blackSquareIndexes, ICollection<int> whiteSquareIndexes)
        {
            IEnumerable<BitSet> a =  this.AllCombinations();
            a = a.Where(c => this.HasBlackIndexes(c, blackSquareIndexes));
            a = a.Where(c => this.HasWhiteIndexes(c, whiteSquareIndexes));
            return a.ToList();
        }

        private bool HasWhiteIndexes(BitSet bitSetToTest, ICollection<int> whiteSquareIndexes)
        {
            return whiteSquareIndexes.All(i => !bitSetToTest.Contains(i));
        }

        private bool HasBlackIndexes(BitSet bitSetToTest, ICollection<int> blackSquareIndexes)
        {
            return blackSquareIndexes.All(bitSetToTest.Contains);
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
