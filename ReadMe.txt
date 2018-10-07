Press 1,2,3 to switch between/reset parts. 1 leads to Part 1, etc.

***Part 1***

Controls:
Move player - Arrow keys or WASD or press and hold with mouse

1) What are the weights of the three steering behaviors in your flocking model?

Pursue - 3
Face - 1
Separation - 5
Cohesion(Arrive) - 1.5
Velocity Match- 1

- The Boids follow the player using Pursue.
- Pursue was set to 3 so that the Boids can keep up with the player.
- Face is the only behavior that affects orientation, so the weight is set to 1.
- Separation is set to 5 to make the Boids more spread out.
- Cohesion is set to 1.5 to make the Boids look grouped.
- Velocity Match is not too important, so the weight is 1.
- Background is colored as a gradient to make the movement more clear.
- Align was originally added so that the Boids had a similar orientation to its group,
but that made the Boids turn unnatural.


***Part 2***

Controls:
Switch to Cone Check - Q
Switch to Collision Prediction - E
Move Camera Horizontally - AD or Left and Right Arrows

2) In Part 2, what did you do for avoiding a group of agents? What are the weights of
path following and evade behavior? Did you use a separation algorithm, and what
were its parameters?

To avoid a group of agents, the Cone Check or CollisionPrediction algorithms were added
as extra behaviors for Flock. The Cone Check does the dot product calculation, then calls
Evade on the target. For multiple targets, the cone check only calls Evade on the closest one.

Cone Check: Threshold = 2
Evade: Max Prediction = 1
Collision Prediction: Radius = 3

- An invisible lead uses the Path Following behavior to follow a path.
- The Flocking behavior calls Pursue on this lead. This allows the Boids to follow the path
as a group
- The Flocking behaviors and weights are:

Pursue - 3
Face - 1
Separate - 5
Arrive - 1
Velocity Match - 1
Cone Check- 2
Collision Prediction - 2

- The last two behaviors are toggled false, but can be set to true using Q or E.
- Even with the collision prevention behaviors, the Boids tend to collide with each other a lot.
- Many values have been tested, but there will usually be one or two Boids that get stuck together.

***Part 3***

Controls:
Switch to Cone Check - Q
Switch to Collision Prediction - E
Move Camera Horizontally - AD or Left and Right Arrows

3) In Part 3, how many rays did you use in your ray-casting, and why?


- 2D or 3D, implement curved shapes
- Says one agent, so only one bird
- Corner Trap demo