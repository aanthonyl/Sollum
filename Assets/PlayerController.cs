using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    MousePosition mouse;
    public GameObject mouseClass;

    // Start is called before the first frame update
    void Start()
    {
        mouse= mouseClass.GetComponent<MousePosition>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(mouse.worldPosition);
    }
}
