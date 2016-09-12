# unity-ufo

UFO is an expansion of the Unity Technologies 2D UFO tutorial as explained here:
https://unity3d.com/learn/tutorials/projects/2d-ufo-tutorial

Each version has been stored in a branch of the version name so that you can see the changes made historically and get an idea of how the app developed over time based on the backlog.

#Big Picture / Roadmap
We want to create a space learning game where kids (including Autistic ones) can be assessed for their base skills - ie assess if they can recognize/match colours, shapes, numbers, letters and even words and then if they can match by sound.

The game will have different levels of increasing difficulty with the inital goal being to match what they need to "collect" by sight and then by aural request and then by reading alone.

It needs to be fun and pretty.

#Version 0.2 [ Current Dev Branch ]

#DONE so far:
* Replace artwork with temporary art as not sure on the rights issues there. ie background, player and asteroid (Pickup)
* Change so randomly generates pickups of the quantity as set by a parameter
* changed the map size
* changed the edge of the map so visually you see the background all the way to the edge of the screen and then bounce off an invisible "edge"
* Game now restarts automatically when you have collected a proportion of the total pickups

##Done as of 12 Sept (Sprint Start: 8 Sept)
* different backgrounds with the ability (via Unity config) to have a fixed one or select at random
* change way player works so has constant velocity in direction pointed(doesn't slow down). (ie mass of 0 and drag of 0?) and doesn't spin. 
* change player controller so is touch screen where you want player to steer to
* add acceleration based on distance of touch from center or up arrow key.
* ability to have different player icons (via Unity confid).  Default is to select one at random.  Includes addition of artwork.
* changed pickup images for initial colour category targets
* added an explosions for pickups (grabbed one from Unity Asset Store)
* introduce full range of pickup objects (different colour) and distribute randomly in equal proportion
* add quest target (ie collect red, then green) and if attempt to collect something other than target bounce off it.  
* add aural hint for pickups/target (ie colour names) of current quest on explode and on create.
* add visual target indicator too.
* add word target indicator to test reading
* add boing on hit wrong target
* add option to collect all objects X times to complete level and make sure there enough objects created to enable that to happen
* improve artwork - additional player images
* scoring based on "damage" from pickup bounces and collects count modified by hint use (50% if use of hint)
* added shield with visual indicator
* added life and end of game based on all lifes used
* changed way interacts with edge of map so teleports..not sure this is good idea. Also have toggle to go back to bounce off edge

#BACKLOG - Wish List for v0.2:
* Complete = Release v0.2

#FUTURE (Product Backlog):
* add big collision barriers/objects to layout to increase complexity - use as map edge to introduce player to (=special type of Target)
* add ability to have different target sets (levels) 
* add additional target/pickup types in additional scenes (colour, shapes, numbers, letters (upper/lower), words, food, furniture, animals, people) + ability for teacher to add / remove from "game"
 - will require refactor of target prefab and work out what to do about audio for each target
* add option to randomise target order 
* edge of map objects
* Game GUI (Pause, Settings, Login/Player Select, Save Progress, results)
* visual indicator of game over
* add own background music
* although will start with visual & aural match, go to next scene (shapes pickups) on completion and then return to level 1 on completion of all levels (scenes) and then switch to just aural to confirm learning
* Solar system based progression for theme for each scene (start at earth or sun?) or other space picture background progression from the NASA archives. 
* Zoom in / out (by pinch & slider?)
* Home base that grows as you collect (for freeplay version has respawn when add to base) - possiblity of drag back to base config (need to work out how to do towing)

#MULTI PLAYER
* add ability to have multiple players either in team or as competitors
* bounce off each other?
* tails? particularly of colourfull M n M's would look good.
* base build mode (colours/types equate to different resource types)

#TRACKING
* use unity services to students, score, progress, metrics

#DISTRIBUTING & UPDATING 
* how does Unity support patching? We will publish through Teacher Virus initially as a WebGL
* How does publishing through Teacher virus effect future ownership disputes etc.

---

#History - 

##Version 0.1
Additions to the base UFO Tutorial code as follows:

###Touchpad from Space Shooter Tutorial (Extending for mobile tutorial)
The touchpad controller capability was derived from the Unity tutorial - Space Shooter:
https://unity3d.com/learn/tutorials/topics/mobile-touch/mobile-development-converting-space-shooter-mobile?playlist=17147

However based on testing with kids I added a second movement area over the top of the player object as they both instinctively went to steer the ship by dragging from the center/player object. 

Possibly remove the duplication of control and just leave the instinctive central control.

###Variation to allow "GitHubing"
I varied the configuration to allow the project to work with Git as per the Stackoverflow article here:
http://stackoverflow.com/questions/21573405/how-to-prepare-a-unity-project-for-git

Which makes the following changes:
1. Switch to Visible Meta Files in EDIT > Project Settings > Editor > Version Control Mode
2. Switch to Force Text in EDIT > Project Settings Editor > Asset Serialization Mode
3. Save 
4. Quit Unity and delete Library and Temp directory.
5. BUT Add .gitignore from GitHub rather than from one in stack.




