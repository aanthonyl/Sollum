using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class Canary_Warning : MonoBehaviour
{
    public static AudioSource chirp;
    public static Action warning;
    // Start is called before the first frame update
    void Start()
    {
        chirp = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        warning += Chirp;
    }
    void OnDisable()
    {
        warning -= Chirp;
    }

    private void Chirp()
    {
        chirp.Play();
    }
}
