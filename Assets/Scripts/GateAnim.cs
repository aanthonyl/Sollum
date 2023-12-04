using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAnim : MonoBehaviour
{
    public bool locked = false;
    private Animator myAnimR;
    private Animator myAnimL;
    private AudioSource creak;
    bool isPlayed = false;
    
    private void Start()
    {
        myAnimR = GameObject.Find("FenceGateR").GetComponent<Animator>();
        myAnimL = GameObject.Find("FenceGateL").GetComponent<Animator>();
        creak = GameObject.Find("Fence_Audio").GetComponent<AudioSource>();
        
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && (isPlayed == false))
        {
            if (!locked)
            {
                creak.Play();
                myAnimR.SetBool("isGate", true);
                myAnimL.SetBool("isGate", true);
                isPlayed = true;
            }
        }
        
    }
}
