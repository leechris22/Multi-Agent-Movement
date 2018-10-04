using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls for the player
public class PlayerController : NPCController {

    // Move the player
    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        rb.AddForce(new Vector2(moveHorizontal, moveVertical) * maxSpeedL);
        rb.MoveRotation(-Mathf.Atan2(rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg);
    }

}