﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Debug = UnityEngine.Debug;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    //public Animator anim;
    public float speed = 1f;
    public TextMeshProUGUI countText;
    
    //public GameObject winTextObject;
    public GameObject completeLevelUI;
    
    
    //Rigidbody variable privat (isolated)
    private Rigidbody rb = null;
    
    //counter of natural numbers
    private int count;
    
    private float movementX;
    private float movementY;
    


    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        //everytime the count variable is updated
        SetCountText();
      
        
        //only display, if the player completed the game
       // winTextObject.SetActive(false);
        
        
    }

    void OnMove(InputValue movementValue)
    {
        //Function input parameters
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
        
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + " / 13"  ;
        
        //if counter number higher than 12 - show Text "You Win!"
        if (count >= 3) // CHANGE THIS TO 13 AGAIN
        {
           // winTextObject.SetActive(true);
            StartCoroutine(WaitAfterWinning());
            // After 5 seconds: Start next level or go back to main menu
            //This screen should stay until something is selected, but it vanishes after a few secoonds

        }
    }

    
    
    void FixedUpdate()
    {
        
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
      

        if (rb.position.y < -1f)
        {
            Debug.Log( message: "GAME OVER!"); // SAME AS FOR COLLIDING WITH ENEMIES
            StartCoroutine(RestartAfterALittleBit());
           

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        //set Pickup Element to false if collision happen
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            //if Pickup Element collected set count +1
            count = count + 1;
            
            SetCountText();

            /*if (count >= 3)
            {
               
              anim.Play("Door");
              Animation.Play("Door");
            }*/
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log( message: "GAME OVER!"); // SAME AS FOR COLLIDING WITH ENEMIES
            Restart();
            
            
            //yield return new WaitForSeconds(5);
/*#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif*/
            // works only with built Apps
            // Application.Quit();
        }
        
    
    }
    
    /*public IEnumerator waitALittleBit()
    {
        yield return new WaitForSeconds(5); //CHANGE THIS LATER TO NEXT LEVEL OR QUIT SCREEN
#if UNITY_EDITOR //the following code is only included in the unity editor
        UnityEditor.EditorApplication.ExitPlaymode();//exits the playmode
#endif
    }*/
    
    private IEnumerator RestartAfterALittleBit()
    {
        yield return new WaitForSeconds(2); //CHANGE THIS LATER TO NEXT LEVEL OR QUIT SCREEN
        Restart();
    }
    private IEnumerator WaitAfterWinning()
    {
        yield return new WaitForSeconds(1); 
        CompleteLevel();
        
    }
    
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    private void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
        //This screen should stay until something is selected, but it vanishes after a few secoonds
    }
}
