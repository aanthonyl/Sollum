using System.Collections.Generic;

[System.Serializable]

public class MainMenuData
{
    #region Variables

        #region Data

            public SceneLoader.Scene savedScene;
            public bool enableContinue;

        #endregion

    #endregion

    #region Constructor

        public MainMenuData(bool serializationFinished)
        {
            if(serializationFinished) // SAVES DATA BELOW
            {
                savedScene = GameManager.instance.savedScene;
                enableContinue = GameManager.instance.enableContinue;
            }
        }

    #endregion
}