using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{

    public float healAmount = 10.0f;
    private bool isHealing = false;

    public AudioClip healingSound; 
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = healingSound;
        audioSource.loop = true; //loop sound while healing
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            isHealing = true;
            audioSource.Play();
        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player)"))
        {
            isHealing = false;
            audioSource.Stop();
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (isHealing && col.CompareTag("Player"))
        {
            // Heal the player over time.
            PlayerHealth playerHealth = col.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount * Time.deltaTime);
            }
        }
    }
}
