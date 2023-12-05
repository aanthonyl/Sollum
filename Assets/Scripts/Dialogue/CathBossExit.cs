using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CathBossExit : MonoBehaviour
{
    bool disappeared = false;
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if (!disappeared && other.CompareTag("Player"))
        {
            anim.SetTrigger("Disappear");
            disappeared = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!disappeared && other.CompareTag("Player"))
        {
            anim.SetTrigger("Disappear");
            disappeared = true;

        }
    }
}
