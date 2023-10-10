using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // phase behaviors need to be defined
    public enum Phase { Phase1, Phase2 }
    public Phase currentPhase = Phase.Phase1;
    public float bossHealth = 100f;

    protected virtual void Start()
    {
        PhaseChange.onPhaseChanged += ChangePhase;
    }

    public void TakeDamage(float damage)
    {
        bossHealth -= damage;
        if (bossHealth <= 50f && currentPhase == Phase.Phase1)
        {
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
}
