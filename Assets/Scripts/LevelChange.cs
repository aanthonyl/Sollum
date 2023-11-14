using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    bool isMiniBossDefeat = true; //for when miniboss is defeated 
    bool isBossDefeat = true; //for when final boss is defeated 


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cathedral")
        {
            SceneManager.LoadScene("Cathedral");

        }
        if (isBossDefeat && collision.gameObject.tag == "CathedralExit")
        {
            SceneManager.LoadScene("Gabrielle-Level 1");
        }
        if (isBossDefeat && collision.gameObject.tag == "Sewer")
        {
            SceneManager.LoadScene("Level 2");
        }
        if (isBossDefeat && collision.gameObject.tag == "Exit")
        {
            SceneManager.LoadScene("Credits");
        }
    }  
}
