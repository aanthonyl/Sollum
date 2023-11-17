using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// block & parry mechanics
public class BlockParryController : MonoBehaviour
{
    public float parryModeTime = 0.4f;
    bool parryWindow = false;
    bool attacking = false;
    bool blockPressed = false;
    public float parryVelocity = 50.0f;
    public GameObject parryClass;
    public Animator anim;
    Parry parry;
    PlayerKnockback knockback;
    Collider col;
    [SerializeField] GameObject parryBlockClass;
    // bool meleeParrySuccess = false;
    // bool meleeBlockSuccess = false;

    public SpriteRenderer protoSprite;

    //Replace with developer friendly way to get this
    public PlayerHealth playerHealth;


    void Start()
    {
        protoSprite = transform.GetChild(2).GetComponent<SpriteRenderer>();
        parry = parryClass.GetComponent<Parry>();
        knockback = parryBlockClass.GetComponent<PlayerKnockback>();
        col = transform.GetChild(0).GetComponent<Collider>();
        col.gameObject.SetActive(false);
    }
    void Update()
    {

        // detecting block and parry button press
        // the parry window is a fixed amount of time where
        // the player is locked in the parry state
        if (Input.GetMouseButtonDown(1))
        {
            blockPressed = true;
            col.gameObject.SetActive(true);
            playerHealth.SetInvincibility(true);
            StartCoroutine(ParryWindow());
        }

        if (Input.GetMouseButtonUp(1))
        {
            blockPressed = false;
            col.gameObject.SetActive(false);
            playerHealth.SetInvincibility(false);
            protoSprite.color = Color.white;
        }

        if (Input.GetMouseButtonDown(0) && !attacking)
        {
            anim.SetTrigger("ParasolAttack");
            StartCoroutine(AttackWindow());
        }

        if (!blockPressed && !attacking)
            protoSprite.color = Color.white;

        // checks if player is in the block or parry state when hit
        // enemyAttack.attacking is a bool from the AttackPlayer class
        /*
        if ((blockPressed && enemyAttack.attacking) || (parryWindow && enemyAttack.attacking))
        {
            Debug.Log("block or parry sucessful");

            if (!parryWindow)
            {
                Debug.Log("Melee attack blocked");
                meleeBlockSuccess = true;
                // player blocks incoming damage?
                // player takes reduced damage 
            }
            else if (parryWindow)
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
    //         if (parryWindow)
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
        return parryWindow;
    }

    public bool isBlocking()
    {
        return blockPressed;
    }

    public void ParryProj()
    {
        parry.PlayerShoot();
    }

    public void KnockPlayer()
    {
        knockback.BlockParryKnockback();
    }

    IEnumerator ParryWindow()
    {
        protoSprite.color = Color.yellow;
        parryWindow = true;
        yield return new WaitForSeconds(parryModeTime);
        parryWindow = false;
        protoSprite.color = Color.red;
    }

    IEnumerator AttackWindow()
    {
        protoSprite.color = Color.green;
        col.gameObject.SetActive(true);
        attacking = true;
        yield return new WaitForSeconds(1);
        attacking = false;
        col.gameObject.SetActive(false);
        protoSprite.color = Color.white;
    }
}

