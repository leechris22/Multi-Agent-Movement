using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a cone to detect collisions
[RequireComponent(typeof(Evade))]
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
        NPCController nearest = null;

        // Obtain the data for nearest boid and calculate separation velocity
        foreach (NPCController target in targets) {
            if (Vector2.Dot(orientation, (target.rb.position - player.rb.position)) > threshold) {
                if (!nearest || Vector2.Distance(player.rb.position, target.rb.position) < Vector2.Distance(player.rb.position, nearest.rb.position)) {
                    nearest = target;
                }
            }
        }
        if (!nearest) {
            return new Steering();
        }

        return GetComponent<Evade>().Output(nearest);
    }
}
