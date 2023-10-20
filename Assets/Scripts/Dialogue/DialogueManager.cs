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

    [Header("Scrolling Options")]
    [Tooltip("Scroll Through Dialogue?")]
    public bool isScrollingText = true;
    [Tooltip("Scroll Speed")]
    public float typeSpeed = 0.05f;
    private string scrollText;

    private Queue<string> inputStream = new Queue<string>(); // stores dialogue

    // SYSTEMS TO BE FROZEN WHILE DIALOGUE IS OPEN
    [HideInInspector]
    public playerMovement playerMovement;
    [HideInInspector]
    public PauseManager pauseManager;
    //[HideInInspector]
    //public WhipManager whipManager;
    //[HideInInspector]
    //public EnemyAttack enemyAttack;

    // ALLOWS DEVELOPER TO SELECT KEY FROM LIST
    [Header("Continue Key")]
    public KeyCode DialogueKey = KeyCode.E;

    [HideInInspector]
    public DialogueTrigger currentTrigger;

    // BOOLEANS
    [HideInInspector]
    public bool freezePlayerOnDialogue = true;
    private bool isInDialogue = false;
    private bool isTyping = false;
    private bool cancelTyping = false;
    [HideInInspector]
    public bool dialogueActive = false;

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
        //whipManager = GameObject.Find("WhipManager").GetComponent<WhipManager>();
        //enemyAttack = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAttack>();

        // GET SPEAKERS FROM LIBRARY & ADD TO LIST
        foreach (SpeakerLibrary.SpriteInfo info in speakerSprites.speakerSpriteList)
        {
            speakerSpriteNames.Add(info.name);
        }
        speakerSprite.sprite = invisSprite;
    }

    // HAULT PLAYER MOVEMENT, WHIP ATTACK, ENEMY ATTACKS, PAUSE MENU
    private void FreezePlayer()
    {
        dialogueActive = true;
        playerMovement.freezeMovement = true;
        pauseManager.dialogueOpen = true;
        //whipManager.dialogueOpen = true;
        //enemyAttack.freezeAttack = true;
    }

    // RESTORE PLAYER MOVEMENT, WHIP ATTACK, ENEMY ATTACKS
    private void UnFreezePlayer()
    {
        dialogueActive = false;
        playerMovement.freezeMovement = false;
        pauseManager.dialogueOpen = false;
        //whipManager.dialogueOpen = false;
        //enemyAttack.freezeAttack = false;
    }

    // STARTS DIALOGUE
    public void StartDialogue(Queue<string> dialogue)
    {
        // SETS BOOL
        isInDialogue = true;
        // CLEARS THE SPEAKER
        speakerSprite.sprite = invisSprite;
        // ENABLES UI
        DialogueUI.SetActive(true);
        continueImage.SetActive(false);
        // FREEZE PLAYER
        if (freezePlayerOnDialogue)
        {
            FreezePlayer();
            Debug.Log("FreezePlayer() HAS BEEN CALLED FROM DialogueManager.cs");
        }

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
            if (inputStream.Peek().Contains("EndQueue")) // special phrase to stop dialogue
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
                PrintDialogue(); // print the rest of this line
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
                if (isScrollingText)
                {
                    if (!isTyping)
                    {
                        // STARTS COROUTINE
                        scrollText = inputStream.Dequeue();
                        StartCoroutine(TextScroll(scrollText));
                        Debug.Log("TextScroll COROUTINE HAS BEEN STARTED");
                    }
                }
                else
                {
                    dialogueBody.text = inputStream.Dequeue();
                    continueImage.SetActive(true);
                }
            }
        }
        else
        {
            if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
                dialogueBody.text = scrollText;
            }
        }
    }

    // COROUTINE TO HANDLE SCROLLING TIMEFRAME
    private IEnumerator TextScroll(string lineOfText)
    {
        // CANNOT HIT CONTINUE WHILE TEXT SCROLLING
        continueImage.SetActive(false);
        int letter = 0;
        dialogueBody.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            dialogueBody.text += lineOfText[letter];
            letter++;
            yield return new WaitForSeconds(typeSpeed);
        }

        dialogueBody.text = lineOfText;
        // ENABLES CONTINUE BUTTON
        continueImage.SetActive(true);
        isTyping = false;
        cancelTyping = false;
    }

    // CLOSES DIALOGUE
    public void EndDialogue()
    {
        dialogueBody.text = "";
        speakerName.text = "";
        inputStream.Clear();
        DialogueUI.SetActive(false);

        isInDialogue = false;
        cancelTyping = false;
        isTyping = false;

        if (freezePlayerOnDialogue)
        {
            UnFreezePlayer();
            Debug.Log("UnFreezePlayer() HAS BEEN CALLED FROM DialogueManager");
        }

        if (currentTrigger.singleUseDialogue)
        {
            currentTrigger.hasBeenUsed = true;
        }
        inputStream.Clear();
    }
}