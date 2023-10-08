using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // ALLOWS DEVELOPER TO SELECT KEY FROM LIST
    [Header("Pause Key")]
    public KeyCode PauseKey = KeyCode.Escape;

    [Header("UI Elements")]
    [Tooltip("Pause UI")]
    public GameObject PauseUI;

    //[Header("Save System")]
    //[Tooltip("Save Script")]
    //public SaveSystem saveSystem;

    [Header("Scene Change")]
    [Tooltip("Main Menu")]
    public string levelToLoad;

    void Start()
    {
        
    }

    void Update()
    {
        // ADVANCE DIALOGUE USING SELECTED KEY FROM DialogueManager.cs //
        if (Input.GetKeyDown(PauseKey))
        {
            Debug.Log("PAUSE GAME");
            PauseUI.SetActive(true);
        }
    }

    public void ContinueButton()
    {
        Debug.Log("PAUSE GAME");
        PauseUI.SetActive(false);
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
