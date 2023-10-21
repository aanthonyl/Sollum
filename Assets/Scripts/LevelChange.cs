using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    bool isBossDefeat = true; //for when miniboss is defeated 
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cathedral")
        {
            SceneManager.LoadScene("Cathedral");

        }
        if (isBossDefeat && collision.gameObject.tag == "Sewer")
        {
            SceneManager.LoadScene("Level 2");
        }
    }  
}
