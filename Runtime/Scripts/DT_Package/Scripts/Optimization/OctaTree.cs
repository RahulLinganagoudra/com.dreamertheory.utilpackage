using System.Collections.Generic;
using UnityEngine;

namespace DT_Helpers.Optimization
{
	public struct Cube
	{
		public Vector3 pos, dimension;
		public float x => pos.x;
		public float y => pos.y;
		public float z => pos.z;
		public float w => dimension.x;
		public float h => dimension.y;
		public float l => dimension.z;

		public Cube(Vector3 position, Vector3 dimension)
		{
			pos = position;
			this.dimension = dimension;
		}
		public override string ToString()
		{
			return $"Position\n{pos}\tDimension\n{dimension}";
		}

		public bool Intersect(Vector3 position)
		{
			return position.y <= y + h &&
				position.y >= y - h &&
				position.x <= x + w &&
				position.x >= x - w &&
				position.z <= z + l &&
				position.z >= z - l;
		}
		public bool Intersect(Cube point)
		{
			return !
				(
				point.y - point.h > y + h ||
				point.y + point.h < y - h ||
				point.x + point.w < x - w ||
				point.x - point.w > x + w ||
				point.z + point.l < z - l ||
				point.z - point.l > z + l
				);
		}
	}
	public class OctTreeElement<T>
	{
		public Vector3 position;
		public T data;

		public OctTreeElement(Vector3 position, T data)
		{
			this.position = position;
			this.data = data;
		}

	}
	public class OctaTree<T>
	{
		public Cube BoundingBox;
		public OctaTree<T> one, two, three, four, five, six, seven, eight;
		public bool subdivided;
		public List<OctTreeElement<T>> points;
		public int capacity;

		public OctaTree(Cube boundingBox, int capacity = 8)
		{
			BoundingBox = boundingBox;
			this.capacity = capacity;
			subdivided = false;
			points = new List<OctTreeElement<T>>(capacity);
		}


		public bool Insert(OctTreeElement<T> position)
		{
			if (!BoundingBox.Intersect(position.position)) return false;

			if (!subdivided)
			{
				if (points.Count <= capacity)
				{
					points.Add(position);
					return true;
				}
				SubDivide();
			}
			return InsertLocal(position);


		}
		private OctaTree<T> SubDivide()
		{
			float w = BoundingBox.w / 2;
			float h = BoundingBox.h / 2;
			float l = BoundingBox.l / 2;

			float x = BoundingBox.x;
			float y = BoundingBox.y;
			float z = BoundingBox.z;

			one = new OctaTree<T>(new Cube(new(x + w, y + h, z - l), new(w, h, l)), capacity);
			two = new OctaTree<T>(new Cube(new(x - w, y + h, z - l), new(w, h, l)), capacity);
			three = new OctaTree<T>(new Cube(new(x - w, y - h, z - l), new(w, h, l)), capacity);
			four = new OctaTree<T>(new Cube(new(x + w, y - h, z - l), new(w, h, l)), capacity);
			five = new OctaTree<T>(new Cube(new(x + w, y + h, z + l), new(w, h, l)), capacity);
			six = new OctaTree<T>(new Cube(new(x - w, y + h, z + l), new(w, h, l)), capacity);
			seven = new OctaTree<T>(new Cube(new(x - w, y - h, z + l), new(w, h, l)), capacity);
			eight = new OctaTree<T>(new Cube(new(x + w, y - h, z + l), new(w, h, l)), capacity);

			subdivided = true;

			foreach (var item in points)
			{
				InsertLocal(item);
			}
			points.Clear();
			points = null;

			return this;
		}
		bool InsertLocal(OctTreeElement<T> position)
		{
			if (
				one.Insert(position) ||
				two.Insert(position) ||
				three.Insert(position) ||
				four.Insert(position) ||
				five.Insert(position) ||
				six.Insert(position) ||
				seven.Insert(position) ||
				eight.Insert(position)
				)
				return true;
			return false;
		}
		public List<OctTreeElement<T>> Query(Cube range)
		{
			List<OctTreeElement<T>> result = new List<OctTreeElement<T>>();
			Query(range, ref result);
			return result;
		}

		private void Query(Cube range, ref List<OctTreeElement<T>> result)
		{
			if (!BoundingBox.Intersect(range)) return;
			else
			{
				if (!subdivided)
				{
					for (int i = 0; i < points.Count; i++)
					{
						result.Add(points[i]);
						//Gizmos.DrawSphere(points[i].position, .2f);
					}
					// Gizmos.DrawWireCube(BoundingBox.pos, BoundingBox.dimension * 2);
					return;
				}

				one.Query(range, ref result);
				two.Query(range, ref result);
				three.Query(range, ref result);
				four.Query(range, ref result);
				five.Query(range, ref result);
				six.Query(range, ref result);
				seven.Query(range, ref result);
				eight.Query(range, ref result);
			}
		}



		public void Show()
		{
			ShowLocal();
		}
		private void ShowLocal()
		{
			Gizmos.DrawWireCube(BoundingBox.pos, BoundingBox.dimension * 2);
			if (!subdivided)
				return;
			one.ShowLocal();
			two.ShowLocal();
			three.ShowLocal();
			four.ShowLocal();
			five.ShowLocal();
			six.ShowLocal();
			seven.ShowLocal();
			eight.ShowLocal();
		}
		public void CreateTree(int count)
		{
			if (count >= 2) { return; }
			if (!subdivided)
				SubDivide();
			count++;
			one.CreateTree(count);
			two.CreateTree(count);
			three.CreateTree(count);
			four.CreateTree(count);
			five.CreateTree(count);
			six.CreateTree(count);
			seven.CreateTree(count);
			eight.CreateTree(count);

		}
	}
	public static class DebugOctTree
	{
		public static void Draw(OctaTree<bool> node)
		{
			//Gizmos.DrawWireCube(node.BoundingBox.pos, node.BoundingBox.dimension * 2);

			if (node.subdivided)
			{
				Draw(node.one);
				Draw(node.two);
				Draw(node.three);
				Draw(node.four);
				Draw(node.five);
				Draw(node.six);
				Draw(node.seven);
				Draw(node.eight);
			}
			else
			{
				if (node.points != null)
					foreach (var item in node.points)
					{
						Gizmos.DrawSphere(item.position, .2f);
					}
			}
		}
	}
}
