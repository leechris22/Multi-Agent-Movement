using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Stores data for an object used in AI calculations
public class NPCController : MonoBehaviour {
    // Store variables for objects
    private AI ai;
    public Rigidbody2D rb;

    // Bounds linear changes
    public float maxSpeedL;
    public float maxAccelerationL;

    // Bounds angular changes
    public float maxSpeedA;
    public float maxAccelerationA;

    // For acceleration computations
    private Vector2 linear;
    private float angular;

    // For AI behavior
    public int state;

    // On initialization
    private void Start() {
        ai = GetComponent<AI>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        switch (state) {
            case 1:
                linear = ai.Pursue();
                angular = ai.FaceTowards();
                break;
            case 2:
                linear = ai.Evade();
                angular = ai.FaceAway();
                break;
            case 3:
                linear = ai.Arrive();
                angular = ai.FaceTowards();
                break;
            case 4:
                angular = ai.Wander(out linear);
                break;
            case 5:
                angular = ai.pathFollow(out linear);
                break;
        }
        updateMovement(linear, angular, Time.deltaTime);
    }

    protected void updateMovement(Vector2 linear, float angular, float time) {
        // Bound the acceleration
        if (linear.magnitude > maxAccelerationL) {
            linear.Normalize();
            linear *= maxAccelerationL;
        }
        float angularAcceleration = Mathf.Abs(angular);
        if (angularAcceleration > maxAccelerationA) {
            angular /= angularAcceleration;
            angular *= maxAccelerationA;
        }

        // Bound the velocity
        if (rb.velocity.magnitude > maxSpeedL) {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeedL);
        }
        if (rb.angularVelocity > maxSpeedA) {
            rb.angularVelocity = maxSpeedA;
        }

        // Update the position and rotation
        rb.AddForce(linear * rb.mass);
        rb.angularVelocity += angular;
    }
}