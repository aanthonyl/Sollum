using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float spdLimit = .7f;

    public void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    public void OnTriggerEnter(Collider playerCol)
    {
        if(playerCol.gameObject.tag == "Player")
        {
            Debug.Log("Collided with " + playerCol.gameObject.tag);
            Destroy(gameObject);
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}