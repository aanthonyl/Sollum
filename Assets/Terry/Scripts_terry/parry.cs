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
    float moveSpeed = 20.0f;
    Vector3 forwardVector;
    public GameObject mousePosition;
    Rigidbody rb;
    MousePosition mouse;


    Vector3 getShootPosition() { return shootPositionObj.transform.position; }
    Vector3 ForwardVelocity() { return forwardVector * moveSpeed; }
    public void setForwardVector(Vector3 targetDirection) { forwardVector = targetDirection.normalized; }

    void Start()
    {
        mouse = mousePosition.GetComponent<MousePosition>();
    }

    public void PlayerShoot(Vector3 returnDir)
    {
        GameObject projectile = Instantiate(projectile_prefab, getShootPosition(), gameObject.transform.rotation);
        rb = projectile.GetComponent<Rigidbody>();
        //setForwardVector(mouse.worldPosition.normalized);
        setForwardVector(returnDir);

        // Debug.Log("test Projectile Forward Vector: " + mouse.worldPosition);
        // Debug.Log("test Mouse World Position: " + projectile.transform.position);
        rb.velocity = ForwardVelocity();

    }

}
