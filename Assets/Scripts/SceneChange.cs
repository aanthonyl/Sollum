using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    #region Variables
    public bool changeSceneOnActive = false;
    bool inSceneLoadingArea = false;

    #region Settings

    public SceneLoader.Scene nextScene;

    #endregion

    #endregion

    #region Built-in Methods

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && inSceneLoadingArea)
        {
            if (nextScene == SceneLoader.Scene.MainMenu)
            {
                GameManager.instance.enableContinue = false;
                GameManager.instance.LoadMainMenu();
            }
            else
            {
                GameManager.instance.levelTransition = true;
                GameManager.instance.savedScene = nextScene;
                GameManager.instance.LoadGameWorld(false, nextScene);
            }
        }
    }

    private void OnEnable()
    {
        if (changeSceneOnActive)
        {
            GameManager.instance.levelTransition = true;
            GameManager.instance.savedScene = nextScene;
            GameManager.instance.LoadGameWorld(false, nextScene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            inSceneLoadingArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inSceneLoadingArea = false;
    }

    #endregion
}