/*
 * using System.Collections;

using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraCtrl : MonoBehaviour {
 
    public GameObject player; //Ball object
    private Vector3 offset; //Vector from camera to ball
    private Vector3 prevPos; //Previous ball position, to calculate movement direction
    private Vector3 prevVec2; //Previous camera rotation vector, for smooth camera movement
 
    //Rotate vector, optimized for rotation about Y only
    private Vector3 rotY(Vector3 a, float theta) {
        Vector3 b = a;
        float ts = Mathf.Sin(theta);
        float tc = Mathf.Cos(theta);
        b.x = (tc*a.x)-(ts*a.z);
        b.z = (ts*a.x)+(tc*a.z);
        return b;
    }
 
    //Setup vectors
    void Start () {
        offset = transform.position - player.transform.position;
        prevPos = player.transform.position;
        prevVec2 = offset;
    }
 
    void Update () {
        Vector3 vel = player.transform.position-prevPos; //Get movement vector
        prevPos = player.transform.position; //Update previous ball position to current ball position
        Vector3 vec2 = rotY(offset,Mathf.Atan2(vel.x,-vel.z)); //Rotate camera to face movement
        if(vel.magnitude>Time.deltaTime*0.1f) { //Update previous camera rotation vector
            prevVec2 = vec2;
        } else { //If not moving, don't reset camera rotation to zero degrees
            vec2 = prevVec2;
        }
        transform.position = player.transform.position+vec2; //Set camera position and rotation
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(player.transform.position-transform.position),Time.deltaTime);
    }
}