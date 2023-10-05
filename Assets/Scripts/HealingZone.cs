using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.name.Equals("Player") && Player.health < 100)
        {
            StartCoroutine("Heal");
        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name.Equals("Player)"))
        {
            StopCoroutine("Heal");
        }
    }

    IEnumerator Heal()
    {
        for(float currentHealth = Player.health; currentHealth <= 100; currentHealth += 0.05f)
        {
            Player.health = currentHealth;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Player.health = 100f;
    }


}
