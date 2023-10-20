/*
    Script Added by Aurora Russell
	10/19/2023
	// ENEMY ATTACK SYSTEM //
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    // SCRIPT TO BE PLACED ON ENEMY GAME OBJECT TAGGED "Enemy"

    public enum EnemyType
    {
        GruntEnemy,
        ThrowEnemy,
        ShootEnemy,
    }
    public EnemyType enemyType = new EnemyType();

    [Header("Attack Stats")]
    [Tooltip("Updates Automatically")]
    public int attackDamage; // = 10;
    [Tooltip("Updates Automatically")]
    public float attackRange; // = 2.0f;
    [Tooltip("Updates Automatically")]
    public float attackCooldown = 2.0f; // Can be adjusted per enemy type if needed
    [Tooltip("Updates Automatically")]
    public float nextAttackTime; // Time when the next attack can occur.

    private Transform player;
    public bool freezeAttack = false;

    //private AudioSource audioSource;
    //[HideInInspector]
    //public AudioClip meleeAttackSound, throwAttackSound, shootAttackSound;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //audioSource = GetComponent<AudioSource>();

        if (enemyType == EnemyType.GruntEnemy)
        {
            attackDamage = 20;
            attackRange = 2.0f;

            //audioSource.clip = meleeAttackSound;
        }
        else if (enemyType == EnemyType.ThrowEnemy)
        {
            attackDamage = 40;
            attackRange = 4.0f;

            //audioSource.clip = throwAttackSound;
        }
        else if (enemyType == EnemyType.ShootEnemy)
        {
            attackDamage = 60;
            attackRange = 4.0f;

            //audioSource.clip = shootAttackSound;
        }
    }

    private void Update()
    {
        if (!freezeAttack)
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
        Debug.Log("ENEMY ATTACK PLAYER");
        //audioSource.Play();

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}