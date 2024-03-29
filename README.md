# DigiLens Inc. Sample Project

To get started, navigate to our developer site: [Getting Started with Unity for ARGO](https://developer.digilens.com/hc/en-us/articles/20469922225435--Getting-Started-with-Unity-for-ARGO)

The webpage provides a detailed description of the steps required to build
Unity applications for ARGO devices.

## Overview: 

This Unity Sample app provides two sample scenes that showcase the different methods of UI interaction
availible. 

## Requirements:

Unity 2021.3+

Basic knowledge of Unity and C#

## Sample Scene 1: Scroll Wheel Interaction:

This scene showcases a dynamic cube that responds to scroll wheel input by changing color and rotation.
To explore the functionality, we have included a script called CubeController.

To gain insight into this feature, open the CubeController script located on the cube.
By examining this script, you will observe the implementation of the scroll wheel and how it is specifically
mapped to Unity's Input System.


Note: ARGO currently does not support the new input system.

## Sample Scene 2: Voice UI

This scene provides a demonstration of how to utilize the Voice UI feature.

The scene consists of the following elements:

    - A VoiceUIHandler prefab which contains a VoiceUITest script
    - A sphere object with a script called SphereController.

The VoiceUITest script is responsible for implementing the Voice UI feature. The script registers any specified voice commands and triggers
actions based on voice callback events. 

The SphereController script contains multiple functions which are invoked from the VoiceUITest script. Each function corresponds to a specific
voice command.

To modify the VoiceUITest script, simply change the reference script from SphereController to your own script.
Then, within the VoiceUITest script, modify the voice commands, the listeners, and implement your own Callback() functions. 
For a more detailed instructions, visit the following site: [Add Voice Commands to Unity Apps](https://developer.digilens.com/hc/en-us/articles/24214954568091-Add-Voice-Commands-to-Unity-Apps)

Note: this is just a demonstration of a scene showcasing how to implement a voice UI feature in your Unity project. The actual implementation
may vary based your project requirements.

## Additional Resources

Unity Documentation: [Input Manager](https://docs.unity3d.com/Manual/class-InputManager.html)

Voice UI Documentation: [DigiOS VoiceUI Overview](https://developer.digilens.com/hc/en-us/articles/19931447980827-DigiOS-VoiceUI)

## Troubleshooting

If you encounter any issues while running the Unity Sample app, please submit a ticket at:
[Submit support ticket](https://developer.digilens.com/hc/en-us/requests/new)
