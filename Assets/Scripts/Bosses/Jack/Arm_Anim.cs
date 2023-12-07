using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Arm_Anim : MonoBehaviour
{
    private Animator anim;
    private string currState;
    public static Action idle;
    public static Action attackLeadUp;
    public static Action attackFollowThru;
    public static Action attackVulnerable;
    public static Action stun;

    private void OnEnable()
    {
        idle += Idle;
        attackLeadUp += AttackLead;
        attackFollowThru += AttackFollow;
        attackVulnerable += AttackVuln;
        stun += Stun;
    }

    private void OnDisable()
    {
        idle -= Idle;
        attackLeadUp -= AttackLead;
        attackFollowThru -= AttackFollow;
        attackVulnerable -= AttackVuln;
        stun -= Stun;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        ChangeState("armIdle");
    }

    private void ChangeState(string newState)
    {
        if (currState == newState) return;

        if(newState == "armIdle" && currState == "armStun" || newState == "armAttackVulnerable")
            StartCoroutine(CanaryDelay());

        anim.Play(newState);
        currState = newState;
    }

    public void Idle()
    {
        ChangeState("armIdle");
    }

    public void AttackLead()
    {
        ChangeState("armAttackLeadUp");
    }

    public void AttackFollow()
    {
        ChangeState("armAttackFollowThru");
    }

    public void AttackVuln()
    {
        ChangeState("armAttackVulnerable");
        // StartCoroutine(CanaryDelay());
    }

    private IEnumerator CanaryDelay()
    {
        yield return new WaitForSeconds(.2f);
        // Canary_Warning.warning();
    }

    public void Stun()
    {
        ChangeState("armStun");
    }
}
