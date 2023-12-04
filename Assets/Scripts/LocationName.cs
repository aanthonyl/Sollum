// How to use:
// Create an empty trigger in the scene that the player will enter
// Attach this script to the trigger and enter the desired location name
// Create a Text TMP object in the Canvas and attach it to the script
// (the same Text TMP object can be used for each instance of the script)

// When an object with the Player tag enters the trigger, the specified location name will appear

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocationName : MonoBehaviour
{
    public TMP_Text text;
    public string locationName;
    public float fadeTime = 1;
    public float totalTime = 5;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.gameObject.SetActive(true);
            text.CrossFadeAlpha(0, 0, false);
            text.text = locationName;
            coroutine = FadeText();

            // Enable other location names and disable the current one
            // This way, the current location will not appear again until the player leaves the area
            foreach (LocationName locationName in FindObjectsOfType<LocationName>())
            {
                locationName.EndFade();
                locationName.GetComponent<Collider>().enabled = true;
            }
            StartCoroutine(coroutine);
            GetComponent<Collider>().enabled = false;
        }
    }

    private IEnumerator FadeText()
    {
        text.CrossFadeAlpha(1, fadeTime, false);
        yield return new WaitForSeconds(totalTime);
        text.CrossFadeAlpha(0, fadeTime, false);
        coroutine = null;
    }

    public void EndFade()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            text.CrossFadeAlpha(0, 0, false);
        }
    }
}
