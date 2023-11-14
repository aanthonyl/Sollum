using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Boss
{
    // the player/spot to charge towards
    public Transform chargeTarget;

    private BossCharge bossCharge;

    protected override void Start()
    {
        base.Start();
        bossCharge = GetComponent<BossCharge>();
    }

    protected override void ChangePhase(Phase newPhase)
    {
        base.ChangePhase(newPhase);

        if (newPhase == Phase.Phase2)
        {
            bossCharge.ChargeTowards(chargeTarget.position);
        }
    }

    // testing
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 40), "Test Charge"))
        {
            TakeDamage(51);  
        }

    }
}