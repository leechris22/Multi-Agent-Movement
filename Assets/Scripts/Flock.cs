using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Combines behaviors to create Flock behavior
public class Flock : AI {
    // Comparison NPCs
    public List<NPCController> targets;

    // Add weight to behaviors
    public float[] weights;
    public Steering[] strengths;

    // Add AI
    public AI[] ai;
    
    // Distance threshold for targets
    public float threshold;

    // On initialization
    private void Awake() {
        player = GetComponent<NPCController>();
        ai = new AI[5];
        ai[0] = GetComponent<Pursue>();
        ai[1] = GetComponent<FaceTowards>();
        ai[2] = GetComponent<Separate>();
        ai[3] = GetComponent<Cohesion>();
        ai[4] = GetComponent<VelocityMatch>();
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
        strengths[2] /= Mathf.Max(count, 1);
        strengths[3] /= Mathf.Max(count, 1);
        strengths[4] /= Mathf.Max(count, 1);

        // Calculate strengths and output
        strengths[0] = ai[0].Output(lead);
        strengths[1] = ai[1].Output(lead);
        strengths[4] -= new Steering(player.rb.velocity, 0);
        Steering steering = new Steering();
        for (int i = 0; i < 5; i++) {
            strengths[i] *= weights[i];
            steering += strengths[i];
        }
        return steering;
    }
}
