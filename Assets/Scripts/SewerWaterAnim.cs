using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewerWaterAnim : MonoBehaviour
{
    public float xSpeed = 0.1f;
    public float ySpeed = 0.1f;
    private float xCur;
    private float yCur;

    // Start is called before the first frame update
    void Start()
    {
        xCur = GetComponent<Renderer>().material.mainTextureOffset.x;
        yCur = GetComponent<Renderer>().material.mainTextureOffset.y;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xCur += Time.deltaTime * xSpeed;
        yCur += Time.deltaTime * ySpeed;
        GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(xCur, yCur));
    }
}
