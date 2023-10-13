using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 2.0f;
    public float attackCooldown = 2.0f;

    private bool isPaused = false;

    private Transform player;
    private float nextAttackTime; // Time when the next attack can occur.
    private AudioSource audioSource;
    public AudioClip meleeAttackSound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
            audioSource.clip = meleeAttackSound;

    }

    private void Update()
    {

        if (!isPaused)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= attackRange)
            {
                // Check if enough time has passed for the next attack.
                if (Time.time >= nextAttackTime)
                {

                    AttackPlayer();

                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
    }

    private void AttackPlayer()
    {
        if (audioSource != null)
            audioSource.Play();

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }


}
