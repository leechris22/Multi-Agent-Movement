using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Face the direction the player is moving
public class FaceForward : AI {
    // Define Output
    override public Steering Output(NPCController target) {
        player.rb.MoveRotation(-Mathf.Atan2(player.rb.velocity.x, player.rb.velocity.y) * Mathf.Rad2Deg);
        return new Steering();
    }
}