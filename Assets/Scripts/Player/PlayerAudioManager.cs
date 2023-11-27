using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] audioClips;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(int index) 
    {
        source.clip = audioClips[index];
        source.Play();
    }

}
