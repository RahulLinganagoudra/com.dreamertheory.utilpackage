using UnityEngine;
namespace DT_Util
{
	public static class ChanceUtil
	{
		/// <summary>
		/// to check luck based on the win percentage
		/// </summary>
		/// <param name="winPercentage">percentage = 0%-100%</param>
		/// <returns></returns>
		public static bool GetChance(int winPercentage)
		{
			int rnd = Random.Range(0, 100);
			return rnd < winPercentage - 1;
		}

		public static bool GetRandomTF()
		{
			return GetChance(50);
		}
	}
}