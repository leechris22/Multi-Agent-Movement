using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : Seek {
    [SerializeField]
	private float avoidDistance;
    [SerializeField]
    private float lookahead;

    // Combine behaviors to create flock behavior
    override public Steering Output(Kinematic target) {
        RaycastHit2D hit = Physics2D.Raycast(player.data.position, player.data.velocity, lookahead);
        if (hit.collider) {
            target.position = hit.point + hit.normal * avoidDistance;
        } else {
            return new Steering();
        }
        return base.Output(target);
    }
}
