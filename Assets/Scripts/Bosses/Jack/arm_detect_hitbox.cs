using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm_detect_hitbox : MonoBehaviour
{
    public arm_controller armControl;
    public GameObject armAttack;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && armControl._currState != arm_controller.ArmState.Stun)
        {
            // Debug.Log("Start Trigger Attack!");
            armControl._currState = arm_controller.ArmState.Attack;
            // armAttack.SetActive(true);
        }
    }

    // void OnTriggerExit(Collider other)
    // {
    //     if(other.CompareTag("Player"))
    //     {
    //         // Debug.Log("End Trigger Attack!");
    //         armControl._currState = arm_controller.ArmState.Idle;
    //         armAttack.SetActive(false);
    //     }
    // }
}
