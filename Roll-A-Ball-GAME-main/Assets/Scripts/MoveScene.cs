using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
    void Update()
    {
        transform.rotation *= Quaternion.Euler(Input.acceleration.y/6, -Input.acceleration.x/3, 0);
    }
}