using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Steering {
    public Vector2 linear;
    public float angular;

    public Steering(Vector2 lin, float ang) {
        linear = lin;
        angular = ang;
    }

    public static Steering operator+(Steering a, Steering b) {
        Steering steering = new Steering();
        steering.linear = a.linear + b.linear;
        steering.angular = a.angular + b.angular;
        return steering;
    }

    public static Steering operator-(Steering a, Steering b) {
        Steering steering = new Steering();
        steering.linear = a.linear - b.linear;
        steering.angular = a.angular - b.angular;
        return steering;
    }

    public static Steering operator/(Steering a, float b) {
        Steering steering = new Steering();
        steering.linear = a.linear / b;
        steering.angular = a.angular / b;
        return steering;
    }

    public static Steering operator*(Steering a, float b) {
        Steering steering = new Steering();
        steering.linear = a.linear * b;
        steering.angular = a.angular * b;
        return steering;
    }

}
