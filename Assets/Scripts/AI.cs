using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    // Comparison NPCs
    public NPCController player;
    public NPCController target;

    // For pursue and evade functions
    public float maxPrediction;

    // For arrive function
    public float slowRadiusL;
    public float stopRadiusL;
    public float timeToTarget;

    // For Face function
    public float slowRadiusA;
    public float stopRadiusA;

    // For wander function
    private float wanderOrientation;
    public float wanderOffset;
    public float wanderRadius;
    public float wanderRate;

    // Holds the path to follow
    public GameObject[] Path;
    public int current = 0;

    protected void Start() {
        player = GetComponent<NPCController>();
        wanderOrientation = player.rb.rotation;
    }

    // Calculate the target to pursue
    public Vector2 Pursue() {
        // Calculate prediction scalar based on current speed and target distance
        float distance = (target.rb.position - player.rb.position).magnitude;
        float speed = player.rb.velocity.magnitude;
        float prediction = (speed <= distance / maxPrediction ? maxPrediction : distance / speed);

        // Create the structure to hold our output
        Vector2 steering = (target.rb.position + target.rb.velocity * prediction) - player.rb.position;

        // Give full acceleration along this direction
        steering.Normalize();
        steering *= player.maxAccelerationL;

        // Return acceleration
        return steering;
    }

    // Calculate the target to evade
    public Vector2 Evade() {
        // Calculate prediction scalar based on current speed and target distance
        float distance = (target.rb.position - player.rb.position).magnitude;
        float speed = player.rb.velocity.magnitude;
        float prediction = (speed <= distance / maxPrediction ? maxPrediction : distance / speed);

        // Create the structure to hold our output
        Vector2 steering = player.rb.position - (target.rb.position + target.rb.velocity * prediction);

        // Give full acceleration along this direction
        steering.Normalize();
        steering *= player.maxAccelerationL;

        // Return acceleration
        return steering;
    }

    // Calculate the target to arrive
    public Vector2 Arrive() {
        // Get the direction to the target
        Vector2 direction = target.rb.position - player.rb.position;
        float distance = direction.magnitude;

        // Check if we are there, return no steering
        if (distance < stopRadiusL) {
            player.rb.velocity = Vector2.zero;
            return Vector2.zero;
        }

        // Calculate a scaled speed if player is inside the slowRadius
        float targetSpeed = (distance > slowRadiusL ? player.maxSpeedL : player.maxSpeedL * distance / slowRadiusL);
        direction.Normalize();
        direction *= targetSpeed;

        // Return acceleration
        return (direction - player.rb.velocity) / timeToTarget;
    }

    // Calculate the target to face
    public float FaceAway() {
        // Work out the direction to target
        Vector2 direction = player.rb.position - target.rb.position;
        float rotation = Mathf.Atan2(direction.x, direction.y)*Mathf.Rad2Deg - player.rb.rotation;

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
            return 0;
        }

        // Calculate a scaled rotation if player is inside the slowRadius
        float targetRotation = (rotationSize > slowRadiusA ? player.maxSpeedA : player.maxSpeedA * rotationSize / slowRadiusA);
        targetRotation *= rotation / rotationSize;

        // Return angular acceleration
        return (player.rb.rotation - targetRotation) / timeToTarget;
    }

    // Calculate the target to face
    public float FaceTowards() {
        // Work out the direction to target
        Vector2 direction = target.rb.position - player.rb.position;
        float rotation = -Mathf.Atan2(direction.x, direction.y)*Mathf.Rad2Deg - player.rb.rotation;

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
            return 0;
        }

        // Calculate a scaled rotation if player is inside the slowRadius
        float targetRotation = (rotationSize > slowRadiusA ? player.maxSpeedA : player.maxSpeedA * rotationSize / slowRadiusA);
        targetRotation *= rotation / rotationSize;

        // Return angular acceleration
        return (targetRotation - player.rb.angularVelocity) / timeToTarget;
    }

    // Calculate the target to face
    public float Wander(out Vector2 linear) {
        // Update the wander orientation
        wanderOrientation += (Random.value - Random.value) * wanderRate;

        // Calculate the wander circle
        float orientation = wanderOrientation + player.rb.rotation;
        Vector2 position = player.rb.position + wanderOffset * new Vector2(Mathf.Sin(player.rb.rotation), Mathf.Cos(player.rb.rotation));
        position += wanderRadius * new Vector2(Mathf.Sin(orientation), Mathf.Cos(orientation));

        // Work out the direction to target
        Vector2 direction = position - player.rb.position;
        float rotation = Mathf.Atan2(direction.x, direction.y)*Mathf.Rad2Deg - player.rb.rotation;

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
        }

        // Calculate a scaled rotation if player is inside the slowRadius
        float targetRotation = (rotationSize > slowRadiusA ? player.maxSpeedA : player.maxSpeedA * rotationSize / slowRadiusA);
        targetRotation *= rotation / rotationSize;

        // Now set the linear acceleration to be at full acceleration in the direction of the orientation
        linear = player.maxAccelerationA * new Vector2(Mathf.Sin(orientation), Mathf.Cos(orientation));

        // Return angular acceleration
        return (targetRotation - player.rb.rotation) / timeToTarget;
    }

    // Calculate the Path to follow
    public float pathFollow(out Vector2 linear) {
        linear = Vector2.zero;
        // If no path to follow, do nothing
        if (current >= Path.Length) {
            return 0;
        }

        // Work out the direction to target
        Vector2 direction = new Vector2(Path[current].transform.position.x, Path[current].transform.position.y) - player.rb.position;
        float rotation = Mathf.Atan2(direction.x, direction.y)*Mathf.Rad2Deg - player.rb.rotation;

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
        }

        // Calculate a scaled rotation if player is inside the slowRadius
        float targetRotation = (rotationSize > slowRadiusA ? player.maxSpeedA : player.maxSpeedA * rotationSize / slowRadiusA);
        targetRotation *= rotation / rotationSize;

        if ((new Vector2(Path[current].transform.position.x, Path[current].transform.position.y) - player.rb.position).magnitude > stopRadiusL) {
            // Give full acceleration along this direction
            direction.Normalize();
            direction *= player.maxAccelerationA;

            // Get the direction to the target
            linear = direction;
        } else {
            current++;
        }

        // Return angular acceleration
        return (targetRotation - player.rb.rotation) / timeToTarget;
    }
}
