using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public LightBeam beam;
    [SerializeField]
    int bounces = 2;
    [SerializeField]
    float maxDist = 200f;
    [SerializeField]
    Material mat;
    int layerMask;
    int otherLayers;

    private void Start()
    {
        beam.Init(this.transform.position, this.transform.forward, mat);
        layerMask = 1 << 7;
        otherLayers = ~layerMask;
    }

    private void FixedUpdate()
    {
        beam.clearBeam();
        ShootLaser(transform.position, transform.TransformDirection(Vector3.forward), beam, bounces);
    }

    private void ShootLaser(Vector3 position, Vector3 direction, LightBeam beam, int numBounces)
    {
        if (numBounces == 0)
        {
            return;
        }

        beam.AddLightPoint(position);

        RaycastHit hit;
        Ray ray = new(position, direction);
        if (Physics.Raycast(position, direction, out hit, maxDist))
        {

            beam.AddLightPoint(hit.point);
            if (hit.collider.name == "DEST")
                hit.collider.gameObject.GetComponent<LightDestination>().Activation();
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Reflective"))
            {
                Debug.DrawRay(position, direction * hit.distance, Color.yellow);
                ShootLaser(hit.point, Vector3.Reflect(ray.direction, hit.normal), beam, --numBounces);
            }
        }
        else
        {
            beam.AddLightPoint(ray.GetPoint(maxDist));
            Debug.DrawRay(position, direction * maxDist, Color.white);
        }
        beam.Updatebeam();
    }
}
