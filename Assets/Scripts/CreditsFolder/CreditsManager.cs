using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    public GameObject[] textObjects;
    public int[] creditsBatches;

    public float activationDelay;
    public float waitTimeBetweenActivations;
    public float timeBetweenObjects = 0.5f;
    public float textPrintSpeed = 0.05f;
    private Text textComponent;
    public GameObject flowerUI, logoUI, thnxText;

    private void Start()
    {
        StartCoroutine(ActivateObjects());
    }

    private IEnumerator ActivateObjects()
    {
        int currentIndex = 0;
        float currentActivationDelay = 5.0f;

        for (int batchIndex = 0; batchIndex < creditsBatches.Length; batchIndex++)
        {
            int batchSize = creditsBatches[batchIndex];

            for (int i = 0; i < batchSize; i += 2)
            {
                if (currentIndex < textObjects.Length)
                {
                    // Get the Text component from the current text object
                    textComponent = textObjects[currentIndex].GetComponent<Text>();

                    // Print text gradually
                    yield return StartCoroutine(PrintText(textComponent));

                    textObjects[currentIndex].SetActive(true);
                    yield return new WaitForSeconds(timeBetweenObjects);
                    currentIndex++;

                    if (i + 1 < batchSize && currentIndex < textObjects.Length)
                    {
                        textComponent = textObjects[currentIndex].GetComponent<Text>();
                        yield return StartCoroutine(PrintText(textComponent));

                        textObjects[currentIndex].SetActive(true);
                        currentIndex++;
                    }

                }
                else
                {
                    EndCredits();
                }
            }

            yield return new WaitForSeconds(currentActivationDelay);

            for (int i = currentIndex - batchSize; i < currentIndex; i++)
            {
                if (i >= 0 && i < textObjects.Length)
                {
                    textObjects[i].SetActive(false);
                }
            }

            if (batchIndex == 1)
            {
                currentActivationDelay = 7.5f;
            }
            else if (batchIndex == 2)
            {
                currentActivationDelay = 11f;
            }
            else if (batchIndex == 3)
            {
                currentActivationDelay = 15f;
            }
            else if (batchIndex >= 4)
            {
                EndCredits();
            }

            yield return new WaitForSeconds(waitTimeBetweenActivations);
        }
    }

    private void EndCredits()
    {
        logoUI.SetActive(false);
        flowerUI.SetActive(false);
        thnxText.SetActive(true);
    }

    private IEnumerator PrintText(Text textComponent)
    {
        string originalText = textComponent.text;
        textComponent.text = "";

        foreach (char c in originalText)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textPrintSpeed);
        }
    }
}