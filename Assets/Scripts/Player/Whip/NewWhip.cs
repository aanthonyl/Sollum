using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWhip : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float startup = 0.3f;
    [SerializeField] float activeTime = 0.3f;
    [SerializeField] float cooldown = 0.3f;
    [SerializeField] BlockParryController bpc;
    [SerializeField] GameObject rightHitbox;
    [SerializeField] GameObject leftHitbox;
    [SerializeField] GameObject upLeftHitbox;
    [SerializeField] GameObject upRightHitbox;
    [SerializeField] GameObject downLeftHitbox;
    [SerializeField] GameObject downRightHitbox;
    [SerializeField] PlayerAudioManager pam;


    playerMovement pm;
    MovementSettings ms;
    Animator anim;
    bool whipWindup = false;
    bool whipping = false;
    bool coolingDown = false;
    float currMovementSpeedMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        pm = transform.parent.parent.GetComponent<playerMovement>();
        ms = transform.parent.parent.GetComponent<MovementSettings>();
        anim = transform.parent.parent.GetComponent<Animator>();
        currMovementSpeedMultiplier = ms.GetMovementMultiplier();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pm.freezeMovement) {
            if (Input.GetButtonDown("Secondary")) {
                if (!bpc.isAttacking() && !bpc.isBlocking() && !bpc.isUnblocking() && !bpc.isCoolingDown() && !coolingDown) {
                    if (!whipping) {
                        StartCoroutine(Whip());
                    }               
                }
            }
        }
    }

    IEnumerator Whip() {
        whipWindup = true;
        whipping = true;
        yield return 0;
        whipWindup = false;
        pam.PlaySound(3);
        yield return new WaitForSeconds(startup);
        GameObject hitbox = ActivateHitbox();
        yield return new WaitForSeconds(activeTime);
        //i wanted to put deactivatehitbox() here but it seems like the active time is so short that sometimes it just immediately turns off without activating (i think)
        yield return new WaitForSeconds(cooldown);
        DeactivateHitbox(hitbox);
        whipping = false;
        coolingDown = true;
        yield return new WaitForSeconds(0.1f);
        
        coolingDown = false;
    }

    public bool isWhipping() {
        return whipping;
    }
    public bool isWindingUp() {
        return whipWindup;
    }
    public float GetDamage() {
        return damage;
    }

    GameObject ActivateHitbox() {
        if (anim.GetBool("Right")) {
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
}
