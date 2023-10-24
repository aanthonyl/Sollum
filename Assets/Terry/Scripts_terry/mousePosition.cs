using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// casts a ray to determine where the mouse is within the scene
// used to aim parried projectiles toward the mouse
public class MousePosition : MonoBehaviour
{

    public Vector3 screenPosition;
    public Vector3 worldPosition;
    public LayerMask hitLayers;

    void Update()
    {
        screenPosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitData, hitLayers)) { worldPosition = hitData.point; }
        // Debug.Log(hitData.transform.name);
        transform.position = worldPosition;
    }
}
