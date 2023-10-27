using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] float knockBackScale = 10.0f;
    [SerializeField] Transform parasol;
    Rigidbody rb;

    public void BlockParryKnockback()
    {
        Debug.Log("BlockParryKnockback called");
        rb.AddForce(-parasol.forward * knockBackScale, ForceMode.Impulse);
        // hi
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

}
