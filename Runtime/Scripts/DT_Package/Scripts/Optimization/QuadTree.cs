using System.Collections.Generic;
using UnityEngine;
namespace Practice.QuadTreePractice
{
    public struct Rect
    {
        public float x;
        public float y;
        public float w, h;

        public Vector2 center => new Vector2(x, y);
        public Vector2 halfRes => new Vector2(w, h);

        public Rect(float x, float y, float w, float h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
        public Rect(Vector2 center, Vector2 haldRes)
        {
            x = center.x;
            y = center.y;
            w = haldRes.x;
            h = haldRes.y;
        }
        public bool Intersects(Rect point)
        {
            return !(
                point.y + point.h < y - h ||
                point.x + point.w < x - w ||
                point.y - point.h > y + h ||
                point.x - point.w > x + w);
        }
        public bool Intersects(Vector2 point)
        {
            return
                point.y <= y + h &&
                point.x <= x + w &&
                point.y >= y - h &&
                point.x >= x - w;
        }
    }



    public class QuadTree
    {
        #region fields
        public Rect boundingBox;

        public QuadTree
            topLeft,
            topRight,
            bottomRight,
            bottomLeft;


        List<Point> points;
        int capacity;
        public bool subdivided;

        #endregion


        public QuadTree(Rect boundingBox, int capacity = 3)
        {
            this.boundingBox = boundingBox;
            this.capacity = capacity;
            points = new List<Point>();
        }


        public bool Insert(Point point)
        {
            if (!Intersects(point.position)) return false;

            if (points.Count < capacity)
            {
                points.Add(point);
                return true;
            }
            else
            {
                if (!subdivided) Subdivide();

                if (topLeft.Insert(point) || topRight.Insert(point) || bottomLeft.Insert(point) || bottomRight.Insert(point))
                {
                    return true;
                }
                return false;
            }
        }
        public List<Point> Query(Rect range)
        {
            List<Point> result = new List<Point>();
            Query(range, ref result);
            return result;
        }


        private bool Intersects(Vector2 point)
        {
            return
                boundingBox.Intersects(point);
        }
        private bool Intersects(Rect point)
        {
            return point.Intersects(boundingBox);
        }
        private void Query(Rect range, ref List<Point> foundPoints)
        {
            if (!Intersects(range))
            {
                return;
            }
            else
            {
                for (int i = 0; i < points.Count; i++)
                {
                    if (range.Intersects(points[i].position))
                        foundPoints.Add(points[i]);
                }
                if (subdivided)
                {
                    topLeft.Query(range, ref foundPoints);
                    topRight.Query(range, ref foundPoints);
                    bottomLeft.Query(range, ref foundPoints);
                    bottomRight.Query(range, ref foundPoints);
                }
            }
        }
        private void Subdivide()
        {
            float x = boundingBox.x;
            float y = boundingBox.y;
            float newH = boundingBox.h / 2f;
            float newW = boundingBox.w / 2f;


            Rect topLeftRect = new Rect(x - newW, y + newH, newW, newH);
            Rect topRightRect = new Rect(x + newW, y + newH, newW, newH);
            Rect bottomLeftRect = new Rect(x - newW, y - newH, newW, newH);
            Rect bottomRightRect = new Rect(x + newW, y - newH, newW, newH);

            topLeft = new QuadTree(topLeftRect, capacity);
            topRight = new QuadTree(topRightRect, capacity);
            bottomLeft = new QuadTree(bottomLeftRect, capacity);
            bottomRight = new QuadTree(bottomRightRect, capacity);

            subdivided = true;
        }


    }
}
