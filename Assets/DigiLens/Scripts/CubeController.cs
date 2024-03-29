using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    Material cubeRenderer;
    public Color a, b;
    float colorVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = GetComponent<MeshRenderer>().material;
    }

    /// <summary>
    /// Based on the incoming input, performs a specific action
    /// Scroll wheel input is mapped to the keyboard keys: RightArrow, LeftArrow, and Return
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) //if scroll wheel is pressed, change cube material
        {
            ChangeColor();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))//if scroll wheel is scrolled forward, rotate cube forward
        {
            RotateCube(Vector3.forward);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))//if scroll wheel is scrolled backwards, rotate cube backwards
        {
            RotateCube(Vector3.back);
        }
    }

    /// <summary>
    /// Rotate cube along the designated axis
    /// </summary>
    /// <param name="direction"></param>
    void RotateCube(Vector3 direction)
    {
        transform.Rotate(direction, Space.Self);
    }

    /// <summary>
    /// Change cube color
    /// Lerp between colors a and b
    /// </summary>
    void ChangeColor()
    {
        Color temp;

        if (colorVal >= 1)
        {

            temp = a;
            a = b;
            b = temp;
            colorVal = 0;
        }

        cubeRenderer.color = Color.Lerp(a, b, colorVal);
        colorVal += 0.1f;
    }
}
