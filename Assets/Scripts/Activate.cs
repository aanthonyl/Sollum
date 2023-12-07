using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    public GameObject objectToAppear;
    public Animator anim;
    public string animVariable;
    public bool triggerAnim = false;

    private void Start()
    {
        objectToAppear.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            Appear();
    }

    public void Appear()
    {
        if (triggerAnim && animVariable != null)
            anim.SetBool(animVariable, true);
        else
            objectToAppear.SetActive(true);
    }
}
