using UnityEngine;

namespace DT_Helpers
{
	public class BillboardBehaviour : MonoBehaviour
	{
		private void Update()
		{
			transform.forward = Camera.main.transform.forward;
		}
	}
}