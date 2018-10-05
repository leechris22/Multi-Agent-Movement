using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : MonoBehaviour, AI {
    public NPCController player;
    public float maxPrediction;

    // On initialization
    private void Start() {
        player = GetComponent<NPCController>();
    }

    // Define Output
    public Steering Output(NPCController target) {
        // Calculate prediction scalar based on current speed and target distance
        float distance = (target.rb.position - player.rb.position).magnitude;
        float speed = target.rb.velocity.magnitude;
        float prediction = (speed <= distance / maxPrediction ? maxPrediction : distance / speed);

        // Create the structure to hold our output
        Steering steering = new Steering(player.rb.position - (target.rb.position + target.rb.velocity * prediction), 0);

        // Give full acceleration along this direction
        steering.linear.Normalize();
        steering.linear *= player.maxAccelerationL;

        // Return acceleration
        return steering;
    }
}
