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

    // For AI behavior
    public int state;

    // On initialization
    private void Start() {
        ai = GetComponent<AI>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        updateMovement(ai.Output(this), Time.deltaTime);
    }

    protected void updateMovement(Steering steering, float time) {
        // Bound the acceleration
        if (steering.linear.magnitude > maxAccelerationL) {
            steering.linear.Normalize();
            steering.linear *= maxAccelerationL;
        }
        float angularAcceleration = Mathf.Abs(steering.angular);
        if (angularAcceleration > maxAccelerationA) {
            steering.angular /= angularAcceleration;
            steering.angular *= maxAccelerationA;
        }

        // Bound the velocity
        if (rb.velocity.magnitude > maxSpeedL) {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeedL);
        }
        if (rb.angularVelocity > maxSpeedA) {
            rb.angularVelocity = maxSpeedA;
        }

        // Update the position and rotation
        rb.AddForce(steering.linear * rb.mass);
        rb.angularVelocity += steering.angular;
    }
}