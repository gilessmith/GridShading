namespace GridShading
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

    public struct BitSet : IEnumerable<int>, IEquatable<BitSet>
    {
        private readonly int maxLength;

        public static BitSet Empty
        {
            get
            {
                return default(BitSet);
            }
        }

        private int bits;

        public BitSet(int maxLength)
        {
            if (maxLength >= 31)
            {
                throw new ArgumentException("maxLength of BitSet cannot be larger than 31");
            }
            
            this.maxLength = maxLength;
            
            this.bits = 0;
        }

        public BitSet(int maxLength, string bitValues)
        {
            if (maxLength >= 31)
            {
                throw new ArgumentException("maxLength of BitSet cannot be larger than 31");
            }
            
            this.maxLength = maxLength;
            this.bits = 0;

            // set all of the bits - 1 represents a set bit
            var counter = 0;
            foreach (char c in bitValues)
            {
                if (c == '1')
                {
                    this.Add(counter);
                }

                counter++;
            }
        }

        public static bool operator ==(BitSet left, BitSet right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BitSet left, BitSet right)
        {
            return !left.Equals(right);
        }

        public bool Equals(BitSet other)
        {
            return this.bits == other.bits;
        }

        public override int GetHashCode()
        {
            return this.bits;
        }

        public bool Contains(int item)
        {
            Debug.Assert(0 <= item && item <= this.maxLength);
            return (this.bits & (1 << item)) != 0;
        }

        public void Add(int item)
        {
            Debug.Assert(0 <= item && item <= this.maxLength);
            this.bits = this.bits | (1 << item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int item = 0; item <= this.maxLength; ++item)
            {
                if (this.Contains(item))
                {
                    yield return item;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BitSet))
            {
                return false;
            }

            var other = (BitSet)obj;

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
    }
}