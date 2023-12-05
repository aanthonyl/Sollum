using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KILL());
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((gameObject.name.Equals("EnemyProjectile") && other.collider.CompareTag("Player")) || (gameObject.name.Equals("PlayerProjectile") && other.collider.CompareTag("Enemy")))
        {
            StopAllCoroutines();
            Destroy(this.gameObject);
        }
    }

    IEnumerator KILL()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
