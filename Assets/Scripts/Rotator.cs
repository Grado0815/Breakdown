using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // It needs to spin the pickup / Collectible spin
    // Update is called once per frame
    
    //translate moves the game object by its transform - Vector3 Variable
    //rotates its game object by its transform - Float variable with XYZ
    void Update()
    {
        transform.Rotate(new Vector3(15,30,45) * Time.deltaTime); //dynamicly change value of framelength
    }
}
