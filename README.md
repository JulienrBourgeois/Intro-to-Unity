# Intro-to-Unity
My intro to Unity was amazing! I started off completing the base Unity course, using C# on unity learn, in just under 2 months. After that, I began to dive into several little mini projects. Unfortunately, I do not have visuals on all (only some) of these projects. Here is where I will talk them out and post the visuals that I do have on my intro to Unity programming! Because there were so many different topics that I did personal projects on in Unity, I will separate this read me into sections.




Player Controllers:
After completing the intro to game design course on Unity Learn, the first thing I wanted to dive into was player movement and camera following. Not knowing that there were pre-made player controllers to use on the Unity Asset store, I attenpted and succeeded in making a smooth player controller on my own. I envisioned building a movement system similar to the layouts of fortnite, minecraft, and call of duty. 

To start off, I simply added a transform.Translate function to the update frame of my script.Using horizontal inputs (a,d, left arrow, right arrow), vertical inputs (w,s, front arrow, back arrow), and pre determined speeds, I was able to create a player that could move left, right, front, back, and in all diagonals.
However, this was not enough for me. I wanted the movement to be realistic and feel smooth for the player. 

There were many problems with my original player controller. 1) Moving diagonally would compound the 2 translate functions, resulting in much greater movement speed. 2) There was no sprint feature to make the speed faster. 3) There were no jump or crouching features. 4) The player could move as fast backwards as it could forwards. I wanted to fix these problems to have the best possible custom player controller. 

To fix the first problem, I had to check if the player was moving in a diagonal direction. To do this, I checked if both horizontal and vertial input was greater than zero. If so, I would limit the currentSpeed of the player using a multiplier that would set the speed similar to that of the forward speed of the player. I set this multiplier through trial an error.

To fix the second prblem, I had to check if the user was inputting a sprint button. For simplicity, I chose shift left and right as my 2 sprint buttons. At any time that this button was being pressed down, I would set a sprint bool to true and increase the general speed of the player by a sprint multiplier. I set this multiplier through trial and error. 

To fix the third problem, first, I added a rigid body to the player object, giving it physics such as gravity and mass. This allowed me to then use the AddForce function to resemble the movement of a jump during runtime. Through trial and error, I found an optimal force to make the jump feel smooth. I added this addforce function to an if statement of the player pressing the space key. If the space key was clicked, the player object would jump up and fall back down. However, the player could still jump while they were in mid air, infinetly jumping into the sky. To fix this, I added a bool called isGrounded and set that to true every time the player's mesh collided with the ground's mesh. Although crouhing is carried by its animation, I had to account for the speed part of it to. To control crouching, I used a modulus and a crouch count variable to determine if the player was crouched or not. When the user pressed the c button, the player is now crouched, resulting in the inability to jump and less movement speed. 

To fix te fourth problem, all I had to do was check if the player was moving back left, back , or back right, and set the speed to a lower speed using a multiplier. This multiplier was determined through trial and error. 

And there you have it! I completed my own movement system of a player controller just two weeks into my Unity journey! The movement after all was smooth, proportionate, and just felt right to the user! 


Animating:
After completing a movement system for my player controller, I wanted to bring it to life! Although I would be very interested in creating my own animations, I just used pre made animations from the Unity Asset store for this task. I purchased movements for every aspect of my controller: walking, running, jumping, andn crouching. I had running and walking animations for movements in all directions!

To start animating my player, I made an an animation controller and added it to my player object. After messing around with the controller, making webs with up to 50 nodes, I soon relaized I could make a much more simple controller using any state nodes. Any animation could be reached from any other animation, given that its parameters were met. To set these parameters, I had to go into my script on my player, access my paramters and set them to their values based on use input. For example, if the user was sprinting, the animation bool isSprint would be true. This would then mean that any running animation could be accessed based on what horizontal and vertical input was occuring. However, just like everything, I still had some problems.
1) Animations would occur at the same and overlap. 2) Adding sound to my animations was difficult. 

To fix the first problem, I had to set the exit and enter times to good enough values to see a smooth transition into and out of all animations. After trial and error, switching between animations while running and walking felt very smooth. When jumping, my player would transition to running mid air if I was moving forward and holding sprint. To fix this problem, I lengthened the jump animation and messed with its exit time to make sure that it could not changed to another animation unless it was finished playing. I did this through trial and error.

To fix the second problem, I could not just simply add the sound code to my animation code in my script. Sounds would eventually overlap, casuing a crash in the program and their syncing would seem slightly off. Instead, after thousands of google searches, I was introduced to animation events. Using animation events, I could pin the exact keyframe in the animation where the sound should occur and call a function in the script which would execute that sound. 
For example, I opened my walking animation and keyframe pinned when the foot hit the ground. I then made a function in my script called step() and played a step sound.

There it was! A fully completed player controller with crouching features, jumping features, movement and syncing sounds! Although I could have just used a pre made controller, this was a great learning experience and I still use this same script for all my games to this day!





3D Object Oriented Programming:



Raycasting:



Binary Formatting:



Random Seeding:



Perlin Noise:



Art and Design:





