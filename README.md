Team Description

ID Group: 8

Names of the members: 
  - Alex Alcaide Arroyes 
  - Marc Ramis Caldes
  
Emails in the same order: 
  - alex.alcaide@enti.cat 
  - marc.ramis@enti.cat
  
Pictures in the same order: 
  - https://enti.classlife.education/user/friends/alex-alcaide-arroyes-1-1496 
  - https://enti.classlife.education/user/friends/marc-ramis-caldes-1-1497
  
To start the game press space, this will make the force slider move. When you release space the scorpion will start moving. Change the effect slider with z and x. After shooting the ball, you can reset the scorpion with the button in the canvas "Reset Position". In game you can see the ball arrow of where is going without the effect before shooting the ball and after shooting you will be able to see the arrows of initial velocity, velocity and force. You can also turn Off/On the arrows with I.

Exercise 1 
It can be found in the gameObject controller in the script controller.
The direction of the target is controlled by the gameObject BlueTarget inside the Blue_Team gameObject.
To reset the scorpion, controller calls a function inside the script ScorpionWalk_Controller that is inside the gameObject Scorpion.

1, 2, 3, 4, 5, 6 Completed but Scorpion legs doesn't have initial rotation when restarting.

Exercise 2
The Magnus Effect formula can be found inside the gameObject controller in the script Magnus Effect.
The small red sphere to show where the tall will hit is controlled by the script MovingBall inside the gameObject Ball.
To toggle the arrows the input is inside the script Controller and the code is in the gameObject Ball inside the script MovingBall.

1, 3, 4, 6 Completed but in exercise 3 we don't draw blue points.

Exercise 3
The code to move the legs is in the DLL MyScorpionController in line 178.
The raysCast of the future legs is made inside the script ScorpionWalk_Controller that is inside the gameObject Scorpion. Also here is aplayed the rotation and height of the scropion depending of the legs position.

1, 2, 3, 4, 5, 6 Completed.

Exercise 4
The code to change the animations of the robots is inside IK_tentacles script, it can be found inside the gameObject Blue_team in OctopusBlueTeam.

All Completed

Formula of the instantaneuos position

We use Euler Solver 
x=xº+vt;
v=vº+at;
a = gravity + direction ball force;

Formula used to calculate the forces

We sum all the forces to later apply them in Euler. We sum the start force, the gravity and the magnus effect, that is calculed by F = (w x v) and later that force we multiplied it with a value of the slider Effect Strenght between 0 and 1.


Formula to make the animation of the legs

We calculate the distance between the leg and the future leg. If the distance is bigger than the offset (0.8f), we make a Lerp of the leg to the future leg + an offset in Y to make it higher and we sum half the distance to Z. Then if the time is the half of the total time of animation we change the to another Lerp, that starts in the current leg position and ends in the future leg without offsets. Also when the leg finished moving we put the leg correctly on the floor with a raycast.
The code can be found in the DLL MyScorpionController in line 178.

