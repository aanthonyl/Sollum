using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelChange : MonoBehaviour
{
    bool isBossDefeat = true; //for when miniboss is defeated 
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            if (isBossDefeat & GameObject.FindWithTag("Sewer"))
            {
                SceneManager.LoadScene("Level 2");
            }
            if (GameObject.FindWithTag("Cathedrel"))
            {
                SceneManager.LoadScene("Cathedral");

            }

        }
    }
}
