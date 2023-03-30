using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easing
{
    public static float InQuad(float t) => t * t;
    public static float OutQuad(float t) => 1 - (t - 1) * (t - 1);
}
