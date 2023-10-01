using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    #region Variables

        #region Singleton

            public static PauseMenu instance = null;

        #endregion

        #region References

            [SerializeField] private GameObject resumeButton;
            [SerializeField] private GameObject quitButton;
            [SerializeField] private GameObject pauseBackground;

        #endregion

        #region DEBUG

            [HideInInspector] public bool gameIsPaused = false;
            [HideInInspector] public bool disableEscape = false;

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

        private void Update()
        {
            if(!disableEscape)
            {
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    if(gameIsPaused)
                    {
                        ResumeGame();
                    }
                    else
                    {
                        PauseGame();
                    }
                }
            }
        }

    #endregion

    #region Custom Methods

        private void PauseGame()
        {
            resumeButton.SetActive(true);
            quitButton.SetActive(true);
            pauseBackground.SetActive(true);

            gameIsPaused = true;
        }

        public void ResumeGame()
        {
            resumeButton.SetActive(false);
            quitButton.SetActive(false);
            pauseBackground.SetActive(false);

            gameIsPaused = false;
        }

        public void ExitToMainMenu()
        {
            DisableButtons();
            GameManager.instance.enableContinue = true;
            GameManager.instance.LoadMainMenu();
        }

        private void DisableButtons()
        {
            resumeButton.GetComponent<Image>().enabled = false;
            resumeButton.GetComponent<Button>().enabled = false;
            quitButton.GetComponent<Image>().enabled = false;
            quitButton.GetComponent<Button>().enabled = false;
        }

    #endregion
}