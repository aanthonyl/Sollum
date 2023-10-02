using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    #region Variables

        #region Singleton

            public static SceneLoader instance = null;

        #endregion
    
        #region Inspector

            [Tooltip("The amount of seconds it takes to fade in and out of scenes")]
            public float fadeLength = 1f;
            [Tooltip("The amount of seconds between each fade out and fade in")]
            public float transitionLength = 1f;
            [Tooltip("The minimum amount of seconds held on each loading screen")]
            public float minimumLoadingLength = 1f;

        #endregion
    
        #region DEBUG
        
            public enum Scene
            {
                MainMenu,
                Loading,
                Aboveground,
                Underground
            }

            [HideInInspector] public bool crossfadeComplete = false;
            [HideInInspector] public bool beginLoaderCallback = false;
        
        #endregion
    
        #region Components
        
            private Action onLoaderCallback;
            private AsyncOperation loadingAsyncOperation;
            private AsyncOperation unloadingAsyncOperation;
            private AsyncOperation setupAsyncOperation;
        
        #endregion
    
    #endregion
    
    #region Built-in Methods
    
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    
    #endregion
    
    #region Custom Methods
    
        public void Load(Scene scene)
        {
            onLoaderCallback = () =>
            {
                LoadScene(scene);
            };

            LoadScene(Scene.Loading);
        }

        public void LoaderCallback()
        {
            if(onLoaderCallback != null)
            {
                onLoaderCallback();
                onLoaderCallback = null;
            }
        }

        public void LoadScene(Scene scene)
        {
            StartCoroutine(LoadSceneAsync(scene));
        }

        public void UnloadScene(Scene scene)
        {
            StartCoroutine(UnloadSceneAsync(scene));
        }

        public void GameSetup()
        {
            StartCoroutine(LoadSetup());
        }

        public void EnableMainMenuButtons(bool enableButtons)
        {
            if(enableButtons)
            {
                MainMenu.instance.continueButton.GetComponent<Button>().enabled = true;
                MainMenu.instance.continueButton.GetComponent<Image>().enabled = true;
                MainMenu.instance.newGameButton.GetComponent<Button>().enabled = true;
                MainMenu.instance.newGameButton.GetComponent<Image>().enabled = true;
                MainMenu.instance.quitButton.GetComponent<Button>().enabled = true;
                MainMenu.instance.quitButton.GetComponent<Image>().enabled = true;
            }
            else
            {
                MainMenu.instance.continueButton.GetComponent<Image>().enabled = false;
                MainMenu.instance.continueButton.GetComponent<Button>().enabled = false;
                MainMenu.instance.newGameButton.GetComponent<Image>().enabled = false;
                MainMenu.instance.newGameButton.GetComponent<Button>().enabled = false;
                MainMenu.instance.quitButton.GetComponent<Image>().enabled = false;
                MainMenu.instance.quitButton.GetComponent<Button>().enabled = false;
            }
        }

    #endregion
    
    #region Coroutines
    
        private IEnumerator LoadSceneAsync(Scene scene)
        {
            if(scene == Scene.Loading) // FADE OUT OF PREVIOUS SCENE
            {
                CrossfadeController.instance.FadeOut(fadeLength);

                while(!crossfadeComplete)
                {
                    yield return null;
                }

                crossfadeComplete = false;

                yield return new WaitForSeconds(transitionLength);
            }
            else
            {
                yield return new WaitForSeconds(minimumLoadingLength);
            }

            loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive); // LOAD NEXT SCENE

            while(!loadingAsyncOperation.isDone)
            {
                yield return null;
            }

            if(scene == Scene.Loading) // FADE INTO LOADING SCENE
            {
                CrossfadeController.instance.FadeIn(fadeLength);

                while(!crossfadeComplete)
                {
                    yield return null;
                }

                crossfadeComplete = false;
            }
            else  // FADE OUT OF LOADING SCENE
            {
                if(scene == Scene.Aboveground || scene == Scene.Underground) // LOAD GAME DATA
                {
                    GameManager.instance.LoadGame();

                    while(!GameManager.instance.dataLoaded)
                    {
                        yield return null;
                    }

                    GameManager.instance.dataLoaded = false;
                    GameManager.instance.levelTransition = false;
                }
                else if(scene == Scene.MainMenu) // LOAD MAIN MENU DATA AGAIN
                {
                    GameManager.instance.EnableContinue();
                    GameManager.instance.levelTransition = false;
                }

                GameManager.instance.currentScene = scene;

                CrossfadeController.instance.FadeOut(fadeLength);

                while(!crossfadeComplete)
                {
                    yield return null;
                }

                crossfadeComplete = false;

                yield return new WaitForSeconds(transitionLength);
            }

            if(scene == Scene.Loading)
            {
                beginLoaderCallback = true;
            }
            else
            {
                UnloadScene(Scene.Loading);
            }
        }

        private IEnumerator UnloadSceneAsync(Scene scene)
        {
            if(scene == Scene.Aboveground || scene == Scene.Underground)
            {
                GameManager.instance.SaveGame(); // SAVE GAME DATA

                while(!GameManager.instance.dataSaved)
                {
                    yield return null;
                }

                GameManager.instance.dataSaved = false; 

                GameManager.instance.SaveMainMenuData(); // SAVE MAIN MENU DATA

                while(!GameManager.instance.dataSaved)
                {
                    yield return null;
                }

                GameManager.instance.dataSaved = false;
            }

            unloadingAsyncOperation = SceneManager.UnloadSceneAsync(scene.ToString()); // UNLOAD SCENE

            while(!unloadingAsyncOperation.isDone)
            {
                yield return null;
            }

            if(scene != Scene.Loading)
            {
                LoaderCallback();
            }
            else
            {
                CrossfadeController.instance.FadeIn(fadeLength);

                while(!crossfadeComplete)
                {
                    yield return null;
                }

                crossfadeComplete = false;

                if(GameManager.instance.currentScene == Scene.MainMenu)
                {
                    EnableMainMenuButtons(true); // ENABLE MAIN MENU
                }
                else if(GameManager.instance.currentScene == Scene.Aboveground || GameManager.instance.currentScene == Scene.Underground)
                {
                    // START GAME
                }
            }
        }

        private IEnumerator LoadSetup()
        {
            setupAsyncOperation = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive); // LOAD MAIN MENU

            while(!setupAsyncOperation.isDone)
            {
                yield return null;
            }

            GameManager.instance.LoadMainMenuData(); // LOAD MAIN MENU DATA

            while(!GameManager.instance.dataLoaded)
            {
                yield return null;
            }

            GameManager.instance.dataLoaded = false;

            CrossfadeController.instance.FadeIn(fadeLength); // FADE INTO MAIN MENU

            while(!crossfadeComplete)
            {
                yield return null;
            }

            crossfadeComplete = false;

            EnableMainMenuButtons(true); // ENABLE MAIN MENU
        }
    
    #endregion
}