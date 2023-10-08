using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    #region Variables

        #region Settings

            public SceneLoader.Scene nextScene;

        #endregion

    #endregion

    #region Built-in Methods

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(nextScene == SceneLoader.Scene.MainMenu)
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

    #endregion
}