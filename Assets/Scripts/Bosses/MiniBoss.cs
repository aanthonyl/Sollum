using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : Boss
{
    // the player/spot to charge towards
    public Transform chargeTarget;

    private BossCharge bossCharge;

    bool isCharging = false;

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

    private void Update()
    {
        if (!isCharging)
            StartCoroutine(charge());
    }

    IEnumerator charge()
    {
        isCharging = true;
        bossCharge.ChargeTowards(chargeTarget.position);
        yield return new WaitForSeconds(3f);
        isCharging = false;
    }

    // testing
    // void OnGUI()
    // {
    //     if (GUI.Button(new Rect(10, 10, 150, 40), "Test Charge"))
    //     {
    //         // TakeDamage(51); 
    //         bossCharge.ChargeTowards(chargeTarget.position);
    //     }

    // }
}