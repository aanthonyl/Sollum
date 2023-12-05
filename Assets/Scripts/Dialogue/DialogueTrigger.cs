/*
	Script Added by Aurora Russell
	09/30/2023
	// TRIGGERS DIALOGUE UI //
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // SCRIPT TO BE PLACED ON BOX TRIGGER GAME OBJECT WHERE PLAYER WILL ACTIVATE DIALOGUE

    DialogueManager manager;
    public TextAsset DialogueTextFile; // Your imported text file

    private Queue<string> dialogue = new Queue<string>(); // Stores the dialogue
    public float waitTime = 0.5f; // Lag time for advancing dialogue so you can actually read it

    public bool singleUseDialogue = false; // Set true if they should be able to enter dialogue again
    [HideInInspector]
    public bool hasBeenUsed = false;
    public bool enterZoom = false;
    public bool exitZoom = false;
    public Vector3[] ZoomVals = new Vector3[3];

    private GameObject hpbar;
    private GameObject camref;
    private bool once = true;

    private void Start()
    {
        manager = FindObjectOfType<DialogueManager>();
        hpbar = GameObject.Find("Canvas/HealthBar");
        camref = GameObject.Find("Main Camera 1");
    }

    // START DIALOGUE //
    void TriggerDialogue()
    {
        ReadTextFile(); // Loads in the text file
        manager.inDialogueZone = true;
        manager.StartDialogue(dialogue, exitZoom); // Accesses Dialogue Manager and Starts Dialogue
    }

    // LOAD TEXT FILE //
    private void ReadTextFile()
    {
        string txt = DialogueTextFile.text;

        string[] lines = txt.Split(System.Environment.NewLine.ToCharArray()); // Split dialogue lines by newline

        SearchForTags(lines);

        dialogue.Enqueue("EndQueue");
    }

    private void SearchForTags(string[] lines)
    {
        foreach (string line in lines) // For every line of dialogue
        {
            if (!string.IsNullOrEmpty(line)) // Ignore empty lines of dialogue
            {
                if (line.StartsWith("[")) // e.g [NAME=Michael] Hello, my name is Michael
                {
                    string special = line.Substring(0, line.IndexOf(']') + 1); // special = [NAME=Michael]
                    string curr = line.Substring(line.IndexOf(']') + 1); // curr = Hello, ...
                    dialogue.Enqueue(special); // adds to the dialogue to be printed
                    string[] remainder = curr.Split(System.Environment.NewLine.ToCharArray());
                    SearchForTags(remainder);
                }
                else
                {
                    dialogue.Enqueue(line); // adds to the dialogue to be printed
                }
            }
        }
    }

    // TRIGGER DIALOGUE, UNTIL LEAVES AREA //
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !hasBeenUsed)
        {
            manager.currentTrigger = this;
            TriggerDialogue();
            // Debug.Log("DIALOGUE TRIGGERED");
            hpbar.SetActive(false);
            if (enterZoom)
            {
                StartCoroutine(camref.GetComponent<CameraControl>().PanToPositionHold(ZoomVals[0], ZoomVals[2].z));
                StartCoroutine(camref.GetComponent<CameraControl>().DynamicRotation(ZoomVals[1].x, ZoomVals[1].y, ZoomVals[1].z, ZoomVals[2].z));
                StartCoroutine(camref.GetComponent<CameraControl>().DynamicZoom(ZoomVals[2].x, ZoomVals[2].z));

            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && once)
        {
            manager.inDialogueZone = false;
            //manager.EndDialogue();
            //hpbar.SetActive(true);
            /*if(exitZoom && once){
                StartCoroutine(camref.GetComponent<CameraControl>().PanToPlayer());
                StartCoroutine(camref.GetComponent<CameraControl>().DynamicRotation(15,0,0));
                StartCoroutine(camref.GetComponent<CameraControl>().DynamicZoom(60));
                once = false;
            } */
        }
    }
}
