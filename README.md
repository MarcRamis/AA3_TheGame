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

