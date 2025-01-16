using System;
using UnityEngine;
using Utilities;

namespace DT_Util
{
	public static class TransformUtility
	{
		public static void DestroyChildren(this Transform transform)
		{
			for (int i = transform.childCount - 1; i >= 0; i--)
			{
				UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
			}
		}
		public static bool CompareDist(this Transform self, Transform a, float b)
		{
			return (self.position - a.position).sqrMagnitude <= b.SQ();
		}
		public static void FaceToPlayer2D(this Transform self, Transform player)
		{
			if (Vector3.Dot((player.position - self.position).normalized, self.right) < 0)
			{
				self.localScale = new Vector3(-1, 1, 1);
			}
			else
			{
				self.localScale = Vector3.one;
			}
		}
		public static int GetFurthestPositionFrom(this Transform[] perimeterPositions, Transform reference, int excludeIndex = -1)
		{
			if (perimeterPositions == null || perimeterPositions.Length == 0) return -1;

			int max = 0;
			for (int i = 0; i < perimeterPositions.Length; i++)
			{
				if (i == excludeIndex) continue;
				if (!reference.position.CompareDist(perimeterPositions[i].position, perimeterPositions[max].position))
				{
					max = i;
				}
			}
			return max;
		}
		public static int GetClosestPositionFrom(this Transform[] perimeterPositions, Transform reference, int excludeIndex = -1)
		{
			if (perimeterPositions == null || perimeterPositions.Length == 0) return -1;
			int min = 0;
			for (int i = 0; i < perimeterPositions.Length; i++)
			{
				if (i == excludeIndex) continue;
				if (reference.position.CompareDist(perimeterPositions[i].position, perimeterPositions[min].position))
				{
					min = i;
				}
			}
			return min;
		}
		public static bool IsInView(this Transform self, Transform other, float viewingAngleDeg = 120, float maxDist = float.MaxValue)
		{
			Vector3 dir = other.position - self.position;
			//Dot product returns +ve for both left and right half dividing by 2 will result in correct view cone
			return Vector3.Dot(self.forward, dir) <= viewingAngleDeg / 2f
				&& dir.sqrMagnitude <= maxDist.SQ();
		}
	}
	[Serializable]
	public abstract class DamageInfoBase
	{
		public GameObject damageCauser;
		public int damageAmmount;
		public bool canBlock;
		public bool canParry;
		public bool canIntrupt;
	}
}