using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    // ALLOWS DEVELOPER TO SELECT KEY FROM LIST
    [Header("Pause Key")]
    public KeyCode PauseKey = KeyCode.Escape;

    [Header("UI Elements")]
    [Tooltip("Pause UI")]
    public GameObject PauseUI;
    [Tooltip("Image of Location")]
    public Image locationSprite;
    [Tooltip("Name of Location")]
    public Text locationName;
    [Tooltip("Current Objective Text")]
    public Text currentObjective;

    [Header("Player Interaction")]
    [Tooltip("Player Movement Script")]
    public playerMovement playerMovement;

    //[Header("Save System")]
    //[Tooltip("Save Script")]
    //public SaveSystem saveSystem;

    [Header("Scene Change")]
    [Tooltip("Main Menu")]
    public string levelToLoad;

    // HOLDS LIST OF LOCATIONS NAMES AND SPRITES
    [Header("Location Library")]
    [Tooltip("Invisible/Placeholder sprite for when no location info")]
    public Sprite invisSprite;
    public LocationLibrary locationLibrary;
    [HideInInspector]
    public List<string> locationNames;

    private bool pauseActive = false;
    private bool isTyping = false;
    private bool cancelTyping = false;

    [HideInInspector]
    public DialogueTrigger currentTrigger;
    private Queue<string> inputStream = new Queue<string>(); // stores dialogue

    void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();

        foreach (LocationLibrary.SpriteInfo info in locationLibrary.locationSpriteList)
        {
            locationNames.Add(info.name);
        }
        locationSprite.sprite = invisSprite;
    }

    void Update()
    {
        // ADVANCE DIALOGUE USING SELECTED KEY FROM DialogueManager.cs //
        if (Input.GetKeyDown(PauseKey) && pauseActive == false)
        {
            ActivatePause();
        }
        else if (Input.GetKeyDown(PauseKey) && pauseActive == true)
        {
            ContinueButton();
        }
    }

    public void NewLocation(Queue<string> objective)
    {
        // CLEARS THE SPEAKER
        locationSprite.sprite = invisSprite;
        // STORES DIALOGUE FROM DIALOGUE TRIGGER
        inputStream = objective;
        // PRINTS FIRST LINE OF DIALOGUE
        PrintObjective();
    }

    private void PrintObjective()
    {
        if (!isTyping)
        {
            if (inputStream.Peek().Contains("EndQueue"))
            {
                // ENDS OBJECTIVE
                if (!isTyping)
                {
                    inputStream.Dequeue();
                    EndObjective();
                    Debug.Log("END OBJECTIVE");
                }
                else
                {
                    cancelTyping = true;
                }
            }
            else if (inputStream.Peek().Contains("[LOCATION="))
            {
                // SETS SPEAKER NAME TO NAME UI
                string name = inputStream.Peek();
                name = inputStream.Dequeue().Substring(name.IndexOf('=') + 1, name.IndexOf(']') - (name.IndexOf('=') + 1));
                locationName.text = name;
                PrintObjective(); // print the rest of this line
            }
            else if (inputStream.Peek().Contains("[SPRITE="))
            {
                string part = inputStream.Peek();
                string spriteName = inputStream.Dequeue().Substring(part.IndexOf('=') + 1, part.IndexOf(']') - (part.IndexOf('=') + 1));
                if (spriteName != "")
                {
                    // SETS SPEAKER SPRITE TO THE SPRITE UI
                    locationSprite.sprite = locationLibrary.locationSpriteList[locationNames.IndexOf(spriteName)].sprite;
                }
                else
                {
                    // RETURNS SPEAKER SPRITE TO INVISIBLE SPRITE
                    locationSprite.sprite = invisSprite;
                }
                PrintObjective(); // print the rest of this line
            }
        }
    }

    // CLOSES DIALOGUE
    public void EndObjective()
    {
        currentObjective.text = "";
        locationName.text = "";
        inputStream.Clear();
        cancelTyping = false;
        isTyping = false;
        inputStream.Clear();
    }

    // HAULT PLAYER MOVEMENT WHILE DIALOGUE OPEN
    private void FreezePlayer()
    {
        playerMovement.freezeMovement = true;
    }

    private void UnFreezePlayer()
    {
        playerMovement.freezeMovement = false;
    }

    public void ActivatePause()
    {
        Debug.Log("PAUSE GAME");
        FreezePlayer();
        PauseUI.SetActive(true);
        pauseActive = true;
    }

    public void ContinueButton()
    {
        Debug.Log("UNPAUSE GAME");
        PauseUI.SetActive(false);
        UnFreezePlayer();
        pauseActive = false;
    }

    public void SaveButton()
    {
        // Access save system
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
