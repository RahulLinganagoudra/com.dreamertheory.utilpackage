using UnityEngine;

namespace DT_Helpers
{
	// can have x offset and have multiple motors assigned to single path
	// i.e transform.TransformPoint(Vector3.right * .5f) + .....;

	[CreateAssetMenu(fileName = "NewPath", menuName = "DT/Create Patrol path")]
	public class Path : ScriptableObject
	{
		[Tooltip("Waypoints are just bunch of position that a mover will cover")]
		public Vector3[] wayPoints;

		public int GetWayPointIndex(ref int currentWayPointIndex, bool curcular = true)
		{

			int modded = currentWayPointIndex % wayPoints.Length;
			if (curcular)
			{
				return modded;
			}
			else
			{
				if (currentWayPointIndex / wayPoints.Length % 2 == 0)
				{
					if (currentWayPointIndex % wayPoints.Length == 0) currentWayPointIndex = modded;
					return modded;
				}
				return wayPoints.Length - 1 - modded;
			}
		}
	}
}