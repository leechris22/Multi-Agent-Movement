using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPrediction : AI {
    // Initialize necessary variables
    [HideInInspector]
    public List<NPCController> targets;
    [SerializeField]
    private List<NPCController> radius;


    // Define Output
    override public Steering Output(NPCController target) {
        // Calculate prediction scalar based on current speed and target distance
        //float shortestTime = Mathf.Infinity;

        return new Steering();
        /*
        NPCController firstTarget = null;
        float firstTarget = None;
        float firstMinSeparation;
        float firstDistance;
        float firstRelativePos;
        float firstRelativeVel;

        for target in targets:
        33
        34 # Calculate the time to collision
        35 relativePos = target.position - character.position
        36 relativeVel = target.velocity - character.velocity
        37 relativeSpeed = relativeVel.length()
        38 timeToCollision = (relativePos.relativeVel) /
        39(relativeSpeed * relativeSpeed)
        40
        41 # Check if it is going to be a collision at all
        42 distance = relativePos.length()
        43 minSeparation = distance - relativeSpeed * shortestTime
        44 if minSeparation > 2 * radius: continue
        45
        46 # Check if it is the shortest
        47 if timeToCollision > 0 and

        timeToCollision < shortestTime:
        49
        50 # Store the time, target and other data
        51 shortestTime = timeToCollision
        52 firstTarget = target
        53 firstMinSeparation = minSeparation
        54 firstDistance = distance
        55 firstRelativePos = relativePos
        56 firstRelativeVel = relativeVel
        57
        58 # 2. Calculate the steering
        59
        60 # If we have no target, then exit
        61 if not firstTarget: return None
        62
        63 # If we’re going to hit exactly, or if we’re already
        64 # colliding, then do the steering based on current
        65 # position.
        66 if firstMinSeparation <= 0 or distance < 2 * radius:
        67 relativePos = firstTarget.position -
        68 character.position
        69
        70 # Otherwise calculate the future relative position
        71 else:
        72 relativePos = firstRelativePos +
        73 firstRelativeVel* shortestTime
        74
        75 # Avoid the target
        76 relativePos.normalize()
        77 steering.linear = relativePos * maxAcceleration
        78
        79 # Return the steering
        80 return steering*/
    }
}
