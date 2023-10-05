using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{

    GameObject Player;

    [SerializeField] float health;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player") && health < 100)
        {
            StartCoroutine("Heal");
        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player)"))
        {
            StopCoroutine("Heal");
        }
    }

    IEnumerator Heal()
    {
        for(float currentHealth = health; currentHealth <= 100; currentHealth += 0.05f)
        {
            health = currentHealth;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        health = 100f;
    }


}
