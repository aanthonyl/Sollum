using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCredits : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public float activationDelay = 1.0f;
    public float waitTimeBetweenActivations = 2.0f;
    public int[] batchSizes;
    public float timeBetweenObjects = 0.5f;

    private void Start()
    {
        StartCoroutine(ActivateObjects());
    }

    private IEnumerator ActivateObjects()
    {
        int currentIndex = 0;

        for (int batchIndex = 0; batchIndex < batchSizes.Length; batchIndex++)
        {
            int batchSize = batchSizes[batchIndex];

            for (int i = 0; i < batchSize; i++)
            {
                if (currentIndex < objectsToActivate.Length)
                {
                    objectsToActivate[currentIndex].SetActive(true);
                    yield return new WaitForSeconds(timeBetweenObjects);
                    currentIndex++;
                }
            }

            yield return new WaitForSeconds(activationDelay);

            for (int i = currentIndex - batchSize; i < currentIndex; i++)
            {
                if (i >= 0 && i < objectsToActivate.Length)
                {
                    objectsToActivate[i].SetActive(false);
                }
            }

            yield return new WaitForSeconds(waitTimeBetweenActivations);
        }
    }
}