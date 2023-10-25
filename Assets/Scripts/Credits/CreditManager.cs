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

    void Start()
    {
        production[0].SetActive(true);
        art[0].SetActive(true);
    }

    public void ProductionNext(int number)
    {
        production[number + 1].SetActive(true);

        if (number == 4)
        {
            //StartCoroutine("WaitTime");
            productionUI.SetActive(false);
            //production[0].SetActive(false);
            design[0].SetActive(true);
        }
    }

    public void DesignNext(int number)
    {
        if (number >= 2)
        {
            StartCoroutine("WaitTime");
            design[number - 1].SetActive(false);
        }
        design[number + 1].SetActive(true);

        if (number == 20)
        {
            StartCoroutine("WaitTime");
            designUI.SetActive(false);
            //design[0].SetActive(false);
        }
    }

    public void ArtNext(int number)
    {
        if (number >= 2)
        {
            StartCoroutine("WaitTime");
            art[number - 1].SetActive(false);
        }
        art[number + 1].SetActive(true);

        if (number == 21)
        {
            StartCoroutine("WaitTime");
            artUI.SetActive(false);
            //art[0].SetActive(false);
            engineering[0].SetActive(true);
        }
    }

    public void EngineeringNext(int number)
    {
        if (number >= 2)
        {
            StartCoroutine("WaitTime");
            engineering[number - 1].SetActive(false);
        }
        engineering[number + 1].SetActive(true);

        if (number == 21)
        {
            StartCoroutine("WaitTime");
            engineeringUI.SetActive(false);
            //engineering[0].SetActive(false);
        }
    }

    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1);
        
    }
}
