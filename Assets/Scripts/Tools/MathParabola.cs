
// Author: ditzel
// https://gist.github.com/ditzel/68be36987d8e7c83d48f497294c66e08#file-mathparabola-cs
// modified

using UnityEngine;
using System;

public static class MathParabola {

    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t) {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.LerpUnclamped(start, end, t);

        //return new Vector3(mid.x, f(t) + Mathf.LerpUnclamped(start.y, end.y, t), mid.z);
        return new Vector3(mid.x, mid.y, f(t) + Mathf.LerpUnclamped(start.z, end.z, t));
        //return new Vector3(f(t) + Mathf.LerpUnclamped(start.x, end.x, t), mid.y, mid.z);
    }

    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t) {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector2.Lerp(start, end, t);

        return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
    }

}