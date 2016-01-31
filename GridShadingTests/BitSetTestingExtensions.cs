namespace GridShadingTests
{
    using System.Collections.Generic;
    using System.Linq;

    using GridShading;
    using GridShading.DataStructures;

    public static class BitSetTestingExtensions
    {
        public static bool HasExactBitSets(this ICollection<BitGroup> bitSets, params string[] expectedBitSets)
        {
            return bitSets.HasExactBitSets(expectedBitSets.Select(b => new BitGroup(b.Length, b)).ToList());
        }

        public static bool HasExactBitSets(this ICollection<BitGroup> bitSets, ICollection<BitGroup> bitSetsToCompare)
        {
            if (bitSets.Count != bitSetsToCompare.Count)
            {
                return false;
            }

            var comparisonHashset = new HashSet<BitGroup>(bitSetsToCompare);

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