/*
    Script Added by Aurora Russell
	10/08/2023
	// LOCATION TRIGGER SENDS LOCATION INFO TO LOCATION MANAGER //
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTrigger : MonoBehaviour
{
    // SCRIPT TO BE PLACED ON BOX TRIGGER GAME OBJECT WHERE PLAYER WILL PASS THROUGH SETTING NEW LOCATION

    PauseManager manager;
    public TextAsset LocationTextFile; // Your imported text file

    private Queue<string> objective = new Queue<string>(); // Stores the dialogue

    private void Start()
    {
        manager = FindObjectOfType<PauseManager>();
    }

    // START OBJECTIVE //
    void TriggerLocation()
    {
        ReadTextFile(); // Loads in the text file
        manager.NewLocation(objective); // Accesses Pause Manager and Starts Objective
    }

    // LOAD TEXT FILE //
    private void ReadTextFile()
    {
        string txt = LocationTextFile.text;

        string[] lines = txt.Split(System.Environment.NewLine.ToCharArray()); // Split lines by newline

        SearchForTags(lines);

        objective.Enqueue("EndQueue");
    }

    private void SearchForTags(string[] lines)
    {
        foreach (string line in lines) // For every line of text
        {
            if (!string.IsNullOrEmpty(line)) // Ignore empty lines
            {
                if (line.StartsWith("[")) // e.g [NAME=Michael]
                {
                    string special = line.Substring(0, line.IndexOf(']') + 1); // special = [NAME=Michael]
                    string curr = line.Substring(line.IndexOf(']') + 1); // curr = Hello, ...
                    objective.Enqueue(special); // adds on to be printed
                    string[] remainder = curr.Split(System.Environment.NewLine.ToCharArray());
                    SearchForTags(remainder);
                }
                else
                {
                    objective.Enqueue(line);
                }
            }
        }

    }

    // TRIGGER NEW LOCATION, NEW OBJECTIVE //
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            TriggerLocation();
            this.gameObject.SetActive(false);
            Debug.Log("NEW LOCATION TRIGGERED");
        }
    }
}
