using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public float rotX = 0f;
    public float rotY = 0f;
    public float rotZ = 0f;
    public float speed = 1f;

    void Update()
    {
        float rY = Mathf.SmoothStep(-rotY, rotY, Mathf.PingPong(Time.time * speed, 1));
        transform.rotation = Quaternion.Euler(0, rY, 0);
    }
}