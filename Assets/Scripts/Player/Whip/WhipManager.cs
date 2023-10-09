/*
    Script Added by Aurora Russell
	10/05/2023
	// WHIP MANAGER ACTIVATES WHIP ATTACK ZONE //
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipManager : MonoBehaviour
{
    public GameObject whipZone, dialogueUI, pauseUI;


    // ALLOWS DEVELOPER TO SELECT KEY FROM LIST
    [Header("Whip Attack Key")]
    public KeyCode WhipAttackKey = KeyCode.Mouse0;

    private bool whipCoolDown = false;

    void Update()
    {
        // ACTIVATE WHIP ATTACK ZONE
        if (whipZone.activeInHierarchy != true && dialogueUI.activeInHierarchy != true && pauseUI.activeInHierarchy != true && Input.GetKeyDown(WhipAttackKey) && whipCoolDown == false)
        {
            Debug.Log("START WHIP ATTACK");
            StartCoroutine(AttackDuration());
        }
    }
    
    private IEnumerator AttackDuration()
    {
        // PREVENTS OVER ATTACKING
        whipCoolDown = true;
        yield return new WaitForSeconds(1);
        whipZone.SetActive(true);
        yield return new WaitForSeconds(1);
        // ENDS ATTACK
        whipZone.SetActive(false);
        whipCoolDown = false;
    }
    
}


