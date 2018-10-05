using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohesion : AI {
    // Initialize necessary variables
    public float slowRadiusL;
    public float stopRadiusL;
    public float timeToTarget;

    // Define Output
    override public Steering Output(NPCController target) {
        // Get the direction to the target
        Vector2 direction = target.rb.position - player.rb.position;
        float distance = direction.magnitude;

        // Check if we are there, return no steering
        if (distance < stopRadiusL) {
            return new Steering();
        }

        // Calculate a scaled speed if player is inside the slowRadius
        float targetSpeed = (distance > slowRadiusL ? player.maxSpeedL : player.maxSpeedL * distance / slowRadiusL);
        Vector2 targetVelocity = Vector2.ClampMagnitude(direction, targetSpeed);

        // Create the structure to hold our output and bound acceleration
        Steering steering = new Steering((targetVelocity - player.rb.velocity) / timeToTarget, 0);
        steering.linear = Vector2.ClampMagnitude(steering.linear, player.maxAccelerationL);

        // Return acceleration
        return steering;
    }
}
