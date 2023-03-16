/*==============================================================================   
 * Copyright (c) 2023 Digilens Inc.   
 * All rights reserved.   
 * DigiLens Proprietary and Confidential. 
 * ==============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceUIInterface
{
    AndroidJavaObject _Activity = null;

    public bool Initialize()
    {
        _Activity = new AndroidJavaObject("com.digilens.digios_unity_plugin.DigiOSUnityPlayerActivity");

        if (_Activity == null)
        {
            Debug.LogError("No DigiOS unity plugin is missing");
            return false;
        }
        else
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
            return _Activity.Call<bool>("init", context);
        }
    }

    public bool REGISTER_VOICE_COMMANDS(List<VoiceUIListener> voiceUIListeners)
    {
        string[] voice_commands = new string[voiceUIListeners.Count];
        string[] GameObjects = new string[voiceUIListeners.Count];
        string[] CallbackMethods = new string[voiceUIListeners.Count];

        string voice_command;
        string GameObject;
        string CallbackMethod;
        for (int i = 0; i < voiceUIListeners.Count; i++)
        {
            voice_command = voiceUIListeners[i].voice_command;
            GameObject = voiceUIListeners[i].GameObject;
            CallbackMethod = voiceUIListeners[i].CallbackMethod;
            if (voice_command == null)
            {
                return false;
            }
            else if (GameObject == null)
            {
                return false;
            }
            else if (CallbackMethod == null)
            {
                return false;
            }
            else
            {
                voice_commands[i] = voice_command;
                GameObjects[i] = GameObject;
                CallbackMethods[i] = CallbackMethod;
            }
        }

        if (_Activity == null)
        {
            Debug.LogError("No DigiOS unity plugin is missing");
            return false;
        }
        else
        {
            return _Activity.Call<bool>("REGISTER_VOICE_COMMANDS", voice_commands, GameObjects, CallbackMethods); ;
        }
    }

    public bool UNREGISTER_ALL_VOICE_COMMAND()
    {
        if (_Activity == null)
        {
            Debug.LogError("No DigiOS unity plugin is missing");
            return false;
        }
        else
        {
            return _Activity.Call<bool>("UNREGISTER_ALL_VOICE_COMMAND");
        }
    }
}
