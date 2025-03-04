using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    //[SerializeField] private BillboardType billboardType;
    // Start is called before the first frame update
    [SerializeField] bool freezeXAxis = true;

    //enum BillboardType { LookAtCamera, CameraForward };

    void LateUpdate()
    {
        if (freezeXAxis)
        {
            transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = Camera.main.transform.rotation;
        }

    }

}
