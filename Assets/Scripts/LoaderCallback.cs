using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    #region Variables

        private bool firstUpdate = true;

    #endregion

    #region Built-in Methods

        private void Update()
        {
            if(firstUpdate)
            {
                if(SceneLoader.instance.beginLoaderCallback)
                {
                    firstUpdate = false;
                    SceneLoader.instance.beginLoaderCallback = false;
                    GameManager.instance.UnloadPreviousScene();
                }
            }
        }

    #endregion
}