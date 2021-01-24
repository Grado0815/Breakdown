using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
using UnityEngine.Serialization;

public class OSCDummy : MonoBehaviour
{
    public string Address = "/example/1";
    //[FormerlySerializedAs("Address")] public string address = "/example/1";

    //[Header("OSC Settings")]
    public OSCReceiver Receiver;

    //Player controller

    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        Receiver.Bind(Address, ReceivedMessage);  
    }

    private void ReceivedMessage(OSCMessage message)
    {
        Vector2 touch;
        Debug.Log(message.ToVector2Double(out touch));

        if (message.ToVector2Double(out touch) == true)
        {
            Debug.Log(touch);
        }
        
        //Debug.LogFormat("Received: {0}", message); 
        
    }
    // Update is called once per frame
    //protected abstract void ReceivedMessage(OSCMessage message);
   
}
