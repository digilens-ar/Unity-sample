/*==============================================================================   
 * Copyright (c) 2023 Digilens Inc.   
 * All rights reserved.   
 * DigiLens Proprietary and Confidential. 
 * ==============================================================================*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController instance;
    public GameObject ball;
    public GameObject player;
    Animator animator;
    float scrollInput;



    void Start()
    {
        instance = this;
        scrollInput = 0;
        animator = GetComponent<Animator>();
    }


    /// <summary>
    /// Switches animation clips based on user input 
    /// </summary>
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))//throw ball
        {
            JumpAnim();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GetScrollVal(true);
            ScrollAnimClip();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GetScrollVal(false);
            ScrollAnimClip();
        }

    }


    /// <summary>
    /// Stores scrollwheel input
    /// </summary>
    void GetScrollVal(bool increase)
    {
        if (increase)
        {
            scrollInput += Time.deltaTime;
        }
        else
        {
            scrollInput -= Time.deltaTime;
        }

        //Ensures input stays within 0-1 range 
        scrollInput = Mathf.Clamp01(scrollInput);
    }

    /// <summary>
    /// Traverses animation clip 
    /// </summary>
    void ScrollAnimClip()
    {
        if (scrollInput == 1)
        {
            animator.Play("Idle", 0, 0);
            scrollInput = 0; ///resets value 
        }
        else
        {
            animator.Play("Spin", 0, scrollInput);
        }

    }

    /// <summary>
    /// Starts the jump animation sequence
    /// </summary>
    void JumpAnim()
    {
        StartCoroutine(TossBall(2));
        animator.SetTrigger("Jump");
    }

    /// <summary>
    /// Starts the kick animation sequence
    /// </summary>
    void KickAnim()
    {
        //create new tossing where the target position is the bear's kicking leg
        StartCoroutine(TossBall(2));
        animator.SetTrigger("Kick");
    }

    /// <summary>
    /// Starts the spin animation sequence
    /// </summary>
    void SpinAnim()
    {
        animator.SetTrigger("Spin");
    }

    /// <summary>
    /// Moves ball in direction of bear
    /// </summary>
    IEnumerator TossBall(float totalTime)
    {
        Vector3 startPos = player.transform.position + new Vector3(0, -0.1f, 0);
        Vector3 endPos = transform.position;
        float elapsedTime = 0;

        ball.SetActive(true);

        while (elapsedTime < totalTime)
        {
            ball.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / totalTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ball.SetActive(false);
        ball.transform.position = startPos;
    }

    /// <summary>
    /// Enables action based on voice command
    /// </summary>
    public void VoiceCallbackAction(string voice_command)
    {

        switch (voice_command)
        {
            case "catch ball":
                JumpAnim();
                break;
            case "start spin":
                SpinAnim();
                break;
            default:
                break;
        }
    }

}
