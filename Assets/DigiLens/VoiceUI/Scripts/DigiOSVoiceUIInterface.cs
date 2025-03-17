using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DigiOSVoiceUIInterface : MonoBehaviour
{
    //Interface variables
    AndroidJavaObject voiceUI_Interface = null;
    AndroidJavaClass voiceUI_Interface_Constants = null;

    //Voice UI language models
    AndroidJavaObject voiceUI_Model = null;

    //Voice UI listeners
    AndroidJavaObject voiceUI_Listener_expand = null;
    AndroidJavaObject voiceUI_Listener_shrink = null;

    //Voice Commands
    string command1 = "expand";
    string command2 = "shrink";

    //Script whos functions will be referenced
    [Tooltip("Reference script")]
    public SphereController sphereController;

    //Text object
    public TMPro.TMP_Text text;

    /// <summary>
    /// Starting the voice UI service
    /// </summary>
    void Awake()
    {

        if (VoiceUI_Initialize())// Initializing voice UI interface. 
        {
            VoiceUI_Start(); //On success, start voice UI 
        }

    }

    /// <summary>
    /// Initialize and acquire the instance of the interface to the voice UI
    /// </summary>
    public bool VoiceUI_Initialize()
    {
        // Acquire the instance of the interface to the voice UI
        voiceUI_Interface = new AndroidJavaObject("com.digilens.digios_unity_plugin.VoiceUI_Interface");
        if (voiceUI_Interface == null)
        {
            Debug.LogError("DigiOS unity plugin is missing");
            return false;
        }
        // Acquire the handle to the Constants.class which will be required for the voice UI interface
        voiceUI_Interface_Constants = new AndroidJavaClass("com.digilens.digios_unity_plugin.utils.Constants");
        return true;
    }

    /// <summary>
    /// Start the voice UI service for your Unity application
    /// </summary>
    public bool VoiceUI_Start()
    {
        try
        {
            ConfigureVoiceUI();

            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            voiceUI_Interface.Call("start", activity);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }

        return true;
    }

    /// <summary>
    /// Setup the language models and register the voice commands.
    /// </summary>
    void ConfigureVoiceUI()
    {
        //create the models
        voiceUI_Model = new AndroidJavaObject("com.digilens.digios_unity_plugin.utils.VoiceUI_Model", "en");

        //create and register voice command listeners
        RegisterVoiceCommands();

        //Adding the VoiceUI Models to the VoiceUI_Interface
        voiceUI_Interface.Call("add_model", voiceUI_Model);

    }

    /// <summary>
    /// Creates the VoiceUI_Listeners' for each voice command that needs to be registered with the DigiOS VoiceUI.
    ///
    /// Two configuration types for each voice command:
    ///     1. Voice_Command_CONFIG_TYPE_FEEDBACK_ONLY - Used for regular voice commands
    ///     2. Voice_Command_CONFIG_TYPE_FEEDBACK_WITH_NUMBER_ONLY - Used for voice commands with variable number input
    ///
    /// Listener format: ("com.digilens.digios_unity_plugin.utils.VoiceUI_Listener", VOICECOMMAND, voiceUIconstants,
    /// NAME_OF_GAMEOBJECT, CALLBACKFUNCTION)
    /// **To modify listeners, add your custom voice command and specify which callback funtion should be called.
    /// If script is attached to a new gameobject, specify name of the gameobject. By default, the script uses the
    /// prefab name.
    ///
    /// Note:
    ///     1. Maximum character limit on voice command : 32
    ///     2. For voice commands with number input, the voice UI only accepts number input between 0 to 100.
    ///     3. The voiceUI service can only accept limited unique voice commands throughout the system.If there's
    ///         no available slots for the new commands, the voiceUI_interface will fail to your voice commands and
    ///         throws exception with message "Unable to register voice command".
    /// </summary>
    void RegisterVoiceCommands()
    {
        //Creating voice UI listeners
        voiceUI_Listener_expand = new AndroidJavaObject("com.digilens.digios_unity_plugin.utils.VoiceUI_Listener",
            command1, voiceUI_Interface_Constants.GetStatic<int>("Voice_Command_CONFIG_TYPE_FEEDBACK_ONLY"),
            gameObject.name, "VoiceUI_Callback1");
        voiceUI_Listener_shrink = new AndroidJavaObject("com.digilens.digios_unity_plugin.utils.VoiceUI_Listener",
            command2, voiceUI_Interface_Constants.GetStatic<int>("Voice_Command_CONFIG_TYPE_FEEDBACK_ONLY"),
            gameObject.name, "VoiceUI_Callback2");


        //Adding listeners to the appropriate language VoiceUI_Model. 
        voiceUI_Model.Call("addVoiceUI_Listener", voiceUI_Listener_expand);
        voiceUI_Model.Call("addVoiceUI_Listener", voiceUI_Listener_shrink);


    }

    /// <summary>
    /// Unregister all the voice commands and disconnects the interface from DigiOS Voice UI Service.
    /// </summary>
    public bool VoiceUI_Stop()
    {
        if (voiceUI_Interface != null) //If voice UI interface is initalized, disconnect service
        {
            voiceUI_Interface.Call("stop");

            //Disposing of the AndroidJavaObjects
            voiceUI_Listener_expand.Dispose();
            voiceUI_Listener_shrink.Dispose();

            //Disposing of all other elements
            voiceUI_Model.Dispose();
            voiceUI_Interface_Constants.Dispose();
            voiceUI_Interface.Dispose();

            return true;
        }

        return false;
    }

    /// <summary>
    /// The Voice UI interface will call a callback function once its respective voice command event has been received.
    /// Specify the action to be performed based on the incoming voice command
    /// </summary>
    /// <param name="voice_command">
    /// The voice command spoken
    /// </param>


    /// <summary>
    /// Callback funtion corresponding to the first voice command 
    /// </summary>
    public void VoiceUI_Callback1(string voice_command)
    {
        //TODO: Call functions here
        Debug.Log(voice_command);

        text.text = voice_command;
        sphereController.Grow();

    }

    /// <summary>
    /// Callback funtion corresponding to the second voice command
    /// </summary>
    public void VoiceUI_Callback2(string voice_command)
    {
        //TODO: Call functions here

        Debug.Log(voice_command);

        text.text = voice_command;
        sphereController.Shrink();

    }

}