using UnityEngine;
namespace DT_Helpers
{
	[RequireComponent(typeof(CanvasRenderer))]
	public class HexGraphUI : MonoBehaviour
	{
		int partition = 6;
		[SerializeField] Material mat;
		CanvasRenderer canvasRenderer;
		private void Awake()
		{
			canvasRenderer = GetComponent<CanvasRenderer>();
		}
		public void UpdateGraph(float FirstAmmount, float SecondAmmount, float ThirdAmmount, float FifthAmmount, float FourthAmmount)
		{

			CreateGraph(FirstAmmount, SecondAmmount, ThirdAmmount, FourthAmmount, FifthAmmount);
		}
		private void CreateGraph(float FirstAmmount, float SecondAmmount, float ThirdAmmount, float FifthAmmount, float FourthAmmount)
		{
			Mesh mesh = new Mesh();

			Vector3[] verts = new Vector3[6];
			Vector2[] uvs = new Vector2[6];
			int[] triangles = new int[3 * partition];

			float angleIncrement = -360f / partition;
			float radarChartSize = 100f;
			int firstIndex = 1;

			Vector3 firstPiller = Vector3.up * radarChartSize * FirstAmmount / 100;
			Vector3 secondPiller = Quaternion.Euler(0, 0, angleIncrement * 1) * Vector3.up * radarChartSize * SecondAmmount / 100;
			Vector3 thirdPiller = Quaternion.Euler(0, 0, angleIncrement * 2) * Vector3.up * radarChartSize * ThirdAmmount / 100;
			Vector3 fourthPiller = Quaternion.Euler(0, 0, angleIncrement * 3) * Vector3.up * radarChartSize * FourthAmmount / 100;
			Vector3 FifthPiller = Quaternion.Euler(0, 0, angleIncrement * 4) * Vector3.up * radarChartSize * FifthAmmount / 100;


			verts[0] = Vector3.zero;
			verts[1] = firstPiller;
			verts[2] = secondPiller;
			verts[3] = thirdPiller;
			verts[4] = fourthPiller;
			verts[5] = FifthPiller;

			uvs[0] = Vector2.zero;
			uvs[1] = Vector2.one;
			uvs[2] = Vector2.one;
			uvs[3] = Vector2.one;
			uvs[4] = Vector2.one;
			uvs[5] = Vector2.one;
			triangles[0] = 0;
			triangles[1] = firstIndex;
			triangles[2] = 2;

			triangles[3] = 0;
			triangles[4] = 2;
			triangles[5] = 3;

			triangles[6] = 0;
			triangles[7] = 3;
			triangles[8] = 4;

			triangles[9] = 0;
			triangles[10] = 4;
			triangles[11] = 5;

			triangles[12] = 0;
			triangles[13] = 5;
			triangles[14] = 1;


			mesh.vertices = verts;
			mesh.uv = uvs;
			mesh.triangles = triangles;

			canvasRenderer.Clear();
			canvasRenderer.SetMesh(mesh);
			canvasRenderer.SetMaterial(mat, 0);
		}
	}
}