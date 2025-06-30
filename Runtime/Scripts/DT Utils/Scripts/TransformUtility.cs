using System;
using UnityEngine;
using Utilities;

namespace DT_Util
{
    /// <summary>
    /// A collection of extension methods for Transform operations.
    /// </summary>
    public static class TransformUtility
    {
        /// <summary>
        /// Destroys all child objects of a given Transform.
        /// </summary>
        /// <param name="transform">The parent transform whose children will be destroyed.</param>
        public static void DestroyChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// Checks if the squared distance between two Transforms is within a given threshold.
        /// </summary>
        /// <param name="self">The Transform to compare from.</param>
        /// <param name="a">The Transform to compare to.</param>
        /// <param name="b">The maximum distance threshold.</param>
        /// <returns>True if the distance is less than or equal to b.</returns>
        public static bool CompareDist(this Transform self, Transform a, float b)
        {
            return (self.position - a.position).sqrMagnitude <= b.SQ();
        }

        /// <summary>
        /// Rotates a 2D object to face the player, flipping its scale on the X-axis if necessary.
        /// </summary>
        /// <param name="self">The Transform of the object.</param>
        /// <param name="player">The Transform of the player.</param>
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

        /// <summary>
        /// Finds the index of the Transform in the array that is farthest from the reference Transform.
        /// </summary>
        /// <param name="perimeterPositions">Array of Transforms to check.</param>
        /// <param name="reference">The reference Transform.</param>
        /// <param name="excludeIndex">Index to exclude from comparison (-1 to include all).</param>
        /// <returns>Index of the farthest Transform, or -1 if none found.</returns>
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

        /// <summary>
        /// Finds the index of the Transform in the array that is closest to the reference Transform.
        /// </summary>
        /// <param name="perimeterPositions">Array of Transforms to check.</param>
        /// <param name="reference">The reference Transform.</param>
        /// <param name="excludeIndex">Index to exclude from comparison (-1 to include all).</param>
        /// <returns>Index of the closest Transform, or -1 if none found.</returns>
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

        /// <summary>
        /// Determines if another Transform is within a given field of view and distance.
        /// </summary>
        /// <param name="self">The Transform performing the check.</param>
        /// <param name="other">The Transform being checked.</param>
        /// <param name="viewingAngleDeg">The field of view angle in degrees.</param>
        /// <param name="maxDist">The maximum viewing distance.</param>
        /// <returns>True if the other Transform is within the view cone and range.</returns>
        public static bool IsInView(this Transform self, Transform other, float viewingAngleDeg = 120, float maxDist = float.MaxValue)
        {
            Vector3 dir = other.position - self.position;
            return Vector3.Dot(self.forward, dir.normalized) >= Mathf.Cos(viewingAngleDeg / 2f * Mathf.Deg2Rad)
                && dir.sqrMagnitude <= maxDist.SQ();
        }
    }
}
