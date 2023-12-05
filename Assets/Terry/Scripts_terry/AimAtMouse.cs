using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAtMouse : MonoBehaviour
{
    private float angle;
    MousePosition mouse;
    public GameObject mouseClass;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        mouseClass = GameObject.Find("MousePosition");
        mouse = mouseClass.GetComponent<MousePosition>();
    }



    // Update is called once per frame
    void Update()
    {
        Vector3 lookdir = (mouse.worldPosition - transform.position);
        angle = Mathf.Atan2(lookdir.z, lookdir.x) * Mathf.Rad2Deg - 90f;
        anim.SetFloat("Horizontal", lookdir.x);
        anim.SetFloat("Vertical", lookdir.z);
        this.transform.rotation = Quaternion.Euler(0, -angle, 0);
    }


}
