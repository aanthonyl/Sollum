/*
    Script Added by Aurora Russell
	09/30/2023
	// MANAGES DIALOGUE SYSTEM //
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class DialogueManager : MonoBehaviour
{
    // SCRIPT TO BE PLACED ON EMPTY GAME OBJECT CALLED "DialogueManager"

    [Header("UI Elements")]
    [Tooltip("Dialogue UI")]
    public GameObject DialogueUI;
    [Tooltip("Body of Text")]
    public Text dialogueBody;
    [Tooltip("Name of Speaker")]
    public Text speakerName;
    [Tooltip("Image of Speaker")]
    public Image speakerSprite;
    [Tooltip("Continue Button")]
    public GameObject continueImage;

    [Tooltip("Type Speed")]
    public float typeSpeed = 0.05f;


    private Queue<string> inputStream = new Queue<string>(); // stores dialogue

    // SYSTEMS TO BE FROZEN WHILE DIALOGUE IS OPEN
    [HideInInspector]
    public playerMovement playerMovement;
    [HideInInspector]
    public PauseManager pauseManager;

    // ALLOWS DEVELOPER TO SELECT KEY FROM LIST
    [Header("Continue Key")]
    public KeyCode DialogueKey = KeyCode.E;

    [HideInInspector]
    public DialogueTrigger currentTrigger;

    // BOOLS
    public bool isTyping = false;
    private bool cancelTyping = false;
    [HideInInspector]
    public bool dialogueActive = false;
    public bool inDialogueZone = false;


    [Header("Speaker Library")]
    [Tooltip("Invisible/Placeholder sprite for when no one is talking")]
    public Sprite invisSprite;
    public SpeakerLibrary speakerSprites;
    [HideInInspector]
    public List<string> speakerSpriteNames;

    private void Start()
    {
        // ACCESS PLAYER MOVEMENT, WHIP ATTACK, PAUSE MENU, ENEMY ATTACK FOR FREEZE MOVEMENT
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();
        pauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();

        // GET SPEAKERS FROM LIBRARY & ADD TO LIST
        foreach (SpeakerLibrary.SpriteInfo info in speakerSprites.speakerSpriteList)
        {
            speakerSpriteNames.Add(info.name);
        }
        speakerSprite.sprite = invisSprite;
    }

    private void Update()
    {
        if (inDialogueZone && Input.GetKeyDown(DialogueKey))
        {
            AdvanceDialogue();
        }
    }

    // HAULT PLAYER MOVEMENT, WHIP ATTACK, ENEMY ATTACKS, PAUSE MENU
    private void FreezePlayer()
    {
        playerMovement.freezeMovement = true;
        //playerMovement.enabled = false;
        dialogueActive = true;

        pauseManager.dialogueOpen = true;
    }

    // RESTORE PLAYER MOVEMENT, WHIP ATTACK, ENEMY ATTACKS
    private void UnFreezePlayer()
    {
        playerMovement.freezeMovement = false;
        //playerMovement.enabled = true;
        dialogueActive = false;

        pauseManager.dialogueOpen = false;
    }

    // STARTS DIALOGUE
    public void StartDialogue(Queue<string> dialogue)
    {
        // CLEARS THE SPEAKER
        speakerSprite.sprite = invisSprite;

        // ENABLES UI
        DialogueUI.SetActive(true);
        //continueImage.SetActive(true);

        // FREEZE PLAYER
        FreezePlayer();
        Debug.Log("FreezePlayer() HAS BEEN CALLED FROM DialogueManager.cs");

        // STORES DIALOGUE FROM DIALOGUE TRIGGER
        inputStream = dialogue;

        // PRINTS FIRST LINE OF DIALOGUE
        PrintDialogue();
    }

    // CONTINUE BUTTON TO ADVANCE DIALOGUE
    public void AdvanceDialogue()
    {
        PrintDialogue();
    }

    private void PrintDialogue()
    {
        if (!isTyping)
        {
            if (inputStream.Peek().Contains("EndQueue"))
            {
                // ENDS DIALOGUE
                if (!isTyping)
                {
                    inputStream.Dequeue();
                    EndDialogue();
                    Debug.Log("END DIALOGUE");
                }
                else
                {
                    cancelTyping = true;
                }
            }
            else if (inputStream.Peek().Contains("[NAME="))
            {
                // SETS SPEAKER NAME TO NAME UI
                string name = inputStream.Peek();
                name = inputStream.Dequeue().Substring(name.IndexOf('=') + 1, name.IndexOf(']') - (name.IndexOf('=') + 1));
                speakerName.text = name;
                PrintDialogue();
            }
            else if (inputStream.Peek().Contains("[SPEAKERSPRITE="))
            {
                string part = inputStream.Peek();
                string spriteName = inputStream.Dequeue().Substring(part.IndexOf('=') + 1, part.IndexOf(']') - (part.IndexOf('=') + 1));
                if (spriteName != "")
                {
                    // SETS SPEAKER SPRITE TO THE SPRITE UI
                    speakerSprite.sprite = speakerSprites.speakerSpriteList[speakerSpriteNames.IndexOf(spriteName)].sprite;
                }
                else
                {
                    // RETURNS SPEAKER SPRITE TO INVISIBLE SPRITE
                    speakerSprite.sprite = invisSprite;
                }
                PrintDialogue();
            }
            else
            {
                dialogueBody.text = inputStream.Dequeue();
                /*
                if (dialogueBody.text == "")
                {
                    StartCoroutine(Wait());
                    //continueImage.SetActive(true);
                }
                */
            }
        }
        else
        {
            if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }
    /*
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4);
        ContinueButton();
    }
    */
    /*
    public void ContinueButton()
    {
        continueImage.SetActive(true);
        AdvanceDialogue();
    }
    */

    // CLOSES DIALOGUE
    public void EndDialogue()
    {
        dialogueBody.text = "";
        speakerName.text = "";
        inputStream.Clear();
        DialogueUI.SetActive(false);
        
        cancelTyping = false;
        isTyping = false;

        UnFreezePlayer();
        Debug.Log("UnFreezePlayer() HAS BEEN CALLED FROM DialogueManager");

        if (currentTrigger.singleUseDialogue)
        {
            currentTrigger.hasBeenUsed = true;
        }
        inputStream.Clear();
    }
}