using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DigiOSVoiceUIInterface : MonoBehaviour
{

    AndroidJavaObject voiceUI_Interface = null;
    AndroidJavaClass voiceUI_Interface_Constants = null;

    AndroidJavaObject voiceUI_Model_EN = null;
    AndroidJavaObject voiceUI_Listener_expand_EN = null;
    AndroidJavaObject voiceUI_Listener_shrink_EN = null;
    AndroidJavaObject voiceUI_Listener_hello_EN = null;

    AndroidJavaObject voiceUI_Model_ES = null;
    AndroidJavaObject voiceUI_Listener_expandir_ES = null;
    AndroidJavaObject voiceUI_Listener_encoger_ES = null;
    AndroidJavaObject voiceUI_Listener_hola_ES = null;

    //Voice Commands
    string command1EN = "expand";
    string command2EN = "shrink";
    string command3EN = "hello";
    string command4ES = "expandir";
    string command5ES = "encoger";
    string command6ES = "hola";

    //Add script whos functions will be referenced
    [Tooltip("Reference script")]
    public SphereController sphereController;


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
    /// Setup the language models and corresponding voice commands.
    ///
    ///  Supported languages :
    ///  1. US-ENGLISH - "en"
    ///  2. MEX-SPANISH - "es"
    ///
    /// Note:
    /// 1. You can only add one voice model for a given language in the VoiceUI_Interface.Adding a new VoiceUI_Model
    ///    to the VoiceUI_Interface for the same language will replace the previous added voice model for that LANGUAGE_CODE.
    /// </summary>
    void ConfigureVoiceUI()
    {
        //create the models
        voiceUI_Model_EN = new AndroidJavaObject("com.digilens.digios_unity_plugin.utils.VoiceUI_Model", "en");
        voiceUI_Model_ES = new AndroidJavaObject("com.digilens.digios_unity_plugin.utils.VoiceUI_Model", "es");

        //create and register voice command listeners
        RegisterVoiceCommands();

        //Adding the VoiceUI Models to the VoiceUI_Interface
        voiceUI_Interface.Call("add_model", voiceUI_Model_EN);
        voiceUI_Interface.Call("add_model", voiceUI_Model_ES);
    }

    /// <summary>
    /// Creates the VoiceUI_Listeners' for each voice command that needs to be registered with the DigiOS VoiceUI.
    ///
    /// Two configuration types for each voice command:
    ///     1. Voice_Command_CONFIG_TYPE_FEEDBACK_ONLY - Used for regular voice commands
    ///     2. Voice_Command_CONFIG_TYPE_FEEDBACK_WITH_NUMBER_ONLY - Used for voice commands with variable number input
    ///
    /// Listener format: ("com.digilens.digios_unity_plugin.utils.VoiceUI_Listener", VOICECOMMAND, voiceUIconstants, NAME_OF_GAMEOBJECT, CALLBACKFUNCTION)
    /// **To modify listeners, add your custom voice command and specify which callback funtion should be called. If script is attached to a new
    /// gameobject, specify name of the gameobject. By default, the script uses the prefab name.
    ///
    /// Note:
    ///     1. Maximum character limit on voice command : 32
    ///     2. For voice commands with number input, the voice UI only accepts number input between 0 to 100.
    ///     3. The voiceUI service can only accept limited unique voice commands throughout the system.If there's no available slots for the new commands, the voiceUI_interface will fail to your voice commands and throws exception with message "Unable to register voice command".
    /// </summary>
    void RegisterVoiceCommands()
    {
        //Creating english voice UI listeners
        voiceUI_Listener_expand_EN = new AndroidJavaObject("com.digilens.digios_unity_plugin.utils.VoiceUI_Listener",
            command1EN, voiceUI_Interface_Constants.GetStatic<int>("Voice_Command_CONFIG_TYPE_FEEDBACK_ONLY"), "VoiceUI_Handler", "VoiceUI_Callback1");
        voiceUI_Listener_shrink_EN = new AndroidJavaObject("com.digilens.digios_unity_plugin.utils.VoiceUI_Listener",
            command2EN, voiceUI_Interface_Constants.GetStatic<int>("Voice_Command_CONFIG_TYPE_FEEDBACK_ONLY"), "VoiceUI_Handler", "VoiceUI_Callback2");
        voiceUI_Listener_hello_EN = new AndroidJavaObject("com.digilens.digios_unity_plugin.utils.VoiceUI_Listener",
           command3EN, voiceUI_Interface_Constants.GetStatic<int>("Voice_Command_CONFIG_TYPE_FEEDBACK_ONLY"), "VoiceUI_Handler", "VoiceUI_Callback3");

        //creating spanish voice UI listeners
        voiceUI_Listener_expandir_ES = new AndroidJavaObject("com.digilens.digios_unity_plugin.utils.VoiceUI_Listener",
            command4ES, voiceUI_Interface_Constants.GetStatic<int>("Voice_Command_CONFIG_TYPE_FEEDBACK_ONLY"), "VoiceUI_Handler", "VoiceUI_Callback1");
        voiceUI_Listener_encoger_ES = new AndroidJavaObject("com.digilens.digios_unity_plugin.utils.VoiceUI_Listener",
            command5ES, voiceUI_Interface_Constants.GetStatic<int>("Voice_Command_CONFIG_TYPE_FEEDBACK_ONLY"), "VoiceUI_Handler", "VoiceUI_Callback2");
        voiceUI_Listener_hola_ES = new AndroidJavaObject("com.digilens.digios_unity_plugin.utils.VoiceUI_Listener",
            command6ES, voiceUI_Interface_Constants.GetStatic<int>("Voice_Command_CONFIG_TYPE_FEEDBACK_ONLY"), "VoiceUI_Handler", "VoiceUI_Callback3");


        //Adding listeners to the appropriate language VoiceUI_Model. 
        voiceUI_Model_EN.Call("addVoiceUI_Listener", voiceUI_Listener_expand_EN);
        voiceUI_Model_EN.Call("addVoiceUI_Listener", voiceUI_Listener_shrink_EN);
        voiceUI_Model_EN.Call("addVoiceUI_Listener", voiceUI_Listener_hello_EN);
        voiceUI_Model_ES.Call("addVoiceUI_Listener", voiceUI_Listener_expandir_ES);
        voiceUI_Model_ES.Call("addVoiceUI_Listener", voiceUI_Listener_encoger_ES);
        voiceUI_Model_ES.Call("addVoiceUI_Listener", voiceUI_Listener_hola_ES);
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
            voiceUI_Listener_expand_EN.Dispose();
            voiceUI_Listener_shrink_EN.Dispose();
            voiceUI_Listener_hello_EN.Dispose();
            voiceUI_Listener_expandir_ES.Dispose();
            voiceUI_Listener_encoger_ES.Dispose();
            voiceUI_Listener_hola_ES.Dispose();
            voiceUI_Model_ES.Dispose();
            voiceUI_Model_EN.Dispose();
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
        Debug.Log(voice_command);

        //TODO: Call functions here
        sphereController.Grow();

    }

    /// <summary>
    /// Callback funtion corresponding to the second voice command
    /// </summary>
    public void VoiceUI_Callback2(string voice_command)
    {
        Debug.Log(voice_command);

        //TODO: Call functions here
        sphereController.Shrink();

    }

    /// <summary>
    /// Callback funtion corresponding to the third voice command 
    /// </summary>
    public void VoiceUI_Callback3(string voice_command)
    {
        Debug.Log(voice_command);

        //TODO: Call functions here
        sphereController.Speak();

    }
}