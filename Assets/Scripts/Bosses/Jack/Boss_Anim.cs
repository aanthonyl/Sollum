using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Boss_Anim : MonoBehaviour
{
    private Animator anim;
    private string currState;
    public static Action phase1Idle;
    public static Action transformation;
    public static Action phase2Idle;
    public static Action hurt;

    private void OnEnable()
    {
        phase1Idle += P1Idle;
        transformation += Trans;
        phase2Idle += P2Idle;
        hurt += Hurt;
    }

    private void OnDisable()
    {
        phase1Idle -= P1Idle;
        transformation -= Trans;
        phase2Idle -= P2Idle;
        hurt -= Hurt;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        ChangeState("Phase1Idle");
    }

    private void ChangeState(string newState)
    {
        if(currState == newState) return;

        anim.Play(newState);
        currState = newState;
    }

    public void P1Idle()
    {
        ChangeState("Phase1Idle");
    }

    public void Trans()
    {
        ChangeState("Trans");
    }

    public void P2Idle()
    {
        ChangeState("Phase2Idle");
    }

    public void Hurt()
    {
        ChangeState("Hurt");
    }
}
