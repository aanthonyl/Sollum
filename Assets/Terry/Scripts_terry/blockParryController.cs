using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// block & parry mechanics
public class BlockParryController : MonoBehaviour
{
    public float parryModeTime = 0.4f;

    [SerializeField] float blockingMovementSpeedMultiplier;
    float currMovementSpeedMultiplier;
    bool parrying = false;
    bool attacking = false;
    bool blocking = false;
    public float parryVelocity = 50.0f;
    public GameObject parryClass;
    Parry parry;
    PlayerKnockback knockback;
    Collider col;
    bool attackBuffer;
    bool blockBuffer;
    playerMovement pm;
    MovementSettings ms;
    [SerializeField] GameObject parryBlockClass;
    // bool meleeParrySuccess = false;
    // bool meleeBlockSuccess = false;

    public SpriteRenderer protoSprite;

    //Replace with developer friendly way to get this
    public PlayerHealth playerHealth;


    void Start()
    {
        parry = parryClass.GetComponent<Parry>();
        knockback = parryBlockClass.GetComponent<PlayerKnockback>();
        col = transform.GetChild(0).GetComponent<Collider>();
        col.gameObject.SetActive(false);
        pm = transform.parent.parent.GetComponent<playerMovement>();
        ms = transform.parent.parent.GetComponent<MovementSettings>();

    }
    void Update()
    {

        // detecting block and parry button press
        // the parry window is a fixed amount of time where
        // the player is locked in the parry state
        if (!pm.freezeMovement)
        {
            if (Input.GetButtonDown("Block"))
            {
                if (attacking) {
                    blockBuffer = true;
                } else if (!blocking) {
                    Block();
                }
            }

            if (Input.GetButtonUp("Block") && !parrying && blocking)
            {
                Unblock();
            }

            if (Input.GetButtonDown("Primary"))
            {
                if (parrying) {
                    attackBuffer = true;
                } else if (!attacking) {
                    StartCoroutine(Attack());
                }
                
            }

            if (!blocking && !attacking) protoSprite.color = Color.white;
        }

        

        // checks if player is in the block or parry state when hit
        // enemyAttack.attacking is a bool from the AttackPlayer class
        /*
        if ((blockPressed && enemyAttack.attacking) || (parrying && enemyAttack.attacking))
        {
            Debug.Log("block or parry sucessful");

            if (!parrying)
            {
                Debug.Log("Melee attack blocked");
                meleeBlockSuccess = true;
                // player blocks incoming damage?
                // player takes reduced damage 
            }
            else if (parrying)
            {
                Debug.Log("Melee attack parried");
                meleeParrySuccess = true;
                // player blocks incoming damage
                // enemy is stunned
            }
        }
        */
    }


    // projectile block & parry mechanic.
    // checks if parasol is in block or parry state
    // when projectile collides with it.
    // Destroys the projectile when blocked or parried.
    // the projectile is created again by the parasol and
    // shot toward the mouse.
    // player is knocked in the opposite direction they are 
    // facing on a sucessful block or parry. 
    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("EnemyProjectile"))
    //     {
    //         if (parrying)
    //         {
    //             Debug.Log("parried");
    //             Destroy(other.gameObject);
    //             parry.PlayerShoot();
    //             knockback.BlockParryKnockback();
    //         }
    //         else if (blockPressed)
    //         {
    //             Debug.Log("blocked");
    //             Destroy(other.gameObject);
    //             knockback.BlockParryKnockback();
    //         }
    //     }
    //     else if (other.CompareTag("Enemy"))
    //     {
    //         if (blockPressed)
    //         {
    //             other.gameObject.GetComponent<EnemyStateManager>().KnockedBack();
    //         }
    //     }
    //     else if (other.CompareTag("Break"))
    //     {
    //         Debug.Log("Detect");
    //         if (attacking)
    //         {
    //             other.gameObject.GetComponent<TempBreak>().Break();
    //         }
    //     }
    // }

    public bool isAttacking()
    {
        return attacking;
    }

    public bool isParrying()
    {
        return parrying;
    }

    public bool isBlocking()
    {
        return blocking;
    }

    public void ParryProj()
    {
        parry.PlayerShoot();
    }

    public void KnockPlayer()
    {
        knockback.BlockParryKnockback();
    }

    IEnumerator Parry()
    {
        protoSprite.color = Color.yellow;
        parrying = true;
        yield return new WaitForSeconds(parryModeTime);
        parrying = false;
        if (!Input.GetButton("Block")) Unblock();
        if (attackBuffer) {
            Unblock();
            StartCoroutine(Attack());
        }
        if (Input.GetButton("Block")) protoSprite.color = Color.red;
    }

    IEnumerator Attack()
    {
        if (attackBuffer) attackBuffer = false;
        protoSprite.color = Color.green;
        col.gameObject.SetActive(true);
        attacking = true;
        yield return new WaitForSeconds(1f/6f);
        attacking = false;
        col.gameObject.SetActive(false);
        protoSprite.color = Color.white;
        if (blockBuffer) Block();
    }

    void Block() {
        if (blockBuffer) blockBuffer = false;
        blocking = true;
        col.gameObject.SetActive(true);
        playerHealth.SetInvincibility(true);
        currMovementSpeedMultiplier = ms.GetMovementMultiplier();
        ms.SetMovementMultiplier(blockingMovementSpeedMultiplier);
        StartCoroutine(Parry());
    }

    void Unblock() {
        blocking = false;
        col.gameObject.SetActive(false);
        playerHealth.SetInvincibility(false);
        ms.SetMovementMultiplier(currMovementSpeedMultiplier);
        protoSprite.color = Color.white;
    }
}

