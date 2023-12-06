/*
    Script Added by Aurora Russell
	10/22/2023
	// CHANGE SCENE //
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelToLoad : MonoBehaviour
{
	public string levelToLoad;
	public bool inExitZone = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inExitZone == true)
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }

    public void LoadLevel(string levelToLoad)
	{
		SceneManager.LoadScene(levelToLoad);
	}

    private void OnTriggerEnter(Collider other)
    {
		inExitZone = true;
    }
}
