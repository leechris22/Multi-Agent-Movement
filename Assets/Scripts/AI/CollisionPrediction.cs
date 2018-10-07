using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Predict the position of the targets and evade
public class CollisionPrediction : AI {
    // Initialize necessary variables
    [HideInInspector]
    public List<NPCController> targets;
    [SerializeField]
    private float radius;

    // Define Output
    override public Steering Output(NPCController lead) {
        // Calculate prediction scalar based on current speed and target distance
        float shortestTime = Mathf.Infinity;
        NPCController firstTarget = null;
        float firstSeparation = 0;
        float firstDistance = 0;
        Vector2 firstPosition = Vector2.zero;
        Vector2 firstVelocity = Vector2.zero;

        foreach (NPCController target in targets) {
            // Calculate the time to collision
            Vector2 position = target.rb.position - player.rb.position;
            Vector2 velocity = target.rb.velocity - player.rb.velocity;
            float timeToCollision = (Vector2.Dot(position, velocity) / (velocity.magnitude * velocity.magnitude));

            // Check if the collision will happen
            float distance = position.magnitude;
            float separation = distance - velocity.magnitude * shortestTime;
            if (separation > 2 * radius) {
                continue;
            }

            // Check if it is the shortest
            if (timeToCollision > 0 && timeToCollision < shortestTime) {
                // Store the time, target and other data
                shortestTime = timeToCollision;
                firstTarget = target;
                firstSeparation = separation;
                firstDistance = distance;
                firstPosition = position;
                firstVelocity = velocity;
            }
        }

        // Calculate the steering
        if (!firstTarget) {
            return new Steering();
        }

        // If we’re going to hit exactly, or if we’re already colliding, then do the steering based on current position.
        Vector2 relativePos = Vector2.zero;
        if (firstSeparation <= 0 || firstDistance < 2 * radius) {
            relativePos = firstTarget.rb.position - player.rb.position;
        } else {
            // Otherwise calculate the future relative position
            relativePos = firstPosition + firstVelocity * shortestTime;
        }

        // Return the steering
        return new Steering(Vector2.ClampMagnitude(relativePos, player.maxAccelerationL), 0);
    }
}
