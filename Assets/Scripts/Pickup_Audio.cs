using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Audio : MonoBehaviour
{
    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        source.Play();
    }
}
