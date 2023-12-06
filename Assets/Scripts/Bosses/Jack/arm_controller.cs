using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
// using UnityEngine.UI;

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
    [HideInInspector] public bool attackCoolDown = false;
    private bool initiatedAttack = false;
    [SerializeField] private GameObject hitDetect; // Hate doing this!!!
    [SerializeField] private GameObject hitZone; // Hate doing this!!!
    private AudioSource attackSound;
    #endregion

    #region Stun
    [SerializeField] private float stunTime;
    [SerializeField] private float stunCoolDownTimer;
    private bool stunCoolDown;
    private bool initiatedStun = false;
    [SerializeField] private Boss boss;
    #endregion

    #region Animation
    // private Animator anim;
    private Arm_Anim anim;
    #endregion



    public void Update()
    {
        switch (_currState)
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

    private void Start()
    {
        anim = GetComponent<Arm_Anim>();
        attackSound = GetComponent<AudioSource>();
    }

    // void OnTriggerEnter(Collider other)
    private void OnTriggerStay(Collider other)
    {
        // if(other.CompareTag("Player") && _currState == ArmState.Idle && !attackCoolDown)
        // {
        //     // Debug.Log("Player Trigger?");
        //     _currState = ArmState.Attack;
        //     attackCoolDown = true;
        // }
        if (other.CompareTag("Parasol") && _currState == ArmState.Idle && !stunCoolDown && attackCoolDown)
        {
            // Debug.Log("Stun Contact");
            _currState = ArmState.Stun;
            stunCoolDown = true;
        }
    }

    private void AttackRoutine()
    {
        // Debug.Log("Start Attack");
        if (!initiatedAttack)
        {
            StartCoroutine(AttackLeadUp());
        }

    }

    private IEnumerator AttackLeadUp()
    {
        initiatedAttack = true;
        _currState = ArmState.Chase;
        hitZone.SetActive(true);
        hitDetect.SetActive(false);
        // Arm_Anim.attackLeadUp();
        // Arm_Anim.AttackLead();
        anim.AttackLead();
        yield return new WaitForSeconds(attackStartUp);
        // This will be responsible for the tracking
        StartCoroutine(Attack());
        // Debug.Log("About to Attack");
        // initiatedAttack = false;
    }

    private IEnumerator Attack()
    {
        // Start Hitbox
        armAttack.SetActive(true);
        _currState = ArmState.Attack;
        // Arm_Anim.attackFollowThru();
        // Arm_Anim.AttackFollow();
        anim.AttackFollow();
        if (attackSound != null)
            attackSound.Play();
        yield return new WaitForSeconds(attackDuration);
        // End Hitbox
        // Debug.Log("Finished Attack");
        AttackReset();
        StartCoroutine(AttackCoolDown());
    }

    private void AttackReset()
    {
        armAttack.SetActive(false);
        hitZone.SetActive(false);
        hitDetect.SetActive(true);
        initiatedAttack = false;
        _currState = ArmState.Idle; // Reset to Idle by default
        // Arm_Anim.attackVulnerable();
        // Arm_Anim.AttackVuln();
        anim.AttackVuln();
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackCoolDownTimer);
        attackCoolDown = false;
        if (!initiatedStun)
            // Arm_Anim.idle();
            // Arm_Anim.Idle();
            anim.Idle(); // ensure is set to Idle
    }

    private void StunRoutine()
    {
        // Play Stun Animation
        if (!initiatedStun)
        {
            initiatedStun = true;
            // Arm_Anim.stun();
            // Arm_Anim.Stun();
            anim.Stun();
            boss.stunCount++;
            StartCoroutine(StunTimer());
        }
    }

    private IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(stunTime);
        StartCoroutine(StunCoolDown());
    }

    private IEnumerator StunCoolDown()
    {
        yield return new WaitForSeconds(stunCoolDownTimer);
        _currState = ArmState.Idle;
        // Arm_Anim.idle();
        // Arm_Anim.Idle();
        anim.Idle();
        stunCoolDown = false;
        initiatedStun = false;
        boss.stunCount--;
    }
}