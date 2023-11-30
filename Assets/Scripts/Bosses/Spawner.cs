using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float cooldown = 10f;
    public MiniBoss goon;
    bool canSpawn = true;

    private void Update()
    {
        if (canSpawn)
            StartCoroutine(spawnGoon());
    }
    IEnumerator spawnGoon()
    {
        canSpawn = false;
        Instantiate(goon, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(cooldown);
        canSpawn = true;
    }
}
