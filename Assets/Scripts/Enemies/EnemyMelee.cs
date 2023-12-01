using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public int damage = 10;
    public float attackRange = 2.0f;
    public float staggerTime = 3.0f;
    public bool drawAttackRange = false;
    public float attackCooldown = 2.0f;
    public bool blocked = false;
    public bool parried = true;
    private bool isPaused = false;

    public Transform player;
    [SerializeField] Transform enemy;
    private float nextAttackTime; // Time when the next attack can occur.
    [SerializeField] AudioSource audioSource;
    public AudioClip meleeAttackSound;

    [SerializeField] Animator anim;
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
        if (audioSource != null)
            audioSource.clip = meleeAttackSound;

    }

    private void Update()
    {

        if (!isPaused)
        {
            float distanceToPlayer = Vector3.Distance(enemy.position, player.position);
            if  (!parried) {
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
        
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Parasol"))
        {
            if (parried) {
                StartCoroutine(Stagger());
            } else if (blocked) {
                //blocked attack
                blocked = false;
            }
        } else if (other.CompareTag("Player")) {
            if (!parried && !blocked) {
                player.GetComponent<PlayerHealth>().TakeDamage(10);
            }
        }
    }

    public IEnumerator Stagger() {
        yield return new WaitForSeconds(staggerTime);
        parried = false;
    }
}
