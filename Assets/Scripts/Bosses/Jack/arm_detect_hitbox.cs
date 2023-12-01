using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arm_detect_hitbox : MonoBehaviour
{
    public arm_controller armControl;
    public GameObject armAttack;
    void OnTriggerEnter()
    {
        // Debug.Log("Trigger Attack!");
        armControl._currState = arm_controller.ArmState.Attack;
        armAttack.SetActive(true);
    }

    void OnTriggerExit()
    {
        // Debug.Log("Trigger Attack!");
        armControl._currState = arm_controller.ArmState.Idle;
        armAttack.SetActive(false);
    }
}
