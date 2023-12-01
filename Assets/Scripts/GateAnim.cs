using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAnim : MonoBehaviour
{
    public bool locked = false;
    public Animator myAnimR;
    public Animator myAnimL;



    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (!locked)
        {
            myAnimR.SetBool("isGate", true);
            myAnimL.SetBool("isGate", true);
        }
    }
}
