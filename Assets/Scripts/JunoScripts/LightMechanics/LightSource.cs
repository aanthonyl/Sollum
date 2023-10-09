using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField]
    int bounces = 2;
    float maxDist = 200f;
    int layerMask;

    private void Start()
    {
        layerMask = 1 << 7;
    }

    private void FixedUpdate()
    {
        ShootLaser(transform.position, transform.TransformDirection(Vector3.forward), bounces);
    }

    private void ShootLaser(Vector3 position, Vector3 direction, int numBounces)
    {
        if (numBounces == 0)
        {
            return;
        }

        Ray ray = new(position, direction);
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(position, direction, out RaycastHit hit, maxDist, layerMask))
        {
            Debug.DrawRay(position, direction * hit.distance, Color.yellow);
            numBounces--;
            ShootLaser(hit.point, Vector3.Reflect(ray.direction, hit.normal), numBounces);
        }
        else
        {
            Debug.DrawRay(position, direction * maxDist, Color.white);
        }
    }
}
