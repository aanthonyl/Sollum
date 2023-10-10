using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapWeapon : MonoBehaviour
{
    public GameObject whip;
    public GameObject parasol;
    

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
        }
    }
}
