using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateKey : MonoBehaviour
{
    public GateAnim gates;
    public DialogueTrigger foundKey;
    private GameObject gateAnimObj;
    private Image keyUI;
    private void Start()
    {
        gateAnimObj = GameObject.Find("GateAnimTrigger");
        gateAnimObj.SetActive(false);
        this.gameObject.SetActive(false);
        keyUI = GameObject.Find("Key_UI").GetComponent<Image>();
        keyUI.enabled = false;
    }
    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyUI.enabled = true;
            foundKey.gameObject.SetActive(true);
            gates.locked = false;
            this.gameObject.SetActive(false);
            gateAnimObj.SetActive(true);
        }
    }
}
