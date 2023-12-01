using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // phase behaviors need to be defined
    public enum Phase { Phase1, Phase2 }
    public Phase currentPhase = Phase.Phase1;
    public float bossHealth = 100f;
    public bool damageCoolDown = false;

    SpriteRenderer sprite;

    protected virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        PhaseChange.onPhaseChanged += ChangePhase;
    }

    public void TakeDamage(float damage)
    {
        bossHealth -= damage;
        StartCoroutine(FlashRed());

        if (bossHealth <= 0)
        {
            EnemyDie();
        }
        if (bossHealth <= 50f && currentPhase == Phase.Phase1)
        {
            Debug.Log("Phase 2");
            PhaseChange.TriggerPhaseChange(Phase.Phase2);
        }
    }

    protected virtual void ChangePhase(Phase newPhase)
    {
        currentPhase = newPhase;
    }

    private void OnDestroy()
    {
        PhaseChange.onPhaseChanged -= ChangePhase;
    }

    public void EnemyDie()
    {
        Debug.Log("ENEMY DIE");

        Destroy(this.gameObject);
    }

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
