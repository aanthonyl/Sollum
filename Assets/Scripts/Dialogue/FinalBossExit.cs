using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossExit : MonoBehaviour
{
    public Animator strickland;
    bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!activated && other.CompareTag("Player"))
            strickland.SetTrigger("Transform");
    }
}
