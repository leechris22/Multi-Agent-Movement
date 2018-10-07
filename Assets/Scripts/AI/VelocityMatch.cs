using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMatch : AI {
    // Initialize necessary variables
    [SerializeField]
    private float timeToTarget;

    // Define Output
    override public Steering Output(NPCController target) {
        // Create the structure to hold our output
        Steering steering = new Steering((target.rb.velocity - player.rb.velocity) / timeToTarget, 0);

        // Give full acceleration along this direction
        steering.linear = Vector2.ClampMagnitude(steering.linear, player.maxAccelerationL);

        // Return acceleration
        return steering;
    }
}
