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

    public float offsetX = 0; // 6.54f;
    public float offsetY = 7; // 8.49f;
    public float offsetZ = -15; //-7.6f;

    public bool test_Pan = false;
    public bool test_Zoom = false;
    public GameObject testOBJ;

    private bool panReseted = true;
    private bool coroutine_running = false;

    void Start()
    {
        //set the angle of the camera looking down in the start, shouldn't be changed except possibly boss fights
        transform.rotation = Quaternion.Euler(new Vector3(23,0,0)); //26.83f
        Camera.main.fieldOfView = 80;
    }

    void Update()
    {
        //check bounds and stay within
        if(test_Zoom){
            //StartCoroutine(DynamicZoom(100, 100)); //how to call zoom -> set the FOV
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

     public IEnumerator PanToPlayer()
    {
        coroutine_running = true;
        Vector3 targ = new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY,player.transform.position.z + offsetZ);
        Vector3 dist = new Vector3(targ.x - transform.position.x, targ.y - transform.position.y,targ.z - transform.position.z);
        float steps = 50;
        dist = new Vector3(dist.x / steps, dist.y / steps, dist.z / steps);
        int count = 0;
        while(count < steps){   
            transform.position += dist;
            count++;
            yield return new WaitForSeconds(0.01F); 
        }
        coroutine_running = false;
        panReseted = true;
        test_Pan = false;
    }

    public IEnumerator PanToPositionHold(Vector3 targ, float _steps)
    {
        coroutine_running = true;
        Vector3 dist = new Vector3(targ.x - transform.position.x, targ.y - transform.position.y,targ.z - transform.position.z);
        float steps = _steps;
        dist = new Vector3(dist.x / steps, dist.y / steps, dist.z / steps);
        int count = 0;
        while(count < steps){   
            transform.position += dist;
            count++;
            yield return new WaitForSeconds(0.01F); 
        }
        //coroutine_running = false;
        //panReseted = true;
        //test_Pan = false;
    }

    //call this by using StartCoroutine
    public IEnumerator DynamicZoom(float FOV, float steps){
        float addative = (FOV - Camera.main.fieldOfView) / steps;
        for(int i = 0;i < steps;i++){
            Camera.main.fieldOfView += addative;
            yield return new WaitForSeconds(0.01F);
        }
    }

    public IEnumerator DynamicZoomBack(float FOV){
        float addative = (FOV - Camera.main.fieldOfView) / 50;
        for(int i = 0;i < 50;i++){
            Camera.main.fieldOfView += addative;
            yield return new WaitForSeconds(0.01F);
        }
    }
    public IEnumerator DynamicRotationBack(float x, float y, float z){
        float addativex = (x - transform.localRotation.eulerAngles.x) / 50;
        float addativey = (y - transform.localRotation.eulerAngles.y) / 50;
        float addativez = (z - transform.localRotation.eulerAngles.z) / 50;
        for(int i = 0;i < 50;i++){
            transform.rotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x + addativex,transform.localRotation.eulerAngles.y + addativey,transform.localRotation.eulerAngles.z + addativez));
            yield return new WaitForSeconds(0.01F);
        }
    }
    
    public IEnumerator DynamicRotation(float x, float y, float z, float steps){
        float addativex = (x - transform.localRotation.eulerAngles.x) / steps;
        float addativey = (y - transform.localRotation.eulerAngles.y) / steps;
        float addativez = (z - transform.localRotation.eulerAngles.z) / steps;
        for(int i = 0;i < steps;i++){
            transform.rotation = Quaternion.Euler(new Vector3(transform.localRotation.eulerAngles.x + addativex,transform.localRotation.eulerAngles.y + addativey,transform.localRotation.eulerAngles.z + addativez));
            yield return new WaitForSeconds(0.01F);
        }
    }

    IEnumerator SetOffset(float x, float y, float z){
        offsetX = x;
        offsetY = y;
        offsetZ = z;
        yield return new WaitForSeconds(0.01F);
    }

}
