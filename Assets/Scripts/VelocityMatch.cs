using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityMathc : MonoBehaviour, AI {
    // Initialize necessary variables
    public NPCController player;
    public float timeToTarget;

    // On initialization
    private void Start() {
        player = GetComponent<NPCController>();
    }

    // Define Output
    public Steering Output(NPCController target) {
        // Create the structure to hold our output
        Steering steering = new Steering((target.rb.velocity - player.rb.velocity) / timeToTarget, 0);

        // Give full acceleration along this direction
        steering.linear = Vector2.ClampMagnitude(steering.linear, player.maxAccelerationL);

        // Return acceleration
        return steering;
    }
}
