/*
	Script Added by Aurora Russell
	12/05/23
	// TRANSITIONS FOR INTRO CUTSCENE //
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public GameObject[] slides;
    public GameObject fadeScreen;
    public float fadeDuration = 1.0f;

    private int currentIndex = 0;
    private bool isFading = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isFading)
        {
            StartCoroutine(TransitionToNextSlide());
        }
    }

    public void NextSlide()
    {
        if (!isFading)
        {
            StartCoroutine(TransitionToNextSlide());
        }
    }

    IEnumerator TransitionToNextSlide()
    {
        isFading = true;

        // Fade in
        yield return StartCoroutine(Fade(fadeScreen.GetComponent<Image>(), 0, 1, fadeDuration));

        // Deactivate current object
        slides[currentIndex].SetActive(false);

        // Increment index
        currentIndex++;

        // Check if it's the last object
        if (currentIndex >= slides.Length)
        {
            // Transition to a different scene
            SceneManager.LoadScene("Aboveground");
            yield break;
        }

        // Activate next object
        slides[currentIndex].SetActive(true);

        // Fade out
        yield return StartCoroutine(Fade(fadeScreen.GetComponent<Image>(), 1, 0, fadeDuration));

        isFading = false;
    }

    IEnumerator Fade(Image image, float startAlpha, float targetAlpha, float duration)
    {
        float currentTime = 0;
        Color startColor = image.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            image.color = Color.Lerp(startColor, targetColor, currentTime / duration);
            yield return null;
        }

        image.color = targetColor;
    }
}
