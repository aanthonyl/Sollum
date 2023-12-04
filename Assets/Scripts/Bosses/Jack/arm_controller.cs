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

    #region Attack
    public GameObject armAttack;
    [SerializeField] private float attackStartUp;
    [SerializeField] private float attackDuration;
    [SerializeField] private float attackCoolDownTimer;
    private bool attackCoolDown = false;
    private bool initiatedAttack = false;
    #endregion

    #region Stun
    [SerializeField] private float stunTime;
    [SerializeField] private float stunCoolDownTimer;
    private bool stunCoolDown;
    private bool initiatedStun = false;
    #endregion



    public void Update()
    {
        switch(_currState)
        {
            case ArmState.Idle:
                break;
            case ArmState.Chase:
                // Lag slightly behind
                break;
            case ArmState.Attack:
                AttackRoutine();
                break;
            case ArmState.Stun:
                StunRoutine();
                break;
            default:
                break;
        }
    }

    // void OnTriggerEnter(Collider other)
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && _currState == ArmState.Idle && !attackCoolDown)
        {
            Debug.Log("Player Trigger?");
            _currState = ArmState.Attack;
            attackCoolDown = true;
        }
        if(other.CompareTag("Parasol") && _currState == ArmState.Idle && !stunCoolDown && attackCoolDown)
        {
            Debug.Log("Stun Contact");
            _currState = ArmState.Stun;
            stunCoolDown = true;
        }
    }

    private void AttackRoutine()
    {
        // Debug.Log("Start Attack");
        if(!initiatedAttack)
        {
            StartCoroutine(AttackLeadUp());
        }

    }

    private IEnumerator AttackLeadUp()
    {
        initiatedAttack = true;
        _currState = ArmState.Chase;
        yield return new WaitForSeconds(attackStartUp);
        // This will be responsible for the tracking
        StartCoroutine(Attack());
        Debug.Log("About to Attack");
        // initiatedAttack = false;
    }

    private IEnumerator Attack()
    {
        // Start Hitbox
        armAttack.SetActive(true);
        _currState = ArmState.Attack;
        yield return new WaitForSeconds(attackDuration);
        // End Hitbox
        Debug.Log("Finished Attack");
        AttackReset();
        StartCoroutine(AttackCoolDown());
    }

    private void AttackReset()
    {
        armAttack.SetActive(false);
        initiatedAttack = false;
        _currState = ArmState.Idle;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackCoolDownTimer);
        attackCoolDown = false;
    }

    private void StunRoutine()
    {
        // Play Stun Animation
        if(!initiatedStun)
        {
            initiatedStun = true;
            StartCoroutine(StunTimer());
        }
    }

    private IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(stunTime);
        _currState = ArmState.Idle;

        StartCoroutine(StunCoolDown());
    }

    private IEnumerator StunCoolDown()
    {
        yield return new WaitForSeconds(stunCoolDownTimer);
        stunCoolDown = false;
        initiatedStun = false;
    }
}