using UnityEngine;

namespace DT_Util
{
    /// <summary>
    /// A collection of extension methods for vector operations.
    /// </summary>
    public static class VectorExt
    {
        public const float Rad = Mathf.PI * 2f;
        /// <summary>
        /// Computes a cubic Bézier curve with a given height.
        /// </summary>
        /// <param name="start">Starting point.</param>
        /// <param name="end">End point.</param>
        /// <param name="height">Height of the midpoint.</param>
        /// <param name="t">Interpolation factor (0 to 1).</param>
        /// <returns>Interpolated point on the Bézier curve.</returns>
        public static Vector3 CubicBezier(Vector3 start, Vector3 end, float height, float t)
        {
            float _1MinusT = 1 - t;
            Vector3 mid = (start + end) / 2;
            mid.y += height;

            return _1MinusT * _1MinusT * start
                + 2 * _1MinusT * t * mid
                + t * t * end;
        }

        /// <summary>
        /// Computes a cubic Bézier curve given three control points.
        /// </summary>
        /// <param name="start">Starting point.</param>
        /// <param name="mid">Control point.</param>
        /// <param name="end">End point.</param>
        /// <param name="t">Interpolation factor (0 to 1).</param>
        /// <returns>Interpolated point on the Bézier curve.</returns>
        public static Vector3 CubicBezier(Vector3 start, Vector3 mid, Vector3 end, float t)
        {
            float _1MinusT = 1 - t;

            return _1MinusT * _1MinusT * start
                + 2 * _1MinusT * t * mid
                + t * t * end;
        }

        /// <summary>
        /// Clamps a Vector2 within a given range.
        /// </summary>
        /// <param name="original">The vector to clamp.</param>
        /// <param name="min">Minimum values.</param>
        /// <param name="max">Maximum values.</param>
        /// <returns>Clamped Vector2.</returns>
        public static Vector2 Clamp(this Vector2 original, Vector2 min, Vector2 max)
        {
            return new Vector2(
                Mathf.Clamp(original.x, min.x, max.x),
                Mathf.Clamp(original.y, min.y, max.y)
            );
        }

        /// <summary>
        /// Clamps a Vector3 within a given range.
        /// </summary>
        /// <param name="original">The vector to clamp.</param>
        /// <param name="min">Minimum values.</param>
        /// <param name="max">Maximum values.</param>
        /// <returns>Clamped Vector3.</returns>
        public static Vector3 Clamp(this Vector3 original, Vector3 min, Vector3 max)
        {
            return new Vector3(
                Mathf.Clamp(original.x, min.x, max.x),
                Mathf.Clamp(original.y, min.y, max.y),
                Mathf.Clamp(original.z, min.z, max.z)
            );
        }

        /// <summary>
        /// Computes a cubic Bézier curve that is distance-independent.
        /// </summary>
        /// <param name="start">Starting point.</param>
        /// <param name="end">End point.</param>
        /// <param name="height">Height of the midpoint.</param>
        /// <param name="t">Interpolation factor (0 to 1).</param>
        /// <returns>Interpolated point on the Bézier curve.</returns>
        public static Vector3 CubicBezierDistIndependent(Vector3 start, Vector3 end, float height, float t)
        {
            float _1MinusT = 1 - t;
            Vector3 mid = (start + end) / 2;
            mid.y += height;

            return _1MinusT * _1MinusT * start
                + 2 * _1MinusT * t * mid
                + t * t * end;
        }

        /// <summary>
        /// Converts a Vector3 to a Vector3 with zero Y (XZ plane).
        /// </summary>
        public static Vector3 XZ(this Vector3 original) => new Vector3(original.x, 0, original.z);

        /// <summary>
        /// Converts a Vector3 to a Vector3 with zero Z (XY plane).
        /// </summary>
        public static Vector3 XY(this Vector3 original) => new Vector3(original.x, original.y, 0);

        /// <summary>
        /// Converts a Vector3 to a Vector2 (XY components).
        /// </summary>
        public static Vector2 XY2D(this Vector3 original) => new Vector2(original.x, original.y);

        /// <summary>
        /// Converts an XZ Vector3 to an XY Vector2.
        /// </summary>
        public static Vector2 XZ_To_XY(this Vector3 original) => new Vector2(original.x, original.z);

        /// <summary>
        /// Converts an XY Vector2 to an XZ Vector3.
        /// </summary>
        public static Vector3 XY_To_XZ(this Vector2 original) => new Vector3(original.x, 0, original.y);

        /// <summary>
        /// Determines if a vector is closer to point 'a' than to point 'b'.
        /// </summary>
        public static bool CompareDist(this Vector3 self, Vector3 a, Vector3 b) => (self - a).sqrMagnitude <= (self - b).sqrMagnitude;

        /// <summary>
        /// Determines if the squared distance from a point is within a threshold.
        /// </summary>
        public static bool CompareDist(this Vector3 self, Vector3 a, float b) => (self - a).sqrMagnitude <= b * b;

        /// <summary>
        /// Checks if the magnitude of a vector is within a given range.
        /// </summary>
        public static bool IsMagnitudeBetween(this Vector3 self, float minDistance, float maxDistance)
        {
            return self.sqrMagnitude > minDistance * minDistance && self.sqrMagnitude <= maxDistance * maxDistance;
        }

        /// <summary>
        /// Rotates a vector in 2D (XY plane) by a given angle in radians.
        /// </summary>
        public static Vector3 AngleToVectorXY(this Vector3 vector, float radians)
        {
            Vector3 direction = vector.normalized;
            float angleOfVector = -Vector3.SignedAngle(Vector3.right, direction, Vector3.up);
            float theta = radians + angleOfVector * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(theta), Mathf.Sin(theta), 0f);
        }

        /// <summary>
        /// Rotates a vector in 3D (XZ plane) by a given angle in radians.
        /// </summary>
        public static Vector3 AngleToVectorXZ(this Vector3 vector, float radians)
        {
            Vector3 direction = vector.normalized;
            float angleOfVector = -Vector3.SignedAngle(Vector3.right, direction, Vector3.up);
            float theta = radians + angleOfVector * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(theta), 0f, Mathf.Sin(theta));
        }

        /// <summary>
        /// Rotates a 2D vector by a given angle in radians.
        /// </summary>
        public static Vector2 AngleToVector(this Vector2 vector, float radians)
        {
            Vector2 direction = vector.normalized;
            float angleOfVector = -Vector3.SignedAngle(Vector2.right, direction, Vector2.up);
            float theta = radians + angleOfVector * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
        }

        /// <summary>
        /// Performs a distance-independent linear interpolation.
        /// </summary>
        public static Vector3 DistIndependentLerp(Vector3 start, Vector3 end, float time)
        {
            Vector3 direction = end - start;
            if (direction.sqrMagnitude < 0.01f) return end;
            time = Mathf.Clamp(time, 0f, 1);
            return start + (time / direction.magnitude) * direction;
        }
        /// <summary>
        /// Calculates the position of an object stacked in a line, given its index, a starting position, a direction, and a slot offset.
        /// </summary>
        /// <param name="index">The index of the object in the stack (0-based).</param>
        /// <param name="startPosition">The starting position of the stack.</param>
        /// <param name="direction">The direction in which to stack objects (should be normalized).</param>
        /// <param name="slotOffset">The distance between each stacked object.</param>
        /// <returns>The calculated position for the object at the given index.</returns>
        public static Vector3 GetStackedPosition(int index, Vector3 startPosition, Vector3 direction, float slotOffset)
        {
            Vector3 offset = index * slotOffset * direction;
            return startPosition + offset; // Apply rotation to the offset
        }

        /// <summary>
        /// Calculates the position of an object stacked in a line, with an additional rotation applied to the stacking direction.
        /// </summary>
        /// <param name="index">The index of the object in the stack (0-based).</param>
        /// <param name="position">The starting position of the stack.</param>
        /// <param name="direction">The direction in which to stack objects (should be normalized).</param>
        /// <param name="eulerRotation">Euler angles (in degrees) to rotate the stacking direction.</param>
        /// <param name="slotOffset">The distance between each stacked object.</param>
        /// <returns>The calculated position for the object at the given index, with rotation applied.</returns>
        public static Vector3 GetStackedPosition(this Vector3 position, int index, Vector3 direction, Vector3 eulerRotation, float slotOffset)
        {
            Vector3 offset = index * slotOffset * direction;
            Quaternion rotation = Quaternion.Euler(eulerRotation); // Convert Vector3 rotation to Quaternion
            return position + rotation * offset; // Apply rotation to the offset
        }

        /// <summary>
        /// Calculates a point along a spiral path at a given time parameter, with optional rotation and direction reversal.
        /// </summary>
        /// <param name="startPosition">The starting position of the spiral.</param>
        /// <param name="t">The normalized time parameter (0 to 1) along the spiral.</param>
        /// <param name="numberOfRopes">The number of spiral turns (affects the spiral's tightness).</param>
        /// <param name="radius">The radius of the spiral.</param>
        /// <param name="height">The total height of the spiral.</param>
        /// <param name="eulerRotation">Euler angles (in degrees) to rotate the spiral.</param>
        /// <param name="reverseDirection">If true, reverses the spiral's winding direction.</param>
        /// <returns>The calculated position along the spiral at the given time.</returns>
        public static Vector3 GetSpiralPointAtTime(this Vector3 startPosition, float t, int numberOfRopes, float radius, float height, Vector3 eulerRotation, bool reverseDirection = false)
        {
            float rad = Rad * numberOfRopes * t;
            int directionMultiplier = reverseDirection ? -1 : 1;

            // Compute local position in spiral
            Vector3 localPos = new Vector3(
                Mathf.Sin(rad * directionMultiplier) * radius,
                Mathf.Lerp(height * numberOfRopes, 0, t),
                Mathf.Cos(rad * directionMultiplier) * radius
            );

            // Apply rotation
            Quaternion rotation = Quaternion.Euler(eulerRotation);
            Vector3 rotatedPos = rotation * localPos;

            return startPosition + rotatedPos;
        }

        /// <summary>
        /// Generates a random normalized directional vector.
        /// </summary>
        public static Vector3 GetRandomDirectionalVector()
        {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }
    }
}
