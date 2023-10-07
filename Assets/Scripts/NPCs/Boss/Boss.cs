using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    public float health = 100;
    public bool isDead = false;
    protected int currentPhase = 1;

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0 && !isDead)
        {
            isDead = true;
            Die();
            return;
        }

        // Check if we need to trigger a phase change
        if (ShouldChangePhase())
        {
            ChangePhase();
        }
    }

    protected virtual bool ShouldChangePhase()
    {
        return false; 
    }

    protected virtual void ChangePhase()
    {
        currentPhase++;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
