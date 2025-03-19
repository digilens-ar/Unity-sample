using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
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

}
