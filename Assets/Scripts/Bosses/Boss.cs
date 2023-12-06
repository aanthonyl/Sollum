using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // phase behaviors need to be defined
    public enum Phase { Phase1, Phase2 }
    public  Phase currentPhase = Phase.Phase1;
    public  float bossHealth;
    public bool damageCoolDown = false;
    public int stunCount {get; set;} // Very Dangerous and stupid

    SpriteRenderer sprite;

    [SerializeField] private List<arm_controller> armsList;
    [SerializeField] private GameObject shotOrigin;

    [SerializeField] private float transformationTime;
    [SerializeField] private float hurtTime;
    private EnemyProjectile rangedAttack;
    private AudioSource hitSound;


    protected virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        PhaseChange.onPhaseChanged += ChangePhase;
        bossHealth = GetComponent<EnemyHealth>().enemyHealth;
        rangedAttack = GetComponent<EnemyProjectile>();
        Debug.Log("Boss Health: " + bossHealth);
        hitSound = GetComponent<AudioSource>();
    }

    // private void Update()
    // {
    //     if(currentPhase == Phase.Phase2)
    //     {
    //         rangedAttack.EnemyShoot();
    //     }
    // }

    // This is just so the Boss knows how much health it has left. [Hacky Solution]
    public  void TakeDamage(float damage)
    {
        if(stunCount == 2)
        {
            Debug.Log("Boss is Vulnerable");
            bossHealth -= damage;
            if(currentPhase == Phase.Phase2)
            {
                Boss_Anim.hurt();
                hitSound.Play();
                StartCoroutine(HurtTime());
            }
        }
        else
            Debug.Log("Boss is Not Vulnerable: " + stunCount);

        // StartCoroutine(FlashRed());

        if (bossHealth <= 0)
        {
            Debug.Log("Boss Death");
            // The termination will be handled by the EnemyHealth Script
            // EnemyDie();
        }
        if (bossHealth <= 50f && currentPhase == Phase.Phase1)
        {
            Debug.Log("Phase 2");
            PhaseChange.TriggerPhaseChange(Phase.Phase2);
            Boss_Anim.transformation();
            shotOrigin.SetActive(true);
            StartCoroutine(TransTime());
        }
    }


    private IEnumerator TransTime()
    {
        yield return new WaitForSeconds(transformationTime);
        Boss_Anim.phase2Idle();
    }

    private IEnumerator HurtTime()
    {
        yield return new WaitForSeconds(hurtTime);
        Boss_Anim.phase2Idle();
    }

    protected virtual void ChangePhase(Phase newPhase)
    {
        currentPhase = newPhase;
    }

    private void OnDestroy()
    {
        PhaseChange.onPhaseChanged -= ChangePhase;
    }

    // public static void EnemyDie()
    // {
    //     Debug.Log("ENEMY DIE");

    //     Destroy(this.gameObject);
    // }

    public void OnTriggerEnter(Collider other)
    {
        // ENTERED WHIP ATTACK TRIGGER
        if (other.gameObject.name == "WhipAttackZone")
        {
            TakeDamage(20);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PlayerProjectile"))
        {
            TakeDamage(20);
        }
    }

    private IEnumerator FlashRed()
    {
        damageCoolDown = true;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.8f);
        damageCoolDown = false;
    }
}
