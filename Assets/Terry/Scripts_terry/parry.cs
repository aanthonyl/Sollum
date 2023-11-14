using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// this class is called when a parry has been successful
// the projectile parried is destroyed
// and a new projectile is instantiated then shot toward the mouse
public class Parry : MonoBehaviour
{
    public GameObject shootPositionObj, projectile_prefab;
    float moveSpeed = 10.0f;
    Vector3 forwardVector;
    public GameObject mousePosition;
    Rigidbody rb;
    MousePosition mouse;


    Vector3 getShootPosition() { return shootPositionObj.transform.position; }
    Vector3 ForwardVelocity() { return forwardVector * moveSpeed; }
    public void setForwardVector(Vector3 targetDirection) { forwardVector = targetDirection; }

    void Start()
    {
        mousePosition = GameObject.Find("MousePosition");
        mouse = mousePosition.GetComponent<MousePosition>();
    }

    public void PlayerShoot()
    {
        GameObject projectile = Instantiate(projectile_prefab, getShootPosition(), gameObject.transform.rotation);
        rb = projectile.GetComponent<Rigidbody>();
        setForwardVector(this.transform.forward);
        rb.velocity = ForwardVelocity();

    }

}
