using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a script used to make sprites parallel with the camera plane 
// used to emulate the way sprites will behave 
public class lookAtCamera : MonoBehaviour
{
    

    // Update is called once per frame
    void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
