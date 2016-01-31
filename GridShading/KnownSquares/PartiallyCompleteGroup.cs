namespace GridShading.KnownSquares
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using GridShading.DataStructures;

    public struct PartiallyCompleteGroup : IPartiallyCompleteGroup
    {
        private readonly int maxLength;

        public static PartiallyCompleteGroup Empty
        {
            get
            {
                return default(PartiallyCompleteGroup);
            }
        }

        private BitGroup blackBits;

        private BitGroup whiteBits;

        public PartiallyCompleteGroup(int maxLength)
        {
            if (maxLength >= 31)
            {
                throw new ArgumentException("maxLength of BitSet cannot be larger than 31");
            }

            this.maxLength = maxLength;

            this.blackBits = new BitGroup(maxLength);
            this.whiteBits = new BitGroup(maxLength);
        }

        public PartiallyCompleteGroup(int maxLength, string bitValues)
        {
            if (maxLength >= 31)
            {
                throw new ArgumentException("maxLength of BitSet cannot be larger than 31");
            }

            this.maxLength = maxLength;

            this.blackBits = new BitGroup(maxLength, bitValues);
            this.whiteBits = new BitGroup(maxLength, bitValues.Replace("1", "_").Replace("0", "1"));
        }

        public PartiallyCompleteGroup(BitGroup bitValues)
        {
            this.maxLength = 31;

            this.blackBits = bitValues.Copy();
            this.whiteBits = bitValues.Invert();
        }

        public PartiallyCompleteGroup(BitGroup blacks, BitGroup whites)
        {
            this.maxLength = 31;
            this.blackBits = blacks;
            this.whiteBits = whites;
        }

        public bool IsSolved()
        {
            return this.blackBits == this.whiteBits.Copy().Invert();
        }

        public bool GroupMatchesKnownSquares(BitGroup group)
        {
            return group.MatchBlacks(this.blackBits) && group.MatchWhites(this.whiteBits);
        }

        public int GetNextUnsolvedSquare()
        {
            for (int item = 0; item < this.blackBits.GroupLength(); item++)
            {
                if (!this.blackBits.Contains(item) && !this.whiteBits.Contains(item))
                {
                    return item;
                }
            }

            throw new InvalidOperationException("Could not find an unset bit. GetNextUnsolvedSquare should only be called after checking that the group is not solved.");
        }

        public IPartiallyCompleteGroup Copy()
        {
            return new PartiallyCompleteGroup(this.blackBits.Copy(), this.whiteBits.Copy());
        }

        public ICollection<int> GetBlackSquares()
        {
            return this.blackBits.ToList();
        }

        public ICollection<int> GetWhiteSquares()
        {
            return this.whiteBits.ToList();
        }

        public void SetSquareBlack(int squareIndex)
        {
            this.blackBits.SetValue(squareIndex);
        }

        public void SetSquareWhite(int squareIndex)
        {
            this.whiteBits.SetValue(squareIndex);
        }

        public string DrawGroup()
        {
            var groupDrawing = new StringBuilder(this.blackBits.GroupLength());

            for (int item = 0; item < this.blackBits.GroupLength(); item++)
            {
                if (this.blackBits.Contains(item))
                {
                    groupDrawing.Append("\u2588"); // unicode to display a black square
                    continue;
                }

                if (this.whiteBits.Contains(item))
                {
                    groupDrawing.Append(" ");
                    continue;
                }

                groupDrawing.Append("."); // unknown square
            }

            return groupDrawing.ToString();
        }

        public PartiallyCompleteGroup Merge(PartiallyCompleteGroup other)
        {
            var blacks = this.blackBits & other.blackBits;
            var whites = this.whiteBits & other.whiteBits;

            return new PartiallyCompleteGroup(blacks, whites);
        }

        public override string ToString()
        {
            return "PartiallyCompleteGroup: " + this.DrawGroup();
        }
    }
}