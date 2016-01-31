namespace GridShading.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    public struct BitGroup : IEnumerable<int>, IEquatable<BitGroup>
    {
        private readonly int groupLength;

        public static BitGroup Empty
        {
            get
            {
                return default(BitGroup);
            }
        }

        private int bits;

        public BitGroup(int groupLength)
        {
            if (groupLength > 31)
            {
                throw new ArgumentException("maxLength of BitSet cannot be larger than 31");
            }

            this.groupLength = groupLength;

            this.bits = 0;
        }

        public BitGroup(int groupLength, string bitValues)
        {
            if (groupLength > 31)
            {
                throw new ArgumentException("maxLength of BitSet cannot be larger than 31");
            }

            this.groupLength = groupLength;
            this.bits = 0;

            // set all of the bits - 1 represents a set bit
            var counter = 0;
            foreach (char c in bitValues)
            {
                if (c == '1')
                {
                    this.SetValue(counter);
                }

                counter++;
            }
        }

        public BitGroup(int groupLength, int bitValues)
        {
            if (groupLength >= 31)
            {
                throw new ArgumentException("maxLength of BitSet cannot be larger than 31");
            }

            this.groupLength = groupLength;
            this.bits = bitValues;
        }

        public static bool operator ==(BitGroup left, BitGroup right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BitGroup left, BitGroup right)
        {
            return !left.Equals(right);
        }
        
        public static BitGroup operator &(BitGroup a, BitGroup b)
        {
            var combinedBits = a.bits & b.bits;
            return new BitGroup(a.groupLength, combinedBits);
        }

        public bool Equals(BitGroup other)
        {
            return this.bits == other.bits;
        }

        public override int GetHashCode()
        {
            return this.bits;
        }

        public bool Contains(int item)
        {
            Debug.Assert(0 <= item && item < this.groupLength);
            return (this.bits & (1 << item)) != 0;
        }

        public void SetValue(int item)
        {
            Debug.Assert(0 <= item && item < this.groupLength);
            this.bits = this.bits | (1 << item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int item = 0; item < this.groupLength; ++item)
            {
                if (this.Contains(item))
                {
                    yield return item;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BitGroup))
            {
                return false;
            }

            var other = (BitGroup)obj;

            return this.bits == other.bits;
        }

        public override string ToString()
        {
            var result = new StringBuilder(this.bits);
            foreach (var bit in this)
            {
                if (this.Contains(bit))
                {
                    result.Append("1");
                }
                else
                {
                    result.Append("0");
                }
            }

            return result.ToString();
        }

        public bool MatchBlacks(BitGroup blackBits)
        {
            return (blackBits.bits & this.bits) == blackBits.bits;
        }

        public bool MatchWhites(BitGroup whiteBits)
        {
            return (whiteBits.bits & (~this.bits)) == whiteBits.bits;
        }

        public BitGroup Invert()
        {
            var invertedBits = ~this.bits;
            return new BitGroup(this.groupLength, invertedBits);
        }

        public BitGroup Copy()
        {
            return new BitGroup(this.groupLength, this.bits);
        }

        public int GroupLength()
        {
            // TODO - actually need to track the length of Bitgroups.
            return this.groupLength;
        }
    }
}