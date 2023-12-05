using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    public GameObject objectToAppear;

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
        objectToAppear.SetActive(true);
    }
}
