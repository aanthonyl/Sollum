using System.Collections;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class ShotProjectile : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float detectionRange = 5.0f;
    public float shootCooldown = 2.0f;
    public GameObject projectilePrefab;
    public Transform shootPoint;

    private Transform player;
    private bool canShoot = true;
    // Vector2 direction;
    Vector3 direction;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ShootCooldown());
    }

    private void Update()
    {
        // if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            // Face the player
            direction = (player.position - transform.position).normalized;
            // direction = new Vector3(direction.x, 0, direction.z);
            // transform.up = direction;
            transform.LookAt(new Vector3(player.position.x, 0, player.position.z));

            // Move towards the player
            // transform.Translate(direction * moveSpeed * Time.deltaTime);

            // Shoot if cooldown is over
            if (canShoot)
            {
                Shoot();
                StartCoroutine(ShootCooldown());
            }
        }
    }

    private void Shoot()
    {
        if (projectilePrefab != null && shootPoint != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
            // You may need to set the projectile's direction and speed here.
            // projectile.GetComponent<Rigidbody>().velocity = new Vector2(direction.x*1, direction.y*1);
            projectile.GetComponent<Rigidbody>().velocity = new Vector3(direction.x*1, direction.y*1, direction.z*1);
        }
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}