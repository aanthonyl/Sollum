using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// block & parry mechanics
public class BlockParryController : MonoBehaviour
{
    public float parryModeTime = 0.4f;
    public float slashKnockback;
    public float blockKnockback;
    [SerializeField] float blockingMovementSpeedMultiplier;
    [SerializeField] float parryMovementSpeedMultiplier;
    [SerializeField] float coolDownTime;
    [SerializeField] Animator anim;
    [SerializeField] GameObject upRightHitbox;
    [SerializeField] GameObject upLeftHitbox;
    [SerializeField] GameObject rightHitbox;
    [SerializeField] GameObject leftHitbox;
    [SerializeField] GameObject downRightHitbox;
    [SerializeField] GameObject downLeftHitbox;
    [SerializeField] GameObject blockRightHitbox;
    [SerializeField] GameObject blockLeftHitbox;
    [SerializeField] GameObject blockUpHitbox;
    [SerializeField] GameObject blockDownHitbox;

    [SerializeField] PlayerAudioManager pam;
    [SerializeField] NewWhip nw;
    float currMovementSpeedMultiplier;
    bool parryStartup = false;
    bool parrying = false;
    bool attacking = false;
    bool attackingFromBlock = false;
    bool coolingDown = false;
    bool canAttack = true;
    bool blocking = false;
    bool unblocking = false;
    bool attackBuffer = false;
    bool blockBuffer = false;
    int direction = 0;
    public float parryVelocity = 50.0f;
    public GameObject parryClass;
    playerMovement pm;
    MovementSettings ms;
    [SerializeField] GameObject parryBlockClass;
    public PlayerHealth playerHealth;

    void Start()
    {
        pm = transform.parent.GetComponent<playerMovement>();
        ms = transform.parent.GetComponent<MovementSettings>();
        currMovementSpeedMultiplier = ms.GetMovementMultiplier();
        direction = 0;
    }
    void Update()
    {

        // detecting block and parry button press
        // the parry window is a fixed amount of time where
        // the player is locked in the parry state
        if (!pm.freezeMovement)
        {
            if (Input.GetButton("Block") && !nw.isWhipping())
            {
                if ((coolingDown || !canAttack) && !attackBuffer && !blockBuffer && !blocking) {
                    blockBuffer = true;
                } else if (!blocking && !attacking && !coolingDown && !unblocking && !blockBuffer) {
                    Block();
                }
            }

            if (Input.GetButtonUp("Block") && !parrying && blocking)
            {
                StartCoroutine(Unblock());
            }

            if (Input.GetButtonDown("Primary") && !nw.isWhipping())
            {
                if ((unblocking || coolingDown || !canAttack) && !blockBuffer && !attackBuffer && !attacking) {
                    attackBuffer = true;
                } else if (blocking && !parrying) {
                    BlockCancel();
                } else if (!attacking && !parrying && !coolingDown && canAttack && !attackBuffer) {
                    StartCoroutine(Attack());
                }
            }

            BlockingHitboxes();

            
        }

        //Debugging Purposes

        if (Input.GetKeyDown(KeyCode.Comma)) {
            Time.timeScale -= 0.2f;
            // Debug.Log("Decreasing Timescale");
        }
        if (Input.GetKeyDown(KeyCode.Period)) {
            Time.timeScale += 0.2f;
            // Debug.Log("Increasing Timescale");
        }
        

        // checks if player is in the block or parry state when hit
        // enemyAttack.attacking is a bool from the AttackPlayer class
        /*
        if ((blockPressed && enemyAttack.attacking) || (parrying && enemyAttack.attacking))
        {
            // Debug.Log("block or parry sucessful");

            if (!parrying)
            {
                // Debug.Log("Melee attack blocked");
                meleeBlockSuccess = true;
                // player blocks incoming damage?
                // player takes reduced damage 
            }
            else if (parrying)
            {
                // Debug.Log("Melee attack parried");
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
    //             // Debug.Log("parried");
    //             Destroy(other.gameObject);
    //             parry.PlayerShoot();
    //             knockback.BlockParryKnockback();
    //         }
    //         else if (blockPressed)
    //         {
    //             // Debug.Log("blocked");
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
    //         // Debug.Log("Detect");
    //         if (attacking)
    //         {
    //             other.gameObject.GetComponent<TempBreak>().Break();
    //         }
    //     }
    // }

    public int getDirection()
    {
        return direction;
    }
    public bool isAttacking()
    {
        return attacking;
    }
    public bool isAttackingFromBlock()
    {
        return attackingFromBlock;
    }
    public bool isCoolingDown()
    {
        return coolingDown;
    }

    public bool isParrying()
    {
        return parrying;
    }
    public bool parryStartingUp()
    {
        return parryStartup;
    }

    public bool isBlocking()
    {
        return blocking;
    }

    public bool isUnblocking()
    {
        return unblocking;
    }

    public void ParryProj()
    {
        //parry.PlayerShoot();
    }

    public void KnockPlayer()
    {
        //knockback.BlockParryKnockback();
    }

    IEnumerator Parry()
    {
        // Debug.Log("Parry() Called.");
        parryStartup = true;
        parrying = true;
        pam.PlaySound(0);
        yield return new WaitForSeconds(0.01f);
        parryStartup = false;
        yield return new WaitForSeconds(parryModeTime - 0.01f);
        parrying = false;
        ms.SetMovementMultiplier(blockingMovementSpeedMultiplier);
        if (attackBuffer) BlockCancel();
        else if (!Input.GetButton("Block")) StartCoroutine(Unblock());
    }

    IEnumerator Attack(int direction = 0)
    {
        // Debug.Log("Attack() Called.");
        attacking = true;
        if (attackBuffer) attackBuffer = false;
        GameObject hitbox = null;
        if (direction != 0) {
            attackingFromBlock = true;
            hitbox = ActivateHitbox(true, direction);
        }
        else {
            attackingFromBlock = false;
            hitbox = ActivateHitbox();
        }
        pam.PlaySound(2);
        yield return new WaitForSeconds(1f/6f * 4f/3f);
        DeactivateHitbox(hitbox);
        coolingDown = true;
        attacking = false;
        attackingFromBlock = false;
        //cooling down
        yield return new WaitForSeconds(coolDownTime);
        coolingDown = false;
        canAttack = false;
        if (blockBuffer && !blocking)  {
            Block();
            yield return new WaitForSeconds(0.1f);
            canAttack = true;
            attackBuffer = false;
        } else if (attackBuffer) {
            yield return new WaitForSeconds(0.1f);
            canAttack = true;
            blockBuffer = false;
            StartCoroutine(Attack());
        } else {
            yield return new WaitForSeconds(0.1f);
            blockBuffer = false;
            attackBuffer = false;
            canAttack = true;
        }
    }

    void Block() {
        // Debug.Log("Block() Called.");
        if (blockBuffer) blockBuffer = false;
        blocking = true;
        playerHealth.SetInvincibility(true);
        currMovementSpeedMultiplier = ms.GetMovementMultiplier();
        ms.SetMovementMultiplier(parryMovementSpeedMultiplier);
        StartCoroutine(Parry());
    }

    IEnumerator Unblock() {
        // Debug.Log("Unblock() Called.");
        blocking = false;
        unblocking = true;
        BlockingHitboxes(true);
        ms.SetMovementMultiplier(parryMovementSpeedMultiplier);
        pam.PlaySound(1);
        yield return new WaitForSeconds(16f/60f);
        unblocking = false;
        playerHealth.SetInvincibility(false);
        ms.SetMovementMultiplier(currMovementSpeedMultiplier);
        if (attackBuffer) StartCoroutine(Attack());
    }

    void BlockCancel() {
        // Debug.Log("BlockCancel() Called.");
        blocking = false;
        GameObject blockbox = BlockingHitboxes(true);
        direction = 0;
        if (blockbox == blockUpHitbox) direction = 1;
        else if (blockbox == blockRightHitbox) direction = 2;
        else if (blockbox == blockDownHitbox) direction = 3;
        else if (blockbox == blockLeftHitbox) direction = 4;
        playerHealth.SetInvincibility(false);
        ms.SetMovementMultiplier(currMovementSpeedMultiplier);
        StartCoroutine(Attack(direction));
    }

    GameObject ActivateHitbox(bool fromBlockCancel = false, int direction = 0) {
        if (fromBlockCancel) {
            if (direction == 1) {
                if (anim.GetBool("FacingForward")) {
                    upRightHitbox.SetActive(true);
                    return upRightHitbox;
                } else {
                    upLeftHitbox.SetActive(true);
                    return upLeftHitbox;
                }
            } else if (direction == 2) {
                rightHitbox.SetActive(true);
                return rightHitbox;
            } else if (direction == 3) {
                if (anim.GetBool("FacingForward")) {
                    downRightHitbox.SetActive(true);
                    return downRightHitbox;
                } else {
                    downLeftHitbox.SetActive(true);
                    return downLeftHitbox;
                }
            } else if (direction == 4) {
                leftHitbox.SetActive(true);
                return leftHitbox;
            } else return null;
        } else if (anim.GetBool("Right")) {
            if (anim.GetBool("Up")) {
                upRightHitbox.SetActive(true);
                return upRightHitbox;
            } else if (anim.GetBool("Down")) {
                 downRightHitbox.SetActive(true);
                 return downRightHitbox;
            } else {
                rightHitbox.SetActive(true);
                return rightHitbox;
            }
        } else if (anim.GetBool("Left")) {
            if (anim.GetBool("Up")) {
                upLeftHitbox.SetActive(true);
                return upLeftHitbox;
            } else if (anim.GetBool("Down")) {
                downLeftHitbox.SetActive(true);
                return downLeftHitbox;
            } else {
                leftHitbox.SetActive(true);
                return leftHitbox;
            }
        } else if (anim.GetBool("Up")) {
            if (anim.GetBool("FacingForward")) {
                upRightHitbox.SetActive(true);
                return upRightHitbox;
            } else {
                upLeftHitbox.SetActive(true);
                return upLeftHitbox;
            }
        } else if (anim.GetBool("Down")) {
            if (anim.GetBool("FacingForward")) {
                downRightHitbox.SetActive(true);
                return downRightHitbox;
            } else {
                downLeftHitbox.SetActive(true);
                return downLeftHitbox;
            }
        } else if (anim.GetBool("FacingForward")) {
            rightHitbox.SetActive(true);
            return rightHitbox;
        } else {
            leftHitbox.SetActive(true);
            return leftHitbox;
        }
    }

    void DeactivateHitbox(GameObject hitbox) {
        if (hitbox != null) {
            hitbox.SetActive(false);
        }
    }

    GameObject BlockingHitboxes(bool deactivate = false) {
        if (blocking) {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("BlockRight") || anim.GetCurrentAnimatorStateInfo(0).IsName("OpenParasolRight")) {
                if (!blockRightHitbox.activeSelf) {
                    blockRightHitbox.SetActive(true);
                    blockLeftHitbox.SetActive(false);
                    blockUpHitbox.SetActive(false);
                    blockDownHitbox.SetActive(false);
                    return blockRightHitbox;
                }
            } else if (anim.GetCurrentAnimatorStateInfo(0).IsName("BlockLeft") || anim.GetCurrentAnimatorStateInfo(0).IsName("OpenParasolLeft")) {
                if (!blockLeftHitbox.activeSelf) {
                    blockRightHitbox.SetActive(false);
                    blockLeftHitbox.SetActive(true);
                    blockUpHitbox.SetActive(false);
                    blockDownHitbox.SetActive(false);
                    return blockLeftHitbox;
                }
                
            } else if (anim.GetCurrentAnimatorStateInfo(0).IsName("BlockUp") || anim.GetCurrentAnimatorStateInfo(0).IsName("OpenParasolUp")) {
                if (!blockUpHitbox.activeSelf) {
                    blockRightHitbox.SetActive(false);
                    blockLeftHitbox.SetActive(false);
                    blockUpHitbox.SetActive(true);
                    blockDownHitbox.SetActive(false);
                    return blockUpHitbox;
                }
            } else if (anim.GetCurrentAnimatorStateInfo(0).IsName("BlockDown") || anim.GetCurrentAnimatorStateInfo(0).IsName("OpenParasolDown")) {
                if (!blockDownHitbox.activeSelf) {
                    blockRightHitbox.SetActive(false);
                    blockLeftHitbox.SetActive(false);
                    blockUpHitbox.SetActive(false);
                    blockDownHitbox.SetActive(true);
                    return blockDownHitbox;
                }
            }
        } else if (deactivate) {
            GameObject hitbox = null;
            if (blockRightHitbox.activeSelf) hitbox = blockRightHitbox;
            if (blockLeftHitbox.activeSelf) hitbox = blockLeftHitbox;
            if (blockUpHitbox.activeSelf) hitbox = blockUpHitbox;
            if (blockDownHitbox.activeSelf) hitbox = blockDownHitbox;
            blockRightHitbox.SetActive(false);
            blockLeftHitbox.SetActive(false);
            blockUpHitbox.SetActive(false);
            blockDownHitbox.SetActive(false);
            return hitbox;
        }
        return null;
    }

    
}

