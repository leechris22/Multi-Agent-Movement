using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Combines behaviors to create Flock behavior
[RequireComponent(typeof(Pursue))]
[RequireComponent(typeof(Separate), typeof(Arrive), typeof(VelocityMatch))]
public class Flock : AI {
    // Flock groups
    [HideInInspector]
    public List<NPCController> targets;

    // Behavior calculations
    [SerializeField]
    private AI facing;
    [SerializeField]
    private float[] weights;
    [SerializeField]
    private AI[] ai;
    private Steering[] strengths;

    // Distance threshold for targets
    [SerializeField]
    private float threshold;

    // On initialization
    override protected void Awake() {
        player = GetComponent<NPCController>();
        strengths = new Steering[weights.Length];
    }

    // Combine behaviors to create flock behavior
    override public Steering Output(NPCController lead) {
        // Define variables
        for (int i = 0; i < strengths.Length; i++) {
            strengths[i] = new Steering();
        }
        int count = 0;
        
        // Obtain the averaged data for nearby boids and calculate separation velocity
        foreach (NPCController target in targets) {
            if (Vector2.Distance(target.rb.position, player.rb.position) < threshold) {
                strengths[2] += ai[2].Output(target);
                strengths[3] += ai[3].Output(target);
                strengths[4] += ai[4].Output(target);
                count++;
            }
        }
        strengths[3] /= Mathf.Max(count, 1);
        strengths[4] /= Mathf.Max(count, 1);

        // Calculate strengths and output
        strengths[0] = ai[0].Output(lead);
        strengths[1] = ai[1].Output(lead);
        if (strengths.Length > 5) {
            strengths[5] = ai[5].Output(lead);
        }
        Steering steering = new Steering();
        for (int i = 0; i < weights.Length; i++) {
            strengths[i] *= weights[i];
            steering += strengths[i];
        }
        return steering;
    }
}
