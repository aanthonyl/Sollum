using System.Collections;
using UnityEngine;

// Example Projectile class for the enemy
// Primarily for testing
public class EnemyProjectile : MonoBehaviour
{
    public GameObject shootPositionObj, projectile_prefab;
    public bool trackPlayer = false;
    public GameObject player;
    public bool drawAggroRadius = false;
    public float aggroRadius = 5f;
    float moveSpeed = 10.0f;
    Vector3 forwardVector;
    Rigidbody rb;
    bool shooting = false;
    Vector3 getShootPosition() { return shootPositionObj.transform.position; }
    Vector3 ForwardVelocity() { return forwardVector * moveSpeed; }
    public void setForwardVector(Vector3 targetDirection) { forwardVector = targetDirection.normalized; }

    private void OnDrawGizmos()
    {
        if (drawAggroRadius)
        {
            Gizmos.DrawWireSphere(transform.position, aggroRadius);
        }
    }

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
        if (!shooting && Vector3.Distance(player.transform.position, transform.position) <= aggroRadius)
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