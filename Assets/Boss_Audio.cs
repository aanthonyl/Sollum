using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Audio : MonoBehaviour
{

    private AudioSource source1;
    private AudioSource source2;

    // Start is called before the first frame update
    void Start()
    {
        source1 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        source2 = GameObject.Find("MayorBoss").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            source2.Play();
            StartCoroutine(DelayAudio());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            source2.Pause();
            source1.Play();
        }
    }

    IEnumerator DelayAudio()
    {
        yield return new WaitForSeconds(1.3f);
        source1.Pause();

    }

}
