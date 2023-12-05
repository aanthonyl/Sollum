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
    Animator anim;
    bool shooting = false;

    //audio
    private AudioSource source;

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

    private void Start()
    {
        anim = GetComponent<Animator>();
        source = GameObject.Find("Throw_Audio").GetComponent<AudioSource>();
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
        StartCoroutine(DelayAudio());
        if (anim != null)
            anim.SetTrigger("Shoot");
        shooting = true;
        yield return new WaitForSeconds(3f);
        shooting = false;
    }
    IEnumerator DelayAudio()
    {
        yield return new WaitForSeconds(1f);
        source.Play();

    }
}