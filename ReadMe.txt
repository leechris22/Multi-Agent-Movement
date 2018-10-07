

2) In Part 2, what did you do for avoiding a group of agents? What are the weights of
path following and evade behavior? Did you use a separation algorithm, and what
were its parameters?

3) In Part 3, how many rays did you use in your ray-casting, and why?

Press 1,2,3 to switch between/reset parts. 1 leads to Part 1, etc.

Part 1
Controls:
Move player- Arrow keys or WASD or press and hold with mouse

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

2)

Part 2
- How to move sinusoidal, path finding using points.
- Show indicators
Part 3
- 2D or 3D, implement curved shapes
- Says one agent, so only one bird
- 1, 3, 4, 5
- The width of object 4
- Corner Trap demo