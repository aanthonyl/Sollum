using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class arm_controller : MonoBehaviour
{
    public enum ArmState
    {
        Idle,
        Chase,
        Attack,
        Stun
    }

    public ArmState _currState = ArmState.Idle;
    public GameObject attackTrigger;
}