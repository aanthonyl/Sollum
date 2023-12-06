using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone_AudioOff : MonoBehaviour
{
    private AudioSource source;
    void Start()
    {
        source = GameObject.Find("Phone_Booth_Audio").GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            source.Stop();
        }
    }
}
