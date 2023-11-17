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

    public bool test_Pan = false;
    public bool test_Zoom = false;
    public GameObject testOBJ;

    private bool panReseted = true;
    private bool coroutine_running = false;

    void Start()
    {
        //set the angle of the camera looking down in the start, shouldn't be changed except possibly boss fights
        transform.rotation = Quaternion.Euler(new Vector3(15,0,0));
    }

    void Update()
    {
        //check bounds and stay within
        if(test_Zoom){
            StartCoroutine(DynamicZoom(100)); //how to call zoom -> set the FOV
            test_Zoom = false;
        }
        if(test_Pan){
            if(panReseted){
                StartCoroutine(PanToGameObject(testOBJ,3.0f)); //how to call Pan to game object-> give it a game object and time to hold
                panReseted = false;
            }
        }else if(!coroutine_running){

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

    //call this by using StartCoroutine
    IEnumerator PanToGameObject(GameObject target, float time)
    {
        coroutine_running = true;
        Vector3 targ = new Vector3(target.transform.position.x + offsetX, target.transform.position.y + offsetY,target.transform.position.z + offsetZ);
        Vector3 dist = new Vector3(targ.x - transform.position.x, targ.y - transform.position.y,targ.z - transform.position.z);
        float steps = 100;
        dist = new Vector3(dist.x / steps, dist.y / steps, dist.z / steps);
        int count = 0;
        while(count < steps){   
            transform.position += dist;
            count++;
            yield return new WaitForSeconds(0.01F); 
        }
        yield return new WaitForSeconds(time);
        count = 0;
        while(count < steps){   
            transform.position -= dist;
            count++;
            yield return new WaitForSeconds(0.01F); 
        } 
        coroutine_running = false;
        panReseted = true;
        test_Pan = false;
    }

    //call this by using StartCoroutine
    IEnumerator DynamicZoom(float FOV){
        float addative = (FOV - Camera.main.fieldOfView) / 100;
        for(int i = 0;i < 100;i++){
            Camera.main.fieldOfView += addative;
            yield return new WaitForSeconds(0.01F);
        }
    }

}
