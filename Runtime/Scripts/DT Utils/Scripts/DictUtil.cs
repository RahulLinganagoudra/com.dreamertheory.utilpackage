using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
	public static class DictUtil
	{
		public static void PrintDict(this Dictionary<int, bool> dict)
		{
#if UNITY_EDITOR
			string debug = "";
			foreach (var keyValue in dict)
			{
				debug += $"{keyValue.Key} has {keyValue.Value}\n";
			}
			Debug.Log(debug);
#endif
		}

	}

}