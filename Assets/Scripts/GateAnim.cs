using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAnim : MonoBehaviour
{
    public Animator myAnimR;
    public Animator myAnimL;


   
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
      

        myAnimR.SetBool("isGate", true);
        myAnimL.SetBool("isGate", true);



    }
}
