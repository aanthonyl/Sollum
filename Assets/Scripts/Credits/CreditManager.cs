using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.UI;

public class CreditManager : MonoBehaviour
{
    public GameObject[] leftObjects;
    public GameObject[] rightObjects;
    public float activationDelay = 1.0f;
    public float waitTimeBetweenActivations = 2.0f;
    public int[] leftBatches;
    public int[] rightBatches;
    public float timeBetweenObjects = 0.5f;
    public float textPrintSpeed = 0.05f; // Adjust the text print speed as needed
    private Text textComponent; // Use Text for legacy text objects

    private void Start()
    {
        StartCoroutine(ActivateLeftObjects());
        //StartCoroutine(ActivateRightObjects());
    }

    private IEnumerator ActivateLeftObjects()
    {
        int currentIndex = 0;

        for (int batchIndex = 0; batchIndex < leftBatches.Length; batchIndex++)
        {
            int batchSize = leftBatches[batchIndex];

            for (int i = 0; i < batchSize; i++)
            {
                if (currentIndex < leftObjects.Length)
                {
                    // Get the Text component from the current text object
                    textComponent = leftObjects[currentIndex].GetComponent<Text>();

                    // Print text gradually
                    yield return StartCoroutine(PrintText(textComponent));

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

                    leftObjects[currentIndex].SetActive(true);
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


/*
[SerializeField] private GameObject[] production = new GameObject[0];
[SerializeField] private GameObject[] design = new GameObject[0];
[SerializeField] private GameObject[] art = new GameObject[0];
[SerializeField] private GameObject[] engineering = new GameObject[0];

//[SerializeField] GameObject productionUI, designUI, artUI, engineeringUI;

public bool next = false;
private bool oneWait = false;
private bool twoWait = false;
private bool waitDone = false;

//void Start()
{
    production[0].SetActive(true);
    art[0].SetActive(true);
}

public void ProductionNext(int number)
{
    if (number < 4 && !oneWait)
    {
        production[number + 1].SetActive(true);
    }

    if (number == 4)
    {
        Debug.Log("Prod Number 4");
        StartCoroutine(WaitTime(1));
        if (waitDone)
        {
            design[0].SetActive(true);
            waitDone = false;
        }
    }
}

public void DesignNext(int number)
{
    if (number < 20 && !oneWait)
    {
        design[number + 1].SetActive(true);
    }

    if (number == 5)
    {
        Debug.Log("Design Number 5");
        StartCoroutine(WaitTime(2));
        if (waitDone)
        {
            design[6].SetActive(true);
            waitDone = false;
        }
    }
    else if (number == 10)
    {
        Debug.Log("Design Number 10");
        StartCoroutine(WaitTime(3));
        if (waitDone)
        {
            design[11].SetActive(true);
            waitDone = false;
        }
    }
    else if (number == 15)
    {
        Debug.Log("Design Number 15");
        StartCoroutine(WaitTime(4));
        if (waitDone)
        {
            design[16].SetActive(true);
            waitDone = false;
        }
    }
    else if (number == 20)
    {
        Debug.Log("Design Number 20");
        StartCoroutine(WaitTime(5));
    }
}

public void ArtNext(int number)
{
    if (number < 15 && !twoWait)
    {
        art[number + 1].SetActive(true);
    }

    if (number == 5)
    {
        Debug.Log("Art Number 5");
        StartCoroutine(WaitTime(6));
        if (waitDone)
        {
            art[6].SetActive(true);
            waitDone = false;
        }
    }
    else if (number == 10)
    {
        Debug.Log("Art Number 10");
        StartCoroutine(WaitTime(7));
        if (waitDone)
        {
            art[11].SetActive(true);
            waitDone = false;
        }
    }
    else if (number == 15)
    {
        Debug.Log("Art Number 15");
        //art[number + 1].SetActive(true);
        StartCoroutine(WaitTime(8));
        if (waitDone)
        {
            engineering[0].SetActive(true);
            waitDone = false;
        }
    }
}

public void EngineeringNext(int number)
{
    if (number < 10 && !twoWait)
    {
        engineering[number + 1].SetActive(true);
    }

    if (number == 3)
    {
        Debug.Log("Eng Number 3");
        StartCoroutine(WaitTime(9));
        if (waitDone)
        {
            engineering[4].SetActive(true);
            waitDone = false;
        }
    }
    else if (number == 5)
    {
        Debug.Log("Eng Number 5");
        StartCoroutine(WaitTime(10));
        if (waitDone)
        {
            engineering[6].SetActive(true);
            waitDone = false;
        }
    }
    else if (number == 8)
    {
        Debug.Log("Eng Number 8");
        StartCoroutine(WaitTime(11));
        if (waitDone)
        {
            engineering[9].SetActive(true);
            waitDone = false;
        }
    }
    else if (number == 10)
    {
        Debug.Log("Eng Number 10");
        StartCoroutine(WaitTime(12));
    }
}

IEnumerator WaitTime(int sect)
{
    Debug.Log("Wait");
    oneWait = true;
    twoWait = true;
    yield return new WaitForSeconds(3);

    if (sect == 1)
    {
        production[1].SetActive(false);
        production[2].SetActive(false);
        production[3].SetActive(false);
        production[4].SetActive(false);
        production[0].SetActive(false);
    }
    else if (sect == 2)
    {
        design[1].SetActive(false);
        design[2].SetActive(false);
        design[3].SetActive(false);
        design[4].SetActive(false);
        design[5].SetActive(false);
    }
    else if (sect == 3)
    {
        design[6].SetActive(false);
        design[7].SetActive(false);
        design[8].SetActive(false);
        design[9].SetActive(false);
        design[10].SetActive(false);
    }
    else if (sect == 4)
    {
        design[11].SetActive(false);
        design[12].SetActive(false);
        design[13].SetActive(false);
        design[14].SetActive(false);
        design[15].SetActive(false);
    }
    else if (sect == 5)
    {
        design[16].SetActive(false);
        design[17].SetActive(false);
        design[18].SetActive(false);
        design[19].SetActive(false);
        design[20].SetActive(false);
        design[0].SetActive(false);
    }
    else if (sect == 6)
    {
        art[1].SetActive(false);
        art[2].SetActive(false);
        art[3].SetActive(false);
        art[4].SetActive(false);
        art[5].SetActive(false);
    }
    else if (sect == 7)
    {
        art[6].SetActive(false);
        art[7].SetActive(false);
        art[8].SetActive(false);
        art[9].SetActive(false);
        art[10].SetActive(false);
    }
    else if (sect == 8)
    {
        art[11].SetActive(false);
        art[12].SetActive(false);
        art[13].SetActive(false);
        art[14].SetActive(false);
        art[15].SetActive(false);
        art[0].SetActive(false);
    }
    else if (sect == 9)
    {
        engineering[2].SetActive(false);
        engineering[3].SetActive(false);
    }
    else if (sect == 10)
    {
        engineering[4].SetActive(false);
        engineering[5].SetActive(false);
        engineering[1].SetActive(false);
    }
    else if (sect == 11)
    {
        engineering[7].SetActive(false);
        engineering[8].SetActive(false);
    }
    else if (sect == 12)
    {
        engineering[9].SetActive(false);
        engineering[10].SetActive(false);
        engineering[6].SetActive(false);
        engineering[0].SetActive(false);
    }

    oneWait = false;
    twoWait = false;
    waitDone = true;
}
}
*/