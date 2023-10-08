using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseChange : MonoBehaviour
{
    protected Boss bossComponent;

    private void Awake()
    {
        bossComponent = GetComponent<Boss>();
    }

    protected virtual bool ShouldChangePhase()
    {
        return false;
    }

    public void TriggerPhaseChange()
    {
        if (ShouldChangePhase() && bossComponent)
        {
            bossComponent.ChangePhase();
        }
    }
}