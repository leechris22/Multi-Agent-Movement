using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls for the camera
public class CameraController : MonoBehaviour {

    // The camera to move to the player
    public GameObject player;

    // The offset between the camera and the player
    private Vector3 offset;

    // On initialization
    void Start() {
        offset = transform.position;
    }
    
    // Move camera with the player
    void LateUpdate() {
        transform.position = player.transform.position + offset;
    }
}
