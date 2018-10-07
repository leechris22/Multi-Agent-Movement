using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : AI {
    // Initialize necessary variables
    [SerializeField]
    private float maxPrediction;

    // Define Output
    override public Steering Output(NPCController target) {
        // Calculate prediction scalar based on current speed and target distance
        float distance = (target.rb.position - player.rb.position).magnitude;
        float speed = target.rb.velocity.magnitude;
        float prediction = (speed <= distance / maxPrediction ? maxPrediction : distance / speed);

        // Create the structure to hold our output and bound acceleration
        Steering steering = new Steering(player.rb.position - (target.rb.position + target.rb.velocity * prediction), 0);
        steering.linear = Vector2.ClampMagnitude(steering.linear, player.maxAccelerationL);

        // Return acceleration
        return steering;
    }
}
