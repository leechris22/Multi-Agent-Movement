using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTowards : AI {
    // Initialize necessary variables
    public float slowRadiusA;
    public float stopRadiusA;
    public float timeToTarget;

    // Define Output
    override public Steering Output(NPCController target) {
        // Work out the direction to target
        Vector2 direction = target.rb.position - player.rb.position;
        float rotation = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg - player.rb.rotation;

        // Map the result to the (-180, 180) interval
        while (rotation > 180) {
            rotation -= 360;
        }
        while (rotation < -180) {
            rotation += 360;
        }
        float rotationSize = Mathf.Abs(rotation);

        // Check if we are there, return no steering
        if (rotationSize < stopRadiusA) {
            player.rb.angularVelocity = 0;
            return new Steering();
        }

        // Calculate a scaled rotation if player is inside the slowRadius
        float targetRotation = (rotationSize > slowRadiusA ? player.maxSpeedA : player.maxSpeedA * rotationSize / slowRadiusA);
        targetRotation *= rotation / rotationSize;

        // Create the structure to hold our output and bound acceleration
        Steering steering = new Steering(Vector2.zero, (targetRotation - player.rb.angularVelocity) / timeToTarget);
        if (Mathf.Abs(steering.angular) > player.maxAccelerationA) {
            steering.angular /= Mathf.Abs(steering.angular);
            steering.angular *= player.maxAccelerationA;
        }
        
        // Return angular acceleration
        return steering;
    }
}
