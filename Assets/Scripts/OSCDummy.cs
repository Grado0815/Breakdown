using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
using UnityEngine.Serialization;

//using UnityEngine.Serialization;

public class OSCDummy : MonoBehaviour
{
    [FormerlySerializedAs("Address")] public string address = "/*/touch0";

    [FormerlySerializedAs("Receiver")] [Header("OSC Settings")]
    public OSCReceiver receiver;
    //Player controller

    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        receiver.Bind(address, ReceivedMessage);  
    }

    private void ReceivedMessage(OSCMessage message)
    {
        //Debug.Log(message.ToVector2Double(out touch));

        if (message.ToVector2Double(out var touch) == true)
        {
            playerController.OnMoveVector2(touch);
            Debug.Log(touch);
        }
        
        Debug.LogFormat("Received: {0}", message); 
        
    }
    // Update is called once per frame
    //protected abstract void ReceivedMessage(OSCMessage message);
   
}
