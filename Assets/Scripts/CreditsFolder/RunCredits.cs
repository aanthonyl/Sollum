using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCredits : MonoBehaviour
{
    public GameObject[] leftObjects;
    public GameObject[] rightObjects;
    public float activationDelay = 1.0f;
    public float waitTimeBetweenActivations = 2.0f;
    public int[] leftBatches;
    public int[] rightBatches;
    public float timeBetweenObjects = 0.5f;

    private void Start()
    {
        StartCoroutine(ActivateObjects());
    }

    private IEnumerator ActivateObjects()
    {
        int currentIndex = 0;

        for (int batchIndex = 0; batchIndex < leftBatches.Length; batchIndex++)
        {
            int batchSize = leftBatches[batchIndex];

            for (int i = 0; i < batchSize; i++)
            {
                if (currentIndex < leftObjects.Length)
                {
                    leftObjects[currentIndex].SetActive(true);
                    yield return new WaitForSeconds(timeBetweenObjects);
                    currentIndex++;
                }
            }

            yield return new WaitForSeconds(activationDelay);

            for (int i = currentIndex - batchSize; i < currentIndex; i++)
            {
                if (i >= 0 && i < leftObjects.Length)
                {
                    leftObjects[i].SetActive(false);
                }
            }

            yield return new WaitForSeconds(waitTimeBetweenActivations);
        }

        for (int batchIndex = 0; batchIndex < rightBatches.Length; batchIndex++)
        {
            int batchSize = rightBatches[batchIndex];

            for (int i = 0; i < batchSize; i++)
            {
                if (currentIndex < rightObjects.Length)
                {
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
}