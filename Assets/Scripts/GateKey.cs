using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKey : MonoBehaviour
{
    public GateAnim gates;
    public DialogueTrigger foundKey;

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foundKey.gameObject.SetActive(true);
            gates.locked = false;
            this.gameObject.SetActive(false);
        }
    }
}
