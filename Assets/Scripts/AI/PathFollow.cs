using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Moves to path
[RequireComponent(typeof(Arrive), typeof(FaceTowards))]
public class PathFollow : AI {
    // Initialize necessary variables
    public NPCController[] path;
    public float pathRadius;
    public int current = 0;

    // Define Output
    override public Steering Output(NPCController target) {
        Steering steering = new Steering();
 
        // If no path to follow, do nothing
        if (current >= path.Length) {
            return new Steering();
        }
    
        // Move to point until player reaches point, then target next point
        if (Vector2.Distance(path[current].rb.position, player.rb.position) > pathRadius) {
            steering += GetComponent<Arrive>().Output(path[current]);
            steering += GetComponent<FaceTowards>().Output(path[current]);
        } else {
            current++;
        }

        // Return output
        return steering;
    }
}
