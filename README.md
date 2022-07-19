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
Having recently completed my first OOP course with java in October, I soon realized how much Unity objects and scripts correlated to the OOP features of the java language. You could use classes and structures to represnt objects and keep their important values, that would express their attributes during runtime. 

Since I had created a player moving around with animations, I figured, why not let this player run around and hit trees? My first OOP experience with unity was with these trees. To start off, I made a script and class, calling it trees. I then made a prefab for a tree and added the script to it. Once the game started, I would instantiate 20 trees within the boundaries of the plane and create a class for all 20 of them, storing these 20 class objects inside of an interactable list. 

For the tree class there were some variables I could take into account and access later on in my program. Every tree had a name, a health, a location etc.
When instantiating the trees I created a class with these variables and inputted that new class into a list. Inside of the tree script I then made a damage funtion that would be called every time the player's hand collided with that tree. This was a great introduction to 3D OOP as I was able to understand how objects and scripts interact with one another through run time. I also had good experience with making custom classes due to my previous Java course.

Now, its time to move on to a better way to hit trees with the player! Raycasting!




Raycasting:
Although I had a system in which the player could hit trees and break them with its hand, it was very scuffed and did not work accurately enough. The player would have to practically be on the tree to hit it and it was not very realistic. After doing some research, I ran into the raycasting feature on Unity. I found it quite fasinaating! Being able to shoot a point vector into 3D space until it collided with something is a very useful feature!

It took my quite a bit to figure it out but eventually I had a function that could shoot ray casts. I even still use this funtion to this day in all of my escape room projects! However, after creating this function it was not very perfect. Several problems arose : 1) I wanted to send the raycast to the position of the mouse 2) The raycast would sometimes hit the player object and not continue on 3) Every time the hit button as clicked, only one thin raycast would be sent, resulting in a small room for error with the aim. 

To fix the first problem, I first had to figure out how to lock the mouse into the screen during run time. This was pretty simple as I found the syntax, which I still use to this day, through several google searches. All it does is set the Cursor lock mode to locked and the mouse is loked once the user clicks on the screen for the first time. 

To fix the second prblem, I had to create a layer mask that would exclude the player object from it. I then had to add this layer mask to my raycast function. After implementing this syntax, my second problem was fixed. 

To fix the third problem, I sent out 5-10 raycasts at a time to decrease the difficulty for the user. For example, if the player shot a raycast in the general direction of the tree and was close enough to it, I wanted the player to be able to damage it. I didn't want to make it a task in itself for the player to hit suh a large, non-moving object. This made the gameplay feel a lot smoother.  


Binary Formatting: 
After experimenting with basic movement systems and OOP in Unity, I wanted to learn how to save data to a local file, which could then be extracted and reloaded every time the game started. To start off, I usesd player preferences, which was a very simple "defaults" syntax that could save things to a loaded dictionary. However, after trying to save tons of things to player preferences and trying to reload the game, I soon realized that player preferences was not strong enough to hold large amounts of data.

As an alternative, I looked towards Binary Formatting to save my data to the file. Binary Formatting takes a class, serializes it into a file of 1's and 0's, and saves it to a local file. Then, when you want to reaccess that data, you can deserialize it back into the class it once was and back into the script. Once I figured out the syntax to save and load any given class to a local file using Binary Formatting, I started with little things such as player location (Vector3), player health (Float) and the amount of trees in the scene. Now, every time I reloaded the program, the player would be in its previous position, all the trees' positions would be saved, and the player would have the same amount of health. 

Binary formatting is a great way to save data in Unity. Being very straight forward, it was easy to get the hang of it and get used to it all. My next task after this was to learn about terrain. I had figured out how to make a player, add objects and save data. Now I wanted to know how to regulate and make terrain in which the player could navigate through.

Random Seeding:



Perlin Noise:

Growing up playing games like Minecraft, I was fascinated by the idea of infinite terrain generation. While doing research over this topic, I came across the concept of perlin noise, or gradual random terrain generation. To apply this to my game, I found perlin noise syntax that could take a terrain, and fix its terrain data vertext points using perlin noise to create a gradual, smooth looking terrain. At first, I didn't quite understand how it worked, but I started to trial and error with the parameters and found that I could make different looking land based on the depth, height and width of my terrain. 

I now had a player on a perlin noise 256 x 256 terrain that could naviagate around and break trees. One more thing I wanted to accomplish was the infinite generation of this terrain (like Minecraft), so this player could run around freely forever. After about 4 days of problems and setbacks, I finally came up with a solid algorithm and idea as to how to achieve this. I would break each terrain into a 4 block quadrant, tracking which quarant the player was on at all times. Then, based on the player's quadrant I would check the distance from the two edges. If the player came close enough, I would spawn a new terrain. The algorithm worked very well, but there wer several problems. 1) After 5 minutes of running on forever, my game would de-render and crash. 2) The perlin noise would change for all spawned terrains when a new piece spawned in. 3) I could not diagonally spawd terrains. 

To fix the first problem, I had to add all of the spawned terrains to a List. I then made a function to iterate through this list and check the distance of each terrain from the player. If that terrain was far enough, we would de-render and stop all functioning of the terrain to free up the memory.

To fix the second problem , I had to to stop using a terrain prefab and had to instantiate a terrain during runtime and call the perlin noise function on it. That way, every new spawned terrain wasn't pointing to the prefab's terrain data and each had some of their own.

To fix the third problem, I had to add one more conditional statement to my infinite terrain algorithm. If the player was close enough to both edges simaltaneously, I would spawn a diagonal terrain as well. 

At this point, I had an infinite terrain using perlin noise that the player could run around on. However, the terrain felt very dull and I wanted to make it look better. For about 2 weeks, I took my sights off of programming and learned the design side of Unity!

Art and Design:

Being a territory I had never corssed paths with, I was so fascinated by Unity's capabilites in the art aspects of game design. To start my journey with design, I watched videos on terrain generation using 3D height map tools such as Map Magic and Mpa Magic 2. I learned how to make beautiful, mountainous terrains using texture nodes and height nodes alone. Eventually, I had my game use these complex heightmaps instead of dull terrain objects.





