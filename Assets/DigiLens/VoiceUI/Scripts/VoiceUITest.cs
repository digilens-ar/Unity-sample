/*==============================================================================   
 * Copyright (c) 2023 Digilens Inc.   
 * All rights reserved.   
 * DigiLens Proprietary and Confidential. 
 * ==============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceUITest : MonoBehaviour
{

    [Tooltip("Register Voice Commands")]
    public string[] voice_commands;


    VoiceUIInterface voiceUIInterface = new VoiceUIInterface();
    List<VoiceUIListener> voiceUIListeners = new List<VoiceUIListener>();
    bool IsVoiceUIInterfaceInitialized = false;
    bool IsVoiceCommandRegistered = false;


    void Awake()
    {
        for (int i = 0; i < voice_commands.Length; i++)
        {
            voiceUIListeners.Add(new VoiceUIListener(voice_commands[i], "VoiceUIHandler", "Callback"));
        }

        IsVoiceUIInterfaceInitialized = voiceUIInterface.Initialize();

        if (IsVoiceUIInterfaceInitialized)
        {
            IsVoiceCommandRegistered = voiceUIInterface.REGISTER_VOICE_COMMANDS(voiceUIListeners);
            if (IsVoiceCommandRegistered)
            {
                Debug.Log("Successfully registered voice commands with DigiOS Voice UI");
            }
            else
            {
                Debug.Log("Failed to register voice commands with DigiOS Voice UI");
            }
        }
    }


    public void Callback(string voice_command)
    {
        Debug.Log(voice_command);

        //TODO: Implement function here 

        AnimationController.instance.VoiceCallbackAction(voice_command);
    }
}
