using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public GameObject player;

    //defined min and maxes for camera bounds (subject to change)
    public float minX = -999;
    public float maxX = 999;
    public float minY = -0;
    public float maxY = 10;
    public float minZ = -7;
    public float maxZ = 10;
    public float offsetX = 0;
    public float offsetY = 7;
    public float offsetZ = -15;

    void Start()
    {
        //set the angle of the camera looking down in the start, shouldn't be changed except possibly boss fights
        transform.rotation = Quaternion.Euler(new Vector3(15, 0, 0));
    }

    void Update()
    {
        //check bounds and stay within
        float camX = player.transform.position.x + offsetX;
        float camY = player.transform.position.y + offsetY;
        float camZ = player.transform.position.z + offsetZ;

        Debug.Log("CamZ pre: " + camZ);

        if (camX > maxX) camX = maxX;
        if (camX < minX) camX = minX;
        if (camY > maxY) camY = maxY;
        if (camY < minY) camY = minY;
        if (camZ > maxZ) camZ = maxZ;
        if (camZ < minZ) camZ = minZ;

        Debug.Log("CamZ post: " + camZ); 
        
        // camX = camX > maxX ? maxX : camX;
        // camX = camX < minX ? minX : camX;
        // camY = camY > maxY ? maxY : camY;
        // camY = camY < minY ? minY : camY;               
        // camZ = camZ > maxZ ? maxZ : camZ;
        // camZ = camZ < minZ ? minZ : camZ;

        transform.position = new Vector3(camX, camY, camZ);
    }
}
