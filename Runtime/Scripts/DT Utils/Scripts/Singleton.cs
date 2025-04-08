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
				if (instance == null)
					instance = FindAnyObjectByType<T>();
				return instance;
			}
			protected set
			{
				instance = value;
			}
		}

	}
}
