using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
     //used to load game first time (scene 1)
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void onResumeButton()
    {
        //should resume game
    }

    public void volumeUPButton()
    {
        //should add to volume
    }
    public void volumeDOWNButton()
    {
        //should subtract from volume
    }

}
