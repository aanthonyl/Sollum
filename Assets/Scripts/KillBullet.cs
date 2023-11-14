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

    IEnumerator KILL()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
