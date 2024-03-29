using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{


    /// <summary>
    /// Disposes application when paused.
    /// Temporary fix to OpenXR rendering issue
    /// </summary>
    void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Application.Quit();
        }
    }
}
