# Rordon Gamsay's Culinary Calamity

Created by:

* Brennon Treadwell
* Andrew Lawson
* Joe Bursch
* Jacob Sisson
* Bradley Hays

## Building / Running the Game

### During Development

Note: Unity will automatically download the packages needed when the project is opened for the first time. See the [developer guide](https://github.com/UNCW-CSC-450/csc450-sp24-project-team_1/wiki/Developer-Guide) for information on text editors/IDEs
* Clone the Repository to your local machine
* [Install Unity]
* Open Unity hub
* Navigate to the projects panel on the left hand menu
* Near the top right corner, select the gray down arrow next to the add button and click 'add project from disk'
* Navigate to the clone repository, select the 'CulinaryCalamity' folder
* Once the project has finished loading, open the project
* In the Unity editor 'Projects' window (default is bottom left) open the 'Scenes' folder
* From the scenes folder, double click on the 'StartScreen' scene to open it
* Click the play button (triangle icon) to start the game

### Building an Executable

#### Build Settings

**Only required initially or if something in the build changes**
Select 'File' then 'Build Settings'. The top panel contains a list of all scenes (if you do not see a scene, open it and select 'add open scenes'). Check all scenes to be included in the build. The top scene is the entry point for the application, this should always be the start screen.
Choose platform in the bottom left panel (for desktop: "Windows, Mac, Linux"). On the right hand side edit the options for 'Target Platform' and 'Architecture'. Other settings can be left on default unless you have a reason to change them.
If you need to change the build version, select 'Player Settings' in the very bottom left and update the 'Version' field.

#### Building

From the build settings menu select 'build' or 'build and run'. Select the location to store the build (should be in the 'Builds' folder since that is git ignored).
The build process will initiate and complete or provide error messages if there are any issues.
You can play the game by navigating to the build folder and invoking the executable.

### Playing Without Building

Players may also choose to play the game without building from the source code.
The game is hosted [here](https://bradleyhays.itch.io/rordon-gamsays-culinary-calamity). The current password to access this page is ```csc550iscool```.
After-download instructions are provided on the download page.

## Black Box Testing Instructions

* If setting up the project for the first time, follow the 'Building/DuringDevelopment' steps to install unity and open the project
* In the top bar menu, select window -> General -> test runner
* In the window that opens, switch to play mode
* Click run all to run all tests
* Click on a specific test suite or test case to run only that suite/case
