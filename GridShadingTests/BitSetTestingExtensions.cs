namespace GridShadingTests
{
    using System.Collections.Generic;
    using System.Linq;

    using GridShading;

    public static class BitSetTestingExtensions
    {
        public static bool HasExactBitSets(this ICollection<BitSet> bitSets, params string[] expectedBitSets)
        {
            return bitSets.HasExactBitSets(expectedBitSets.Select(b => new BitSet(b.Length, b)).ToList());
        }

        public static bool HasExactBitSets(this ICollection<BitSet> bitSets, ICollection<BitSet> bitSetsToCompare)
        {
            if (bitSets.Count != bitSetsToCompare.Count)
            {
                return false;
            }

            var comparisonHashset = new HashSet<BitSet>(bitSetsToCompare);

            foreach (var bitSet in bitSets)
            {
                if (!comparisonHashset.Contains(bitSet))
                {
                    return false;
                }
            }

            return true;
        }
    }
}