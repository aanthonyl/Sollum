using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditManager : MonoBehaviour
{
    [SerializeField] private GameObject[] production = new GameObject[0];
    [SerializeField] private GameObject[] design = new GameObject[0];
    [SerializeField] private GameObject[] art = new GameObject[0];
    [SerializeField] private GameObject[] engineering = new GameObject[0];

    [SerializeField] GameObject productionUI, designUI, artUI, engineeringUI;

    public bool next = false;
    private bool wait = false;

    void Start()
    {
        production[0].SetActive(true);
        art[0].SetActive(true);
    }

    public void ProductionNext(int number)
    {
        if (number < 4)
        {
            production[number + 1].SetActive(true);
        }

        if (number == 4)
        {
            Debug.Log("Prod Number 4");

            production[1].SetActive(false);
            production[2].SetActive(false);
            production[3].SetActive(false);
            production[4].SetActive(false);
            production[0].SetActive(false);

            design[0].SetActive(true);
        }
    }

    public void DesignNext(int number)
    {
        if (number < 20)
        {
            design[number + 1].SetActive(true);
        }

        if (number == 5)
        {
            Debug.Log("Design Number 5");
            StartCoroutine(WaitTime());

            design[1].SetActive(false);
            design[2].SetActive(false);
            design[3].SetActive(false);
            design[4].SetActive(false);
            design[5].SetActive(false);

            design[6].SetActive(true);
        }
        else if (number == 10)
        {
            Debug.Log("Design Number 10");
            StartCoroutine(WaitTime());

            design[6].SetActive(false);
            design[7].SetActive(false);
            design[8].SetActive(false);
            design[9].SetActive(false);
            design[10].SetActive(false);

            design[11].SetActive(true);
        }
        else if (number == 15)
        {
            Debug.Log("Design Number 15");
            StartCoroutine(WaitTime());

            design[11].SetActive(false);
            design[12].SetActive(false);
            design[13].SetActive(false);
            design[14].SetActive(false);
            design[15].SetActive(false);

            design[16].SetActive(true);
        }
        else if (number == 20)
        {
            Debug.Log("Design Number 20");
            StartCoroutine(WaitTime());

            design[16].SetActive(false);
            design[17].SetActive(false);
            design[18].SetActive(false);
            design[19].SetActive(false);
            design[20].SetActive(false);
            design[0].SetActive(false);
        }
    }

    public void ArtNext(int number)
    {
        if (number < 15)
        {
            art[number + 1].SetActive(true);
        }

        if (number == 5)
        {
            Debug.Log("Art Number 5");
            StartCoroutine(WaitTime());

            art[1].SetActive(false);
            art[2].SetActive(false);
            art[3].SetActive(false);
            art[4].SetActive(false);
            art[5].SetActive(false);

            art[6].SetActive(true);
        }
        else if (number == 10)
        {
            Debug.Log("Art Number 10");
            StartCoroutine(WaitTime());

            art[6].SetActive(false);
            art[7].SetActive(false);
            art[8].SetActive(false);
            art[9].SetActive(false);
            art[10].SetActive(false);

            art[11].SetActive(true);
        }
        else if (number == 15)
        {
            Debug.Log("Art Number 15");
            StartCoroutine(WaitTime());

            art[number + 1].SetActive(true);
            art[11].SetActive(false);
            art[12].SetActive(false);
            art[13].SetActive(false);
            art[14].SetActive(false);
            art[15].SetActive(false);
            art[0].SetActive(false);

            engineering[0].SetActive(true);
        }
    }

    public void EngineeringNext(int number)
    {
        if (number < 10)
        {
            engineering[number + 1].SetActive(true);
        }

        if (number == 3)
        {
            Debug.Log("Eng Number 3");
            StartCoroutine(WaitTime());

            engineering[2].SetActive(false);
            engineering[3].SetActive(false);

            engineering[4].SetActive(true);
        }
        else if (number == 5)
        {
            Debug.Log("Eng Number 5");
            StartCoroutine(WaitTime());

            engineering[4].SetActive(false);
            engineering[5].SetActive(false);
            engineering[1].SetActive(false);

            engineering[6].SetActive(true);
        }
        else if (number == 8)
        {
            Debug.Log("Eng Number 8");
            StartCoroutine(WaitTime());

            engineering[7].SetActive(false);
            engineering[8].SetActive(false);

            engineering[9].SetActive(true);
        }
        else if (number == 10)
        {
            Debug.Log("Eng Number 10");
            StartCoroutine(WaitTime());

            engineering[9].SetActive(false);
            engineering[10].SetActive(false);
            engineering[6].SetActive(false);
            engineering[0].SetActive(false);
        }
    }

    IEnumerator WaitTime()
    {
        Debug.Log("Wait");
        wait = true;
        yield return new WaitForSeconds(2);
        wait = false;
    }
}
