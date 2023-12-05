using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public int attackDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AttackPlayer(other);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            AttackPlayer(other.collider);
    }

    private void AttackPlayer(Collider player)
    {
        // Debug.Log("ENEMY ATTACK PLAYER");
        //audioSource.Play();

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}
