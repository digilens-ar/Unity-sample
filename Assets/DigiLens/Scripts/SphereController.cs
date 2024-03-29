using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Increase sphere scale
    /// </summary>
    public void Grow()
    {
        transform.localScale *= 1.5f;
    }

    /// <summary>
    /// Decrease sphere scale
    /// </summary>
    public void Shrink()
    {
        transform.localScale /= 1.5f;
    }

    /// <summary>
    /// Play audioclip
    /// </summary>
    public void Speak()
    {
        audioSource.Play();
    }

}
