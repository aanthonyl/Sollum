using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CathBossTrigger : MonoBehaviour
{
    public GameObject boss;
    private void Start()
    {
        boss.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            boss.SetActive(true);
    }
}
