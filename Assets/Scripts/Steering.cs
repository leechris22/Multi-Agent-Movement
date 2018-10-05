using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Steering {
    public Vector2 linear;
    public float angular;

    public Steering(Vector2 lin, float ang) {
        linear = lin;
        angular = ang;
    }
}
