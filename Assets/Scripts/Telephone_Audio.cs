using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telephone_Audio : MonoBehaviour
{
    private AudioSource source;
    bool isPlayed = false;
   
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" &&(isPlayed == false))
        {
            source.Play();
            isPlayed = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            source.Stop();
        }
    }
}
