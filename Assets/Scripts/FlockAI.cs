using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Combines behaviors to create Flock behavior
public class FlockAI : NPCController {
    // Comparison NPCs
    public NPCController lead;
    public List<FlockAI> targets;

    // Add weight to behaviors
    public float[] weights;
    public Vector2[] strengths;

    // Distance threshold for targets
    public float threshold;

    // For separate
    public float decayCoefficient;

    // For align
    public float slowRadiusA;
    public float stopRadiusA;
    public float timeToTarget;

    // For cohesion
    public float slowRadiusL;
    public float stopRadiusL;

    // For pursue and evade functions
    public float maxPrediction;

    // On initialization
    private void Start() {
        strengths = new Vector2[4];
    }

    // Update is called once per frame
    private void FixedUpdate() {
        Flock();
    }

    // Combine behaviors to create flock behavior
    public void Flock() {
        // Define variables
        for (int i = 0; i < strengths.Length; i++) {
            strengths[i] = Vector2.zero;
        }
        float avgRotation = 0;
        int count = 0;
        
        // Obtain the averaged data for nearby boids and calculate separation velocity
        foreach (FlockAI target in targets) {
            if (Vector2.Distance(target.rb.position, rb.position) < threshold) {
                strengths[0] += Separate(target.rb.position);
                strengths[1] += target.rb.velocity;
                strengths[2] += target.rb.position;
                avgRotation += target.rb.rotation;
                count++;
            }
        }
        strengths[1] /= Mathf.Max(count, 1);
        strengths[2] /= Mathf.Max(count, 1);
        avgRotation /= Mathf.Max(count, 1);

        // Calculate strengths
        strengths[0] = weights[0] * strengths[0];
        strengths[1] = weights[1] * (VelocityMatch(strengths[1]) - rb.velocity);
        strengths[2] = weights[2] * Cohesion(strengths[2]);
        strengths[3] = weights[3] * Pursue();
        avgRotation = weights[4] * Align(avgRotation);
        updateMovement(strengths[0] + strengths[1] + strengths[2] + strengths[3], avgRotation + FaceTowards(), Time.deltaTime);
    }

    // Move away from the flock
    public Vector2 Separate(Vector2 position) {
        // Get direction and distance
        Vector2 direction = position - rb.position;

        // Get the separation strength
        float strength = Mathf.Min(decayCoefficient / (direction.magnitude * direction.magnitude), maxAccelerationL);

        // Return acceleration
        return -Vector2.ClampMagnitude(direction, strength);
    }

    // Copy the velocity of the flock
    public Vector2 VelocityMatch(Vector2 velocity) {
        return (velocity - rb.velocity) / timeToTarget;
    }

    // Copy the rotation of the flock
    public float Align(float rotate) {
        // Work out the rotation to target
        float rotation = rotate - rb.rotation;

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
            rb.angularVelocity = 0;
            return 0;
        }

        // Calculate a scaled rotation if player is inside the slowRadius
        float targetRotation = (rotationSize > slowRadiusA ? maxSpeedA : maxSpeedA * rotationSize / slowRadiusA);
        targetRotation *= rotation / rotationSize;

        // Return angular acceleration
        return (targetRotation - rb.rotation) / timeToTarget;
    }

    // Move towards the flock center
    public Vector2 Cohesion(Vector2 position) {
        // Get the direction to the target
        Vector2 direction = position - rb.position;
        float distance = direction.magnitude;

        // Check if we are there, return no steering
        if (distance < stopRadiusL) {
            return Vector2.zero;
        }

        // Calculate a scaled speed if player is inside the slowRadius
        float targetSpeed = (distance > slowRadiusL ? maxSpeedL : maxSpeedL * distance / slowRadiusL);

        // Return acceleration
        return (Vector2.ClampMagnitude(direction, targetSpeed) - rb.velocity) / timeToTarget;
    }

    // Calculate the target to pursue
    public Vector2 Pursue() {
        // Calculate prediction scalar based on current speed and target distance
        float distance = (lead.rb.position - rb.position).magnitude;
        float speed = lead.rb.velocity.magnitude;
        float prediction = (speed <= distance / maxPrediction ? maxPrediction : distance / speed);

        // Create the structure to hold our output
        Vector2 steering = (lead.rb.position + lead.rb.velocity * prediction) - rb.position;

        // Give full acceleration along this direction
        steering.Normalize();
        steering *= maxAccelerationL;

        // Return acceleration
        return steering;
    }

    // Calculate the target to face
    public float FaceTowards() {
        // Work out the direction to target
        Vector2 direction = lead.rb.position - rb.position;
        float rotation = -Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg - rb.rotation;

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
            rb.angularVelocity = 0;
            return 0;
        }

        // Calculate a scaled rotation if player is inside the slowRadius
        float targetRotation = (rotationSize > slowRadiusA ? maxSpeedA : maxSpeedA * rotationSize / slowRadiusA);
        targetRotation *= rotation / rotationSize;

        // Return angular acceleration
        return (targetRotation - rb.angularVelocity) / timeToTarget;
    }
}
