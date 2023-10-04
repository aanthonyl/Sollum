using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        mouse = mousePosition.GetComponent<MousePosition>();
    }

    public void PlayerShoot()
    {
        GameObject projectile = Instantiate(projectile_prefab, getShootPosition(), gameObject.transform.rotation);
        rb = projectile.GetComponent<Rigidbody>();
        setForwardVector(mouse.worldPosition.normalized);
        rb.velocity = ForwardVelocity();
        
    }

}
