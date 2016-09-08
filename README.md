# unity-ufo

UFO is an expansion of the Unity Technologies 2D UFO tutorial as explained here:
https://unity3d.com/learn/tutorials/projects/2d-ufo-tutorial

Each version has been stored in a branch of the version name so that you can see the changes made historically and get an idea of how the app developed over time based on the backlog.

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


