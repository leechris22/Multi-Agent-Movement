using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : AI {
	//collision detector
	// *have no idea what exactly I have to do with this
	public Rigidbody rb;
	
	//minimum diatance to a wall
	//how far to avoid collision
	//shoule be greater than the radius of the character
	public float AvoidDistance = 3.0f;
	
	//the distance to look ahead for a collision
	//the length of the collision ray
	public float Lookahead = 5.0f;
	
	// Use this for initialization
	// override this awakes
	override protected void Awake () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//1. Calculate the target to delegate to seek
		Vector2 rayVector = rb.velocity;
		rayVector.norma;oze();
		rayVector *= Lookahead;
		
		//find the collision
		collision = collisionDetector.getCollision(character.position, rayVector);
		
		//if there is no collision, do nothing
		if(! collision)
			return null;
		else{
			target = collision.position+collision.normal*AvoidDistance;
			return Seek.getSteering();
		}
	}
}
