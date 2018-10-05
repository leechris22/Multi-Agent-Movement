using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separate : AI {
    // Initialize necessary variables
    public float decayCoefficient;

    // Define Output
    override public Steering Output(NPCController target) {
        // Get direction and distance
        Vector2 direction = target.rb.position - player.rb.position;

        // Get the separation strength
        float strength = Mathf.Min(decayCoefficient / (direction.magnitude * direction.magnitude), player.maxAccelerationL);

        // Return acceleration
        return new Steering(-Vector2.ClampMagnitude(direction, strength), 0);
    }
}
