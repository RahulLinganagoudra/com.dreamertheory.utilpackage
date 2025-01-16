using UnityEngine;


namespace DT_Util
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		static T instance;
		public static T Instance
		{
			get
			{
				if (Instance == null)
					Instance = FindAnyObjectByType<T>();
				return Instance;
			}
			protected set
			{
				Instance = value;
			}
		}

	}
}
