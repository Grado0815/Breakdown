using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Debug = UnityEngine.Debug;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    //public Animator anim;
    public float speed = 1f;
    public TextMeshProUGUI countText;
    
    public GameObject winTextObject;
    
    
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
        winTextObject.SetActive(false);
        
        
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
        if (count >= 13)
        {
            winTextObject.SetActive(true);
        }
    }

    
    
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
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

            if (count >= 3)
            {
               
                //anim.Play("Door");
                //Animation.Play("Door");
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            // works only in Unity Development Environment
            Debug.Log( message: "GAME OVER!");
            //yield return new WaitForSeconds(5);
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#endif
            // works only with built Apps
            // Application.Quit();
        }
        
    
    }
    
    
}
