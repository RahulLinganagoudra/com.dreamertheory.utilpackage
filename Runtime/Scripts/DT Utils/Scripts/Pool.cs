using System.Collections.Generic;
using UnityEngine;
namespace DT_Util
{
	public class Pool<T> where T : MonoBehaviour
	{
		public List<T> pool = new List<T>();
		int pointer, size;
		public Transform parent;

		public Pool(T original, int size, Transform parent)
		{
			if (parent.childCount > 0)
			{
				parent.DestroyChildren();
			}
			this.size = size;
			this.parent = parent;
			for (int i = 0; i < size; i++)
			{
				pool.Add(Object.Instantiate(original, parent));
				pool[i].gameObject.SetActive(false);
			}
		}
		public T GetNextObject()
		{

			T item = pool[pointer++ % size];
			item.gameObject.SetActive(true);
			return item;
		}





		public void Reset()
		{
			pointer = 0;
			for (int i = 0; i < size; i++)
			{
				pool[i].gameObject.SetActive(false);
			}
		}
	}
}
