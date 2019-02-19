# UnityConfBuildUtility

<img src="https://github.com/GimChuang/UnityConfBuildUtility/blob/master/readme_information/BuildUtility_window.png" width="400">

A simple utility helping building applications with different configurations for Unity.


Features
---
- Build applications with different configurations.
  It simply copies a json file which contains some data for games to the built application's StreamingAssets folder.
- Automatically generate a folder to place built files, with date and build count appended to the folder name. (e.g. MyAwesomeGame_2018-12-05_001)
- Write changelog for each build. They will be saved in Changelog.txt.


Exaple of Use Cases
---
As I work in an interactive installation design team, I am often required to have one game run on several computers. Since every computer may has different environment (such as directory path, resolution, webcam device name, etc.), I usually use Json file to edit configuration and game data. I used to copy my application to all computers, and then edit Json file on each computer.

However, I sometimes forget to edit the files, or mess up all the builds after I build many 
new versions due to some changes or quick fixes for the game, when I become flustered in a bustle. So I wrote this utility trying to make my workflow better. 


How to Use
---

![image](https://github.com/GimChuang/UnityConfBuildUtility/blob/master/readme_information/BuildUtility_folder.png)

1. The code requires "Builds" and "FilesToInclude" folder in your project folder to work (as the above picture shows). You have to prepare the Json files for each computer, and place them in the right folders.  (In the example, files for the first computer goes to FilesToInclude/MyAwesomeGame_PC01, files for the second computer goes to FilesToInclude/MyAwesomeGame_PC02. There is also a folder "FilesToInclude/General", which contains files that all the computers need) 
  
2. In Unity, click"BuildUtility" from the menu bar, and you'll see "Build_MyAwesomeGame_PC01" and "Build_MyAwesomeGame_PC02". Click one to build game for the corresponding computer.

3. A lovely window will pop up, showing date and build count of the day. Write down your changelog, click "Build" button, and Unity will start building the game. Nothing will happen if you don't type anything.

4. In the "Builds" folder, a new folder with date and build count appended to its name will be created. Your built game will be there. Right after building, Unity also copies files from "FilesToInclude/xxx" and "FilesToInclude/General" for the corresponding computer to the built StreamingAssets folder. Thus your game will have the right files for the specific computer.

After building for all computers. Copy the game to the computers and you can start the game 
 without editing the Json files first. Also, your changelog will be saved in the Changelog.txt file. 

This solution is quite simple and maybe clumsy, yet it works in my case.


Notes
---
- Game name, file paths and scene names are hardcoded in BuildUtility.cs.
- Only targeted Windows standalone
- Only tested on Windows 10, Unity 2018.2.8f1


To Do
---
- Complete example
- Make the codes more reuseable


Acknowledgement
---
This project is heavilily influenced by [Michael Carriere's ZapdotBuildTools](http://www.rebz.org/2013/10/under-the-hood-building-a-build-pipeline/).
