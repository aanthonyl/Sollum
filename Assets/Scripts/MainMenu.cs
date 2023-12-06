using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region Variables

    #region Singleton

    public static MainMenu instance = null;

    #endregion

    #region References

    public GameObject continueButton;
    public GameObject newGameButton;
    public GameObject creditsButton;
    public GameObject exitButton;

    //Audio
    public AudioSource audioSource;
    //public AudioClip hoverButton;
    public AudioClip pressButton;
    public AudioClip startGameChime;

    //This is used to prevent the startGameChime sound from being interrupted if the player clicks a button while the scene is transitioning
    public bool gameStarting = false;

    #endregion

    #endregion

    #region Built-in Methods

    private void Awake()
    {
        if (instance == null)
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

    public void Continue()
    {
        if (!gameStarting)
        {
            audioSource.clip = startGameChime;
            audioSource.Play();
            gameStarting = true;
        }

        SceneLoader.instance.EnableMainMenuButtons(false);
        GameManager.instance.LoadGameWorld(false, GameManager.instance.savedScene);
    }

    public void NewGame()
    {
        //UNTESTED:
        //I was unable to test if there is already a Crossfade transition here, and whether it is long enough for the full sound to play out (so that the sound doesn't get awkwardly cut off).
        //If this is not the case, the Crossfade transition could be lengthened, or a WaitForSeconds coroutine could be placed below, in between the audio playing and the scene loading.

        if (!gameStarting)
        {
            audioSource.clip = startGameChime;
            audioSource.Play();
            gameStarting = true;
        }



        GameManager.instance.savedScene = SceneLoader.Scene.Aboveground;
        SceneLoader.instance.EnableMainMenuButtons(false);
        GameManager.instance.LoadGameWorld(true, SceneLoader.Scene.IntroCutScene);


    }

    public void Credits()
    {
        // LOAD CREDITS
        if (!gameStarting)
        {
            SceneLoader.instance.Load(SceneLoader.Scene.Credits);
            audioSource.clip = pressButton;
            audioSource.Play();
            audioSource.loop = false;
            // SceneManager.LoadScene("Credits");
        }
    }

    public void Exit()
    {
        SceneLoader.instance.EnableMainMenuButtons(false);
        Application.Quit();
    }

    #endregion
}