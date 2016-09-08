# unity-ufo


UFO is an expansion of the Unity Technologies 2D UFO tutorial as explained here:
https://unity3d.com/learn/tutorials/projects/2d-ufo-tutorial

Each version has been stored in a branch of the version name so that you can see the changes made historically and get an idea of how the app developed over time based on the backlog.

#Big Picture / Roadmap
I want to create a space game where kids (including Autistic ones) can be assessed for their base skills - ie can they recognize colours, shapes, numbers, letters and even words.

The game will have different levels of increasing difficulty with the inital goal being to match what they need to "collect" by sight and then by aural request.



#Version 0.2 [ Curren Dev Branch ]

#DONE:
* Replace artwork with temporary art as not sure on the rights issues there. ie background, player and asteroid (Pickup)
* Change so randomly generates pickups of the quantity as set by param
* changed the map size
* changed the edge of the map so visually you see the background all the way to the edge of the screen and then bounce of an invisible "edge"
* Game now restarts automatically when you have collected a proportion of the total pickups


#BACKLOG - Wish List:
* add explosions for pickups (just use one from space shooter initially but then move to a fun version)
* change way player works so has constant velocity (doesn't slow down). (ie mass of 0 and drag of 0?)
* introduce range of pickup objects (different colour) and distribute randomly in equal proportion (include option to make rare?)

* optimise mass and speed for handling of the player
* add quest target (ie collect red, then green) and if collect something other than target bounce off)
* add quest hint for target (when you click on current target in bottom right corner auto steers you in direction
* make sure there's always an instance of the first target directly in front of the player at start 
* order of collection as per order of targets in list 
* add option to randomise target order
* add option to collect all objects X times to complete level. and make sure there enough objects created to enable that to happen
* add option to turn on and off pickup tumble.
* add aural hint for pickups (ie colour names)
* add sound affects

Bonus:
* improve artwork


#FUTURE (Product Backlog):
* add additional target/pickup types in additional scenes (colour, shapes, numbers, letters (upper/lower), words)

* visual indicator of the "Joystick" with variations for corner and overtop.
* although will start with visual & aural match, go to next scene (shapes pickups) on completion and then return to level 1 on completion of all levels (scenes) and then switch to just aural 

#MULTI PLAYER
* add ability to have multiple players either in team or as competitors
* bounce off each other

#Version 0.1
Additions to the base UFO Tutorial code as follows:

##Touchpad from Space Shooter Tutorial (Extending for mobile tutorial)
The touchpad controller capability was derived from the Unity tutorial - Space Shooter:
https://unity3d.com/learn/tutorials/topics/mobile-touch/mobile-development-converting-space-shooter-mobile?playlist=17147

However based on testing with kids I added a second movement area over the top of the player object as they both instinctively went to steer the ship by dragging from the center/player object. 

##Variation to allow "GitHubing"
I varied the configuration to allow the project to work with Git as per the Stackoverflow article here:
http://stackoverflow.com/questions/21573405/how-to-prepare-a-unity-project-for-git

Which makes the following changes:
1. Switch to Visible Meta Files in EDIT > Project Settings > Editor > Version Control Mode
2. Switch to Force Text in EDIT > Project Settings Editor > Asset Serialization Mode
3. Save 
4. Quit Unity and delete Library and Temp directory.
5. BUT Add .gitignore from GitHub rather than from one in stack.




