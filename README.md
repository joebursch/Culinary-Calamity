# Rordon Gamsay's Culinary Calamity

<p>Created by:
<li>Brennon Treadwell </li>
<li>Andrew Lawson </li>
<li>Joe Bursch </li>
<li>Jacob Sisson</li>
<li>Bradley Hays</li>
</p>

# Building / Running the Game

## During Development
You can run the current scene including all functionality by clicking the 'play' icon in the top center of the screen. This will play the current scene in the 'Game' window which is open by default. If you do not see the 'Game' window, select 'Window' from the top left menu, then 'General' then 'Game'.

## Building an Executable
### Build Settings
**Only required initially or if something in the build changes**
Select 'File' then 'Build Settings'. The top panel contains a list of all scenes (if you do not see a scene, open it and select 'add open scenes'). Check all scenes to be included in the build. The top scene is the entry point for the application, this should always be the start screen.
Choose platform in the bottom left panel (for desktop: "Windows, Mac, Linux"). On the right hand side edit the options for 'Target Platform' and 'Architecture'. Other settings can be left on default unless you have a reason to change them. 
If you need to change the build version, select 'Player Settings' in the very bottom left and update the 'Version' field.

### Building
From the build settings menu select 'build' or 'build and run'. Select the location to store the build (should be in the 'Builds' folder since that is git ignored).
The build process will initiate and complete or provide error messages if there are any issues.
You can play the game by navigating to the build folder and invoking the executable.
