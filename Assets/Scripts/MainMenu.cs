using UnityEngine;

public class MainMenu : MonoBehaviour
{
    #region Variables

        #region Singleton

            public static MainMenu instance = null;

        #endregion

        #region References

            public GameObject continueButton;
            public GameObject newGameButton;
            public GameObject quitButton;

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

        public void Continue()
        {
            SceneLoader.instance.EnableMainMenuButtons(false);
            GameManager.instance.LoadGameWorld(false, GameManager.instance.savedScene);
        }

        public void NewGame()
        {
            GameManager.instance.savedScene = SceneLoader.Scene.Aboveground;
            SceneLoader.instance.EnableMainMenuButtons(false);
            GameManager.instance.LoadGameWorld(true, SceneLoader.Scene.Aboveground);
        }

        public void Quit()
        {
            SceneLoader.instance.EnableMainMenuButtons(false);
            Application.Quit();
        }

    #endregion
}