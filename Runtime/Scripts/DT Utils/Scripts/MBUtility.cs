using UnityEngine;

namespace Utilities
{
	public static class MBUtility
	{
		public static GameObject GetPlayer(this Component mb)
		{
			return GameObject.FindWithTag("Player");
		}
		public static bool IsPlayer(this Component component)
		{
			return component != null && component.CompareTag("Player");
		}
	}

}