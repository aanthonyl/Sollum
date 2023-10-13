using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwapWeapon : MonoBehaviour
{
    public GameObject whip;
    public GameObject parasol;
    public TextMeshProUGUI tmp;

    void Start()
    {
        whip.SetActive(false);
        parasol.SetActive(true);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            whip.SetActive(!whip.activeSelf);
            parasol.SetActive(!parasol.activeSelf);
            if(whip.activeSelf){
                tmp.text = "weapon: whip (press f to change)";
            }else{
                tmp.text = "weapon: parasol (press f to change)";
            }
        }
    }
}
