using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Swing : MonoBehaviour
{
    public Transform target;
    public float rotateSpeed = 2f;
    public float rotateTimer;

    void Update()
    {
        
        // while()
        // transform.RotateAround(target.position, Vector3.up, rotateSpeed*Time.deltaTime);
        float time = Time.time;
        if(time < 1f)
            transform.RotateAround(target.position, Vector3.up, 200f*Time.deltaTime);
    }
}