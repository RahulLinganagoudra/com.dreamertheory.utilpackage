using UnityEngine;
namespace DT_Util
{
	public static class DT_Ext
	{
		public static float SQ(this float original)
		{
			return original * original;
		}
		public static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}
	}
}