using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 2.0f;
    public bool drawAttackRange = false;
    public float attackCooldown = 2.0f;

    private bool isPaused = false;

    private Transform player;
    private float nextAttackTime; // Time when the next attack can occur.
    private AudioSource audioSource;
    public AudioClip meleeAttackSound;

    public Animator anim;
    BoxCollider col;

    private void OnDrawGizmos()
    {
        if (drawAttackRange)
        {
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        anim = transform.GetChild(0).GetComponent<Animator>();
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
        //attack animation
        if (anim != null)
            anim.SetTrigger("Attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(10);
        }
    }
}
