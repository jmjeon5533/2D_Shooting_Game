using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier
{
    public static Vector3 GetBezierPoint(Vector3 p1, Vector3 p2, Vector3 p3,Vector3 p4, float t)
    {
        var p12 = Vector3.Lerp(p1, p2, t);
        var p23 = Vector3.Lerp(p2, p3, t);
        var p34 = Vector3.Lerp(p3, p4, t);

        var p1223 = Vector3.Lerp(p12, p23, t);
        var p2334 = Vector3.Lerp(p23, p34, t);

        var value = Vector3.Lerp(p1223, p2334, t);

        return value;
    }
}
