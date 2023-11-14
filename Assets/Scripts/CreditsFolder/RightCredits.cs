using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightCredits : MonoBehaviour
{
    public GameObject[] rightObjects;
    public float activationDelay = 1.0f;
    public float waitTimeBetweenActivations = 2.0f;
    public int[] rightBatches;
    public float timeBetweenObjects = 0.5f;
    public float textPrintSpeed = 0.05f;
    private Text textComponent;

    private void Start()
    {
        StartCoroutine(ActivateRightObjects());
    }

    private IEnumerator ActivateRightObjects()
    {
        int currentIndex = 0;

        for (int batchIndex = 0; batchIndex < rightBatches.Length; batchIndex++)
        {
            int batchSize = rightBatches[batchIndex];

            for (int i = 0; i < batchSize; i++)
            {
                if (currentIndex < rightObjects.Length)
                {
                    // Get the Text component from the current text object
                    textComponent = rightObjects[currentIndex].GetComponent<Text>();

                    // Print text gradually
                    yield return StartCoroutine(PrintText(textComponent));

                    rightObjects[currentIndex].SetActive(true);
                    yield return new WaitForSeconds(timeBetweenObjects);
                    currentIndex++;
                }
            }

            yield return new WaitForSeconds(activationDelay);

            for (int i = currentIndex - batchSize; i < currentIndex; i++)
            {
                if (i >= 0 && i < rightObjects.Length)
                {
                    rightObjects[i].SetActive(false);
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
