using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour{
    // Initialize necessary variables
    public NPCController player;

    // On initialization
    virtual protected void Start() {
        player = GetComponent<NPCController>();
    }

    public virtual Steering Output(NPCController target) {
        return new Steering();
    }
    /*
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
    }*/
}
