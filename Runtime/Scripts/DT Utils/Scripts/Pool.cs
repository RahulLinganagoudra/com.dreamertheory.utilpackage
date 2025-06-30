using System.Collections.Generic;
using UnityEngine;

namespace DT_Util
{
    public class Pool<T> where T : MonoBehaviour
    {
        private List<T> pool = new List<T>();
        private HashSet<T> inUse = new HashSet<T>();
        private int size;
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
                var obj = Object.Instantiate(original, parent);
                obj.gameObject.SetActive(false);
                pool.Add(obj);
            }
        }

        /// <summary>
        /// Gets an available object from the pool. Returns null if all are in use.
        /// </summary>
        public T Get()
        {
            foreach (var obj in pool)
            {
                if (!inUse.Contains(obj))
                {
                    inUse.Add(obj);
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }
            // All objects are in use
            return null;
        }

        /// <summary>
        /// Returns an object to the pool, making it available again.
        /// </summary>
        public void Release(T obj)
        {
            if (inUse.Contains(obj))
            {
                obj.gameObject.SetActive(false);
                inUse.Remove(obj);
            }
        }

        /// <summary>
        /// Deactivates all objects and clears the in-use set.
        /// </summary>
        public void Reset()
        {
            foreach (var obj in pool)
            {
                obj.gameObject.SetActive(false);
            }
            inUse.Clear();
        }
    }
}
