using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Animator anim;
    // It needs to spin the pickup / Collectible spin
    // Update is called once per frame
    
    //translate moves the game object by its transform - Vector3 Variable
    //rotates its game object by its transform - Float variable with XYZ

    private void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        anim.Play("Door");
        //transform.position = transform.position + new Vector3(-2,-1,50) * Time.deltaTime; //dynamicly change value of framelength
    } 
}