using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //Reference to the Player´s location
    public Transform target;

    //Offset from the Player
    private Vector3 offset = new Vector3(0f, 4f, -8f);

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null) {

            //Set the camera to the players position plus the offset
            transform.position = target.position + offset;
        }

    }
}
