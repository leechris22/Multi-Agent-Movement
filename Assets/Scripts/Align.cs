using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour, AI {
    // Initialize necessary variables
    public NPCController player;
    public float slowRadiusA;
    public float stopRadiusA;
    public float timeToTarget;

    // On initialization
    private void Start() {
        player = GetComponent<NPCController>();
    }

    // Define Output
    public Steering Output(NPCController target) {
        // Work out the rotation to target
        float rotation = target.rb.rotation - player.rb.rotation;

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

        // Return angular acceleration
        return new Steering(Vector2.zero, (targetRotation - player.rb.angularVelocity) / timeToTarget);

    }
}
