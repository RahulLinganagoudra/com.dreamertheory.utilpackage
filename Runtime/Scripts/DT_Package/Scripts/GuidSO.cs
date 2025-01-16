using UnityEngine;
namespace DT_Helpers
{
	public class GuidSO : ScriptableObject
	{
		static int uid = 0;
		[SerializeField] int id = 0;
		public int ID => id;

#if UNITY_EDITOR

		protected virtual void OnValidate()
		{
			if (id == 0)
			{
				id = ++uid;
			}
			else
			{
				if (ID > uid)
				{
					uid = ID;
				}

			}
		}
#endif
	}
}