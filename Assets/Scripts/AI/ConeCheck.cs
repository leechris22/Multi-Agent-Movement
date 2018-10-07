using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a cone to detect collisions
[RequireComponent(typeof(Separate))]
public class ConeCheck : AI {
    // Avoid group
    [HideInInspector]
    public List<NPCController> targets;
    [SerializeField]
    private float threshold;

    // Define Output
    override public Steering Output(NPCController lead) {
        // Define variables
        Vector2 orientation = new Vector2(Mathf.Sin(player.rb.rotation), Mathf.Cos(player.rb.rotation));
        Steering result = new Steering();
        int count = 0;

        // Obtain the averaged data for nearby boids and calculate separation velocity
        foreach (NPCController target in targets) {
            if (Vector2.Dot(orientation, (target.rb.position - player.rb.position)) < threshold) {
                result += GetComponent<Separate>().Output(target);
                count++;
            }
        }
        return result /= Mathf.Max(count, 1);
    }
}
