using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{
    public AudioClip pressButton;
    public AudioSource audioSource;

    public void GoToMainMenu()
    {
        SceneLoader.instance.Load(SceneLoader.Scene.MainMenu);
        audioSource.clip = pressButton;
        audioSource.Play();
    }
}
