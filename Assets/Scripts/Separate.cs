using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separate : MonoBehaviour, AI {
    // Initialize necessary variables
    public NPCController player;
    public float decayCoefficient;

    // On initialization
    private void Start() {
        player = GetComponent<NPCController>();
    }

    // Define Output
    public Steering Output(NPCController target) {
        // Get direction and distance
        Vector2 direction = target.rb.position - player.rb.position;

        // Get the separation strength
        float strength = Mathf.Min(decayCoefficient / (direction.magnitude * direction.magnitude), player.maxAccelerationL);

        // Return acceleration
        return new Steering(-Vector2.ClampMagnitude(direction, strength), 0);
    }
}
