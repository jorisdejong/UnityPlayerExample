## How to get the example project up and running:

* From the Assets/Hybrid/Software folder, run HybridLedgerSetup.exe. This will install the HybridLedger system tray app and the FFGLHybridUnityPlayer.dll in Resolume's effects folder. HybridLedger is started whenever you use the plugin in Resolume and handles the communication between instances of the plugin and Unity executables. It runs in the background and doesn't need any setup. If you want, you can open it from the system tray to get feedback on what is happening under the hood.
* Build the Unity project, making sure the build folder is set to Desktop/Build. Let the scene run once as a fullscreen standalone build. This will ensure that the plugin knows what parameters to display. Once the build has run, you can close it.
* Launch the plugin from Resolume's sources. After a few seconds, the output from your scene will be visible.


## How to use the library with your own project:

* Copy the Hybrid and Plugin folders to the Assets folder of your own project. If you prefer, you can also install [KlakSpout](https://github.com/keijiro/KlakSpout) and [extOSC](https://github.com/Iam1337/extOSC) from source instead.
* From the Assets/Hybrid/ResoLink folder, apply the ResoLinkSetup.cs script anywhere in your Unity scene. This script does all the setup for running your project in windowless mode and communcating with the plugin.
* From the Assets/Hybrid/Param folder, create your interaction using the Param class. The different Param types can be created like you would a regular primitive variable, ie your script would use `public ParamFloat myFloat;` instead of `public float myFloat;`. You can then use either `myFloat.GetValue()` in Unity's Update call, or you can assign a UnityEvent method to be triggered via the inspector under the Advanced options.
* Resolume will recognise ParamFloat as an unranged int, ParamRange as a ranged float, ParamEvent as an event button, ParamBool as a toggle and ParamColor as a color.
* Build your project, again making sure the build folder is set to Desktop/Build. After running once, the plugin will then control the new project.


## What about running different Unity projects side by side?

This is entirely possible and the Ledger app is ready for it. However, it requires the plugin to be recompiled with a new unique identifier and the location of the new project. This is relatively simple to do, but does require access to the plugin source code. If this is something you are really interested in, get in touch with me.

## Why is this open source?

I was very excited about the possibilities of realtime performance in Resolume and I think that Unity has the quality we should expect in 2021. However, after a lot of testing and thinking, I've realised that Resolume is not the right environment for what I want to do. As a very basic example, let's say I have three clips of the included example prepared in Resolume, each with a different a different camera movement. After changing color on one of them, I cannot have that change reflected in the other two clips. Because of how parameters work in Resolume, switching to the other clips will always revert the color back to the value it had when the preset or clip was created. This lack of 'horizontal control' means I cannot perform the way I would like and it's a dealbreaker for me. So Resolume, although still great for video, is a dead end for me here and I'm going to focus my energy on making realtime performance work some other way. However, I completely understand this stuff is still lots of fun and opens up possiblities other than VJ'ing, so I'm sharing the progress I was able to make with the community.


## License

Shield: [![CC BY-NC-SA 4.0][cc-by-nc-sa-shield]][cc-by-nc-sa]

This work is licensed under a
[Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License][cc-by-nc-sa].

[![CC BY-NC-SA 4.0][cc-by-nc-sa-image]][cc-by-nc-sa]

[cc-by-nc-sa]: http://creativecommons.org/licenses/by-nc-sa/4.0/
[cc-by-nc-sa-image]: https://licensebuttons.net/l/by-nc-sa/4.0/88x31.png
[cc-by-nc-sa-shield]: https://img.shields.io/badge/License-CC%20BY--NC--SA%204.0-lightgrey.svg

This project packages [extOSC](https://github.com/Iam1337/extOSC), MIT licensed and [KlakSpout](https://github.com/keijiro/KlakSpout) Unlicense licensed
