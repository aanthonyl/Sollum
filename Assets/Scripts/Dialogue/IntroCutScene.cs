using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroCutScene : MonoBehaviour
{
    private void OnEnable()
    {
        //SceneManager.LoadScene("Aboveground", LoadSceneMode.Single);
        SceneLoader.instance.Load(Aboveground);
    }
}
