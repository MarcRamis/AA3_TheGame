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
  
  
Formula of the instantaneuos position

We use Euler Solver 
x=xº+vt;
v=vº+at;
a = gravity + direction ball force;

Formula used to calculate the forces

We sum all the forces to later apply them in Euler. We sum the start force, the gravity and the magnus effect, that is calculed by F = (w x v) and later that force we multiplied it with a value of the slider Effect Strenght between 0 and 1.


Formula to make the animation of the legs

We calculate the distance between the leg and the future leg. If the distance is bigger than the offset (0.8f), we make a Lerp of the leg to the future leg + an offset in Y to make it higher. Then if the distance is the half of the offset (0.8f) we change the last future leg (future leg + an offset) to his original position without the offset in Y.
The code can be found in the DLL MyScorpionController in line 178.

