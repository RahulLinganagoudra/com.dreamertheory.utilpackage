using UnityEngine;

namespace DT_Helpers
{
	public class PathSeeker : MonoBehaviour
	{
		[SerializeField] Path path;
		int current = 0;
		public bool circular;
		public float speed = 1;
		private void Update()
		{
			Vector3 target = GetTarget();

			if ((target - transform.position).sqrMagnitude < 0.01f)
			{
				current++;
				target = GetTarget();
			}

			Vector3 dx = (target - transform.position).normalized * Time.deltaTime * speed;
			dx.y = 0;
			transform.position += dx;
		}
		Vector3 GetTarget()
		{
			return path.wayPoints[path.GetWayPointIndex(ref current, circular)];
		}
	}
}