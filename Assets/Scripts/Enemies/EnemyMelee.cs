using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMelee : MonoBehaviour
{
    public int damage = 10;
    private float currSpeed;
    public float attackRange = 2.0f;
    public float staggerTime = 3.0f;
    public bool drawAttackRange = false;
    public float attackCooldown = 2.0f;
    public bool blocked = false;
    public bool parried = false;
    public bool staggered = false;
    public bool touchingPlayer = false;
    public bool damageNextTurn = false;
    private bool isPaused = false;

    [SerializeField] float flashTime = 0.25f;
    private float flashTimeInterval;
    private float currentFlashTime;
    public Transform player;
    [SerializeField] UnityEngine.AI.NavMeshAgent nma;
    [SerializeField] SpriteRenderer sr;
    // [SerializeField] Transform enemy;
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
        nma = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = this.transform.GetChild(0).GetComponent<SpriteRenderer>();
        anim = this.transform.GetChild(0).GetComponent<Animator>();
        col = this.transform.GetComponent<BoxCollider>();
        flashTimeInterval = flashTime / 0.025f;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (audioSource != null)
            audioSource.clip = meleeAttackSound;
    }

    private void Update()
    {

        if (!isPaused)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (!staggered)
            {
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
        anim.SetBool("Staggered", staggered);
    }
    private void FixedUpdate()
    {
        if (damageNextTurn)
        {
            touchingPlayer = false;
            damageNextTurn = false;
            if (!blocked && !parried && !staggered)
            {
                Debug.Log("Doing damage");
                player.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
            blocked = false;
        }
        if (touchingPlayer)
        {
            damageNextTurn = true;
        }
        if (parried && !staggered)
        {
            Debug.Log("Starting stagger");
            StartCoroutine(Stagger());
        }
        // if (staggered)
        // {
        //     currentFlashTime++;
        //     if (sr.color == Color.red && currentFlashTime == flashTimeInterval)
        //     {
        //         sr.color = Color.white;
        //         currentFlashTime = 0;
        //     }
        //     else if (sr.color == Color.white && currentFlashTime == flashTimeInterval)
        //     {
        //         sr.color = Color.red;
        //         currentFlashTime = 0;
        //     }
        // }
        else
        {
            currentFlashTime = 0;
        }
    }

    public void AttackPlayer()
    {
        if (audioSource != null)
            audioSource.Play();
        //attack animation
        if (anim != null)
            anim.SetTrigger("Attack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            touchingPlayer = true;
        }
    }

    public IEnumerator Stagger()
    {
        col.enabled = false;
        parried = false;
        staggered = true;
        currSpeed = nma.speed;
        nma.speed = 0;
        yield return new WaitForSeconds(staggerTime);
        nma.speed = currSpeed;
        staggered = false;
    }

}
