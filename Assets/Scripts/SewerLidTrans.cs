using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerLidTrans : MonoBehaviour
{
    private GameObject sewerL;
    // Start is called before the first frame update
    void Start()
    {
        sewerL = GameObject.Find("Sewer Lid UP");
        sewerL.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        sewerL.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        sewerL.SetActive(false);
    }
}
