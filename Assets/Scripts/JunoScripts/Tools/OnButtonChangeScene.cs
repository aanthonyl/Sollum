using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnButtonChangeScene : MonoBehaviour
{
    public void ChangeScene()
    {
        GameManager.instance.enableContinue = false;
        GameManager.instance.LoadMainMenu();
    }
}
