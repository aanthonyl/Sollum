using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject shootPositionObj, projectile_prefab;
    float moveSpeed = 10.0f;
    Vector3 forwardVector;
    Rigidbody rb;
    Vector3 getShootPosition() { return shootPositionObj.transform.position; }
    Vector3 ForwardVelocity() { return forwardVector * moveSpeed; }
    public void setForwardVector(Vector3 targetDirection) { forwardVector = targetDirection; }

    void EnemyShoot()
    {
        GameObject projectile = Instantiate(projectile_prefab, getShootPosition(), gameObject.transform.rotation);
        rb = projectile.GetComponent<Rigidbody>();
        setForwardVector(transform.forward);
        rb.velocity = ForwardVelocity();
    }

    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            EnemyShoot();
        }
    }
}