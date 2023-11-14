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
    public float minZ = -22;
    public float maxZ = 10;

    public float offsetX = 0;
    public float offsetY = 7;
    public float offsetZ = -15;

    public bool test = false;
    public GameObject testOBJ;

    private bool panReseted = true;

    void Start()
    {
        //set the angle of the camera looking down in the start, shouldn't be changed except possibly boss fights
        transform.rotation = Quaternion.Euler(new Vector3(15,0,0));
    }

    void Update()
    {
        //check bounds and stay within

        if(test){
            if(panReseted){
                StartCoroutine(PanToGameObject(testOBJ,3.0f));
                panReseted = false;
            }
        }else{

        float camX = player.transform.position.x + offsetX;
        float camY = player.transform.position.y + offsetY;
        float camZ = player.transform.position.z + offsetZ;

        camX = camX > maxX ? maxX : camX;
        camX = camX < minX ? minX : camX;
        camY = camY > maxY ? maxY : camY;
        camY = camY < minY ? minY : camY;               
        camZ = camZ > maxZ ? maxZ : camZ;
        camZ = camZ < minZ ? minZ : camZ;

        transform.position = new Vector3(camX, camY, camZ);
        }


    }

    IEnumerator PanToGameObject(GameObject target, float time)
    {
        Debug.Log("Panto game called");
        float currtime = 0;
        //make it smooth
        Vector3 targ = new Vector3(target.transform.position.x + offsetX, target.transform.position.y + offsetY,target.transform.position.z + offsetZ);
        Vector3 dist = new Vector3(targ.x - transform.position.x, targ.y - transform.position.y,targ.y - transform.position.z);
        float steps = time / Time.deltaTime;
        Debug.Log("how many steps: " + steps);
        dist = new Vector3(dist.x / steps, dist.y / steps, dist.z / steps);
        int timecounter = 0;
        while(transform.position.x != targ.x){
            if(timecounter > 1000){
                transform.position = transform.position + dist;
                timecounter = 0;
            }
            timecounter++;
        }
        yield return new WaitForSeconds(time); 
        panReseted = true;
        test = false;
    }
}
