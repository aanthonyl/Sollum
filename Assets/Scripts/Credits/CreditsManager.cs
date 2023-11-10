using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    public GameObject[] textObjects;
    public int[] creditsBatches;

    public float activationDelay = 1.0f;
    public float waitTimeBetweenActivations = 2.0f;
    public float timeBetweenObjects = 0.5f;
    public float textPrintSpeed = 0.05f;
    private Text textComponent;

    private void Start()
    {
        StartCoroutine(ActivateObjects());
    }

    private IEnumerator ActivateObjects()
    {
        int currentIndex = 0;

        for (int batchIndex = 0; batchIndex < creditsBatches.Length; batchIndex++)
        {
            int batchSize = creditsBatches[batchIndex];

            for (int i = 0; i < batchSize; i++)
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
                }
            }

            yield return new WaitForSeconds(activationDelay);

            for (int i = currentIndex - batchSize; i < currentIndex; i++)
            {
                if (i >= 0 && i < textObjects.Length)
                {
                    textObjects[i].SetActive(false);
                }
            }

            yield return new WaitForSeconds(waitTimeBetweenActivations);
        }
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