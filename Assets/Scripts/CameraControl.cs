using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject player;

    void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(15,0,0));
    }

    void Update()
    {
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y+7,-15);
    }
}
