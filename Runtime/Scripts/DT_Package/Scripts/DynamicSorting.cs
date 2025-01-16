using UnityEngine;

namespace DT_Helpers
{
	public class DynamicSorting : MonoBehaviour
	{
		[SerializeField] int behindOrder, frontOrder;

		public Transform player;
		public SpriteRenderer spriteRenderer;
		private void Awake()
		{
			player = GameObject.FindWithTag("Player").transform;
		}
		// Update is called once per frame
		void Update()
		{
			float result = Vector2.Dot(transform.up, (player.position - transform.position).normalized);

			int order = result > 0 ? frontOrder : behindOrder;
			spriteRenderer.sortingOrder = order;
		}
	}
}