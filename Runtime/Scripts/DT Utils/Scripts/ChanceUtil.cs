using UnityEngine;
namespace DT_Util
{
    public static class ChanceUtil
    {
        /// <summary>
        /// Returns true based on the given win percentage (0-100).
        /// For example, if winPercentage is 30, returns true 30% of the time.
        /// </summary>
        /// <param name="winPercentage">Chance to win (0-100).</param>
        /// <returns>True if the random roll is within the win percentage, otherwise false.</returns>
        public static bool GetChance(int winPercentage)
        {
            // Generate a random integer between 0 (inclusive) and 100 (exclusive), i.e., 0 to 99.
            int rnd = Random.Range(0, 100);
            // Return true if rnd is less than winPercentage.
            return rnd < winPercentage;
        }

        /// <summary>
        /// Returns true or false with a 50% chance.
        /// </summary>
        public static bool GetRandomTF()
        {
            return GetChance(50);
        }
    }
}