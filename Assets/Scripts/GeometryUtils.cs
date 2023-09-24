using UnityEngine;

public class GeometryUtils
{
    public static bool SegmentRectangleIntersection(Vector2 p1, Vector2 p2, Vector2[] rectangleCorners, out Vector2 intersection)
    {
        bool intersected = false;
        intersection = p1;
        Vector2 tempResult = p1;
        if (SegmentsIntersection(p1, p2, rectangleCorners[0], rectangleCorners[1], out tempResult))
        {
            intersected = true;
            if (Vector2.Distance(tempResult, p1) > Vector2.Distance(intersection, p1))
                intersection = tempResult;
        }
        if (SegmentsIntersection(p1, p2, rectangleCorners[1], rectangleCorners[2], out tempResult))
        {
            intersected = true;
            if (Vector2.Distance(tempResult, p1) > Vector2.Distance(intersection, p1))
                intersection = tempResult;
        }
        if (SegmentsIntersection(p1, p2, rectangleCorners[2], rectangleCorners[3], out tempResult))
        {
            intersected = true;
            if (Vector2.Distance(tempResult, p1) > Vector2.Distance(intersection, p1))
                intersection = tempResult;
        }
        if (SegmentsIntersection(p1, p2, rectangleCorners[3], rectangleCorners[0], out tempResult))
        {
            intersected = true;
            if (Vector2.Distance(tempResult, p1) > Vector2.Distance(intersection, p1))
                intersection = tempResult;
        }
        return intersected;
    }

    // a1 is line1 start, a2 is line1 end, b1 is line2 start, b2 is line2 end
    public static bool SegmentsIntersection(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2, out Vector2 intersection)
    {
        intersection = Vector2.zero;

        Vector2 b = a2 - a1;
        Vector2 d = b2 - b1;
        float bDotDPerp = b.x * d.y - b.y * d.x;

        // if b dot d == 0, it means the lines are parallel so have infinite intersection points
        if (bDotDPerp == 0)
            return false;

        Vector2 c = b1 - a1;
        float t = (c.x * d.y - c.y * d.x) / bDotDPerp;
        if (t < 0 || t > 1)
            return false;

        float u = (c.x * b.y - c.y * b.x) / bDotDPerp;
        if (u < 0 || u > 1)
            return false;

        intersection = a1 + t * b;

        return true;
    }
}
