/*==============================================================================   
 * Copyright (c) 2023 Digilens Inc.   
 * All rights reserved.   
 * DigiLens Proprietary and Confidential. 
 * ==============================================================================*/

public class VoiceUIListener
{
    public string voice_command;
    public string GameObject;
    public string CallbackMethod;

    public VoiceUIListener(string voice_command, string GameObject, string CallbackMethod)
    {
        this.voice_command = voice_command;
        this.GameObject = GameObject;
        this.CallbackMethod = CallbackMethod;
    }
}
