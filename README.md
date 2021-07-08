From Assets/Hybrid/Software:
- Run HybridLedgerSetup.exe. This will install the HybridLedger system tray app and the FFGLHybridUnityPlayer.dll in Resolume's effects folder

From Assets/Hybrid/ResoLink
- Apply ResoLinkSetup script anywhere in your Unity scene

From Assets/Hybrid/Param
- Create your interaction using the Param class. The different Param types can be created like you would a regular primitive variable, ie your script would use 
public ParamFloat myFloat;
	(instead of 
public float myFloat;)

You can then use either myFloat.GetValue() in Unity's Update call, or you can assign a UnityEvent method to be triggered via the inspector under the Advanced options.

Resolume will recognise ParamFloat as an unranged int, ParamRange as a ranged float, ParamEvent as an event button, ParamBool as a toggle and ParamColor as a color.

Build your scene as usual, just make sure the build folder is set to Desktop/Build. The scene needs to run once as a standalone build. After that the plugin will take care of starting and stopping instances. 
