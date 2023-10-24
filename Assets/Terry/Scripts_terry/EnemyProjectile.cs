using System.Collections;
using UnityEngine;

// Example Projectile class for the enemy
// Primarily for testing
public class EnemyProjectile : MonoBehaviour
{
    public GameObject shootPositionObj, projectile_prefab;
    public bool trackPlayer = false;
    public GameObject player;
    float moveSpeed = 10.0f;
    Vector3 forwardVector;
    Rigidbody rb;
    bool shooting = false;
    Vector3 getShootPosition() { return shootPositionObj.transform.position; }
    Vector3 ForwardVelocity() { return forwardVector * moveSpeed; }
    public void setForwardVector(Vector3 targetDirection) { forwardVector = targetDirection.normalized; }

    void EnemyShoot()
    {
        GameObject projectile = Instantiate(projectile_prefab, getShootPosition(), Quaternion.identity);
        rb = projectile.GetComponent<Rigidbody>();
        if (trackPlayer)
            setForwardVector(player.transform.position - shootPositionObj.transform.position);
        else
            setForwardVector(shootPositionObj.transform.forward);
        rb.velocity = ForwardVelocity();
    }

    void Update()
    {
        if (!shooting)
            StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        shooting = true;
        EnemyShoot();
        yield return new WaitForSeconds(3f);
        shooting = false;
    }
}