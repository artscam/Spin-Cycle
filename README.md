Built with Unity version 2020.3.25f1

## The idea:
This is a proto-game about tornado chasing. The player is an off-road jeep and the environment is a woodland trail that contains a wandering tornado 
which will throw you into the air if you get too close! Luckily you can shoot grappling hooks that tether you to a point so you don't blow away - 
releasing the grapple at the right time will launch you in a targeted direction.
There is no game loop yet but I plan to explore the following ideas:
- tornado parkour (use the tornadoes to get air and navigate an obstacle course)
- extreme firefighting (launch yourself above the fire by driving into the tornado, control your flight path with grapples, and release buckets of water from on high)
The game started off as an excuse to learn compute shaders to simulate the look of the tornado with loads of debris flying around it, but it's starting to get fun to play too!

## Controls
 WASD or arrow kets to move
 Space bar to brake
 left click to deploy grapple
 right click to retract grapple

 ## To do:
 - implement hand-brake turns
 - create a more extreme obstacled terrain, add edges to the map
 - experiment with adding fires and water
 - adjust the camera mechanics for so it can rotate without inducing nausea (currently faces one direction as a safety measure!)
 - fiddle with the tornado compute shader to improve the look
 - add a colour filter shader for more cohesive visuals
 - closed game loop
 - menus
 - compile and share!

## Known bugs
- The grapple can be launched at the jeep itself, which can pin you in place even while airbourne
