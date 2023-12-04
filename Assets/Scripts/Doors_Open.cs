using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors_Open : MonoBehaviour
{
    private Animator anim;
    private AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.Find("Doors_Cath").GetComponent<Animator>();
        source = GameObject.Find("Door_Open_Source").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            source.Play();
            anim.SetBool("isDoors", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            source.Play();
            anim.SetBool("isDoorsClosed", true);            
            StartCoroutine(DelayDoors());
        }
    }
    IEnumerator DelayDoors()
    {
        yield return new WaitForSeconds(1);
        anim.SetBool("isDoorsClosed", false);
        anim.SetBool("isDoors", false);

    }
}
