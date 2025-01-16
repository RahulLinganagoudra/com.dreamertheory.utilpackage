using UnityEngine;

namespace DT_Helpers
{
	//using DG.Tweening;

	public class DynamicFieldOfView : MonoBehaviour
	{
		[SerializeField] new Camera camera;
		[SerializeField] float duration = 1f;

		public void ChangeFOV(int FOVsize, float duration = -1)
		{
			//if(duration<0)
			//camera.DOOrthoSize(FOVsize, this.duration);
			//else
			//camera.DOOrthoSize(FOVsize, duration);
		}
	}
}