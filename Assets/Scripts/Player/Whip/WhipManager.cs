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
    public GameObject whipZone;
    public PauseManager pauseManager;
    public DialogueManager dialogueManager;

    // ALLOWS DEVELOPER TO SELECT KEY FROM LIST
    private bool whipCoolDown = false;
    //[HideInInspector]
    //public bool dialogueOpen = false;
    [HideInInspector]
    public bool pauseOpen = false;

    private void Start()
    {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
    }

    void Update()
    {
        if (pauseManager.gamePaused == false && dialogueManager.dialogueActive == false)
        {
            // ACTIVATE WHIP ATTACK ZONE
            if (whipZone.activeInHierarchy != true && Input.GetButtonDown("Secondary") && whipCoolDown == false)//&& dialogueOpen == false && pauseOpen == false && Input.GetKeyDown(WhipAttackKey) && whipCoolDown == false)
            {
                Debug.Log("START WHIP ATTACK");
                StartCoroutine(AttackDuration());
            }
        }
    }
    
    private IEnumerator AttackDuration()
    {
        // PREVENTS OVER ATTACKING
        whipCoolDown = true;
        yield return new WaitForSeconds(0.5f);
        whipZone.SetActive(true);
        if(transform.parent.parent.GetComponent<playerMovement>().facingForward){
            whipZone.transform.localPosition = new Vector3(2.6f,0.31f,0);
        }else{     
            Debug.Log(transform.parent.parent.GetComponent<playerMovement>().facingForward);
            whipZone.transform.localPosition = new Vector3(-2.6f,0.31f,0);
        }
        yield return new WaitForSeconds(1);
        // ENDS ATTACK
        whipZone.SetActive(false);
        whipCoolDown = false;
    }
    
}


